import os
import numpy as np
import cv2
import torch
import facer
from fastapi import FastAPI, File, UploadFile, Form, Request, Form, HTTPException
from fastapi.responses import JSONResponse
import io
from PIL import Image, UnidentifiedImageError
import base64
import logging
from pydantic import BaseModel
import json
import requests
import dlib
import hashlib
import shutil
from threading import Thread
import time 
from contextlib import asynccontextmanager

# CUDA 설정
device = 'cuda' if torch.cuda.is_available() else 'cpu'
# 주피터 노트북에서 실행
# os.environ["CUDA_DEVICE_ORDER"] = "PCI_BUS_ID"
# os.environ["CUDA_VISIBLE_DEVICES"] = "6"
print(device)

# 세그멘테이션 캐시 디렉토리 설정
segmentation_cache_dir = "./segmentation_cache/"
# SSD 경로가 없으면 생성
if not os.path.exists(segmentation_cache_dir):
    os.makedirs(segmentation_cache_dir)

# 캐시 유지 시간 (초 단위, 30분 = 1800초)
CACHE_EXPIRATION_TIME = 30 * 60

# SSD 용량 확인 함수
def check_ssd_usage():
    total, used, free = shutil.disk_usage(segmentation_cache_dir)
    total_gb = total / (1024 * 1024 * 1024)
    used_gb = used / (1024 * 1024 * 1024)
    free_gb = free / (1024 * 1024 * 1024)
    print(f"Total: {total_gb:.2f} GB, Used: {used_gb:.2f} GB, Free: {free_gb:.2f} GB")

    # 만약 사용량이 90% 이상이면 가장 오래된 파일부터 삭제
    if used / total > 0.9:
        remove_oldest_files()
        
# 오래된 파일 삭제 함수       
def remove_oldest_files():
    files = [os.path.join(segmentation_cache_dir, f) for f in os.listdir(segmentation_cache_dir)]
    files.sort(key=os.path.getmtime)  # 파일 생성 날짜 기준으로 정렬(오래된 파일부터)
    for file in files:
        os.remove(file)
        print(f"Deleted: {file}")
        break  # 하나씩 삭제하여 공간 확보
        
# 파일이 만료되었는지 확인
def is_expired(timestamp_path):
    with open(timestamp_path, "r") as f:
        timestamp = float(f.read())
    return (time.time() - timestamp) > CACHE_EXPIRATION_TIME

# 만료된 파일 삭제
def remove_expired_files():
    for file_name in os.listdir(segmentation_cache_dir):
        if file_name.endswith("_timestamp.txt"):
            timestamp_path = os.path.join(segmentation_cache_dir, file_name)
            image_hash = file_name.split("_timestamp.txt")[0]
            seg_file_path = os.path.join(segmentation_cache_dir, f"{image_hash}_seg.npy")
            crop_info_path = os.path.join(segmentation_cache_dir, f"{image_hash}_cropinfo.json")  

            if is_expired(timestamp_path):
                os.remove(timestamp_path)
                if os.path.exists(seg_file_path):
                    os.remove(seg_file_path)
                if os.path.exists(crop_info_path):
                    os.remove(crop_info_path)  
                print(f"Removed expired segmentation and crop info files: {image_hash}")

# 주기적으로 만료된 파일 및 용량 체크
def cleanup_cache():
    while True:
        print("Checking for expired cache files...")
        remove_expired_files()  # 만료된 파일 삭제
        check_ssd_usage()       # SSD 용량 체크
        time.sleep(60)          # 1분마다 실행
        
# 쓰레드를 이용해 비동기적으로 캐시 정리 함수 실행
def start_cache_cleanup_thread():
    cleanup_thread = Thread(target=cleanup_cache, daemon=True)  # 데몬 쓰레드로 설정
    cleanup_thread.start()

@asynccontextmanager
async def lifespan(app: FastAPI):
    # 애플리케이션 시작 시 실행될 작업
    start_cache_cleanup_thread()  # 캐시 정리 쓰레드 시작
    yield

# 로그 설정: ERROR 이상의 로그만 출력
logging.basicConfig(level=logging.ERROR)
# 로거 생성
logger = logging.getLogger(__name__)  # __name__을 사용해 올바른 로거 생성

# FastAPI 앱 생성 및 lifespan 설정
app = FastAPI(lifespan=lifespan)

# 공통 응답 구조를 정의하는 클래스
class ApiResponse(BaseModel):
    code: str
    data: dict

# 공통 응답 생성 함수 (상태 코드 지정 가능)
def create_response(code: str, data: dict, status_code: int = 200) -> JSONResponse:
    return JSONResponse(content={"code": code, "data": data}, status_code=status_code)

# 커스텀 에러 클래스 생성
class CustomHTTPException(HTTPException):
    def __init__(self, code: str, status_code: int, detail: str = None):
        super().__init__(status_code=status_code, detail=detail)
        self.code = code
         # 예외가 발생할 때 자동으로 로그 출력
        logger.error(f"Error Code: {code}, status Code: {status_code}, Detail: {detail}")

# 전역 에러 처리 함수
@app.exception_handler(CustomHTTPException)
async def custom_http_exception_handler(request: Request, exc: CustomHTTPException):
    return create_response(exc.code, None, status_code=exc.status_code)

# 얼굴 탐지기 및 랜드마크 모델 로드 (dlib 사용)
detector = dlib.get_frontal_face_detector()

# 크롭 후 리사이즈
def resize_image(image, target_size=(500, 500)):
    return cv2.resize(image, target_size, interpolation=cv2.INTER_AREA)

# 얼굴 전처리 - 탐지 후 이미지 크롭 및 크롭 정보 저장
def detect_and_crop_largest_face(image):
    gray = cv2.cvtColor(image, cv2.COLOR_RGB2GRAY)
    dets = detector(gray, 1)  # 얼굴 탐지
    if len(dets) == 0:
        raise CustomHTTPException(code="G05", status_code=400, detail="No face detected")

    # 가장 큰 얼굴 탐지
    largest_face = max(dets, key=lambda rect: rect.width() * rect.height())
    
    # 얼굴 크기 조절: 크기가 너무 작으면 무시
    if largest_face.width() * largest_face.height() < 1000:
        raise CustomHTTPException(code="G05", status_code=400, detail="Detected face is too small")

    # 얼굴 주변으로 이미지 자르기
    margin = 0.6  # 얼굴 주위로 60% 여유 공간을 둠
    x1 = max(0, int(largest_face.left() - margin * largest_face.width()))
    y1 = max(0, int(largest_face.top() - margin * largest_face.height()))
    x2 = min(image.shape[1], int(largest_face.right() + margin * largest_face.width()))
    y2 = min(image.shape[0], int(largest_face.bottom() + margin * largest_face.height()))

    cropped_image = image[y1:y2, x1:x2]

    # 크롭된 이미지를 리사이즈
    cropped_image = resize_image(cropped_image, target_size=(500, 500))  # 500 * 500
    
    # 크롭된 이미지와 크롭 위치 저장
    crop_info = (x1, y1, x2 - x1, y2 - y1)  # (x, y, width, height)
    return cropped_image, crop_info

# 세그멘테이션 결과 저장(이미지 파일, 타임 스탬프, 크롭 정보)
def save_segmentation_result(image_hash, seg_result, crop_info):
    seg_file_path = os.path.join(segmentation_cache_dir, f"{image_hash}_seg.npy")
    timestamp_path = os.path.join(segmentation_cache_dir, f"{image_hash}_timestamp.txt")
    crop_info_path = os.path.join(segmentation_cache_dir, f"{image_hash}_cropinfo.json")

    # 세그멘테이션 결과 저장
    np.save(seg_file_path, seg_result)

    # 타임스탬프 저장
    with open(timestamp_path, "w") as f:
        f.write(str(time.time()))  # 현재 시간 기록

    # 크롭 정보 저장
    with open(crop_info_path, "w") as f:
        json.dump(crop_info, f)

    print(f"Segmentation result saved to {seg_file_path} with crop info and timestamp.")

# 얼굴 세그멘테이션 및 색상 변경 함수
def apply_color_changes(image_np, seg_result, eyebrowColor=None, skinColor=None, lipColor=None, lipMode=None):
    # 피부 및 코 색상 변경
    if skinColor is not None:
        print("skin change")
        skin_class = 1  # 피부 클래스 번호
        nose_class = 6  # 코 클래스 번호
        skin_mask = seg_result == skin_class
        nose_mask = seg_result == nose_class
        
        # 피부와 코가 인식되지 않는 경우 건너뛰기
        if np.count_nonzero(skin_mask) == 0 and np.count_nonzero(nose_mask) == 0:
            print("Skin and nose not detected, skipping skin color change.")
        else:
            # 기존 색과 새로운 색을 혼합 
            alpha_face = 0.85
            if np.count_nonzero(skin_mask) > 0:
                skin_color_expanded = np.tile(skinColor, (np.count_nonzero(skin_mask), 1))
                image_np[skin_mask] = cv2.addWeighted(image_np[skin_mask].astype(np.float32), alpha_face, skin_color_expanded.astype(np.float32), 1 - alpha_face, 0)
            
            if np.count_nonzero(nose_mask) > 0:
                nose_color_expanded = np.tile(skinColor, (np.count_nonzero(nose_mask), 1))
                image_np[nose_mask] = cv2.addWeighted(image_np[nose_mask].astype(np.float32), alpha_face, nose_color_expanded.astype(np.float32), 1 - alpha_face, 0)

    # 눈썹 색상 변경
    if eyebrowColor is not None:
        print("eyebrow change")
        left_eyebrow_class = 2  # 왼쪽 눈썹 클래스 번호
        right_eyebrow_class = 3  # 오른쪽 눈썹 클래스 번호
        left_eyebrow_mask = seg_result == left_eyebrow_class
        right_eyebrow_mask = seg_result == right_eyebrow_class

        # 눈썹이 인식되지 않는 경우 건너뛰기
        if np.count_nonzero(left_eyebrow_mask) == 0 and np.count_nonzero(right_eyebrow_mask) == 0:
            print("Eyebrows not detected, skipping eyebrow color change.")
        else:
            # 기존 색과 새로운 색을 혼합 (alpha=0.5)
            alpha = 0.82
            if np.count_nonzero(left_eyebrow_mask) > 0:
                eyebrow_color_expanded = np.tile(eyebrowColor, (np.count_nonzero(left_eyebrow_mask), 1))
                image_np[left_eyebrow_mask] = cv2.addWeighted(image_np[left_eyebrow_mask].astype(np.float32), alpha, eyebrow_color_expanded.astype(np.float32), 1 - alpha, 0)

            if np.count_nonzero(right_eyebrow_mask) > 0:
                eyebrow_color_expanded = np.tile(eyebrowColor, (np.count_nonzero(right_eyebrow_mask), 1))
                image_np[right_eyebrow_mask] = cv2.addWeighted(image_np[right_eyebrow_mask].astype(np.float32), alpha, eyebrow_color_expanded.astype(np.float32), 1 - alpha, 0)
        
    # 입술 색상 변경 (풀 립 또는 그라데이션)
    if lipMode is not None and lipColor is not None:
        upper_lip_class = 7  # 윗입술 클래스 번호
        lower_lip_class = 9  # 아랫입술 클래스 번호
        upper_lip_mask = seg_result == upper_lip_class
        lower_lip_mask = seg_result == lower_lip_class
        
        # 입술이 인식되지 않는 경우 건너뛰기
        if np.count_nonzero(upper_lip_mask) == 0 and np.count_nonzero(lower_lip_mask) == 0:
            print("Lips not detected, skipping lip color change.")
        else:
            if lipMode == "full":
                # 풀 립 색상 변경 (기존 색과 새로운 색을 혼합)
                alpha = 0.6
                if np.count_nonzero(upper_lip_mask) > 0:
                    lip_color_expanded = np.tile(lipColor, (np.count_nonzero(upper_lip_mask), 1))
                    image_np[upper_lip_mask] = cv2.addWeighted(image_np[upper_lip_mask].astype(np.float32), alpha, lip_color_expanded.astype(np.float32), 1 - alpha, 0)
                
                if np.count_nonzero(lower_lip_mask) > 0:
                    lip_color_expanded = np.tile(lipColor, (np.count_nonzero(lower_lip_mask), 1))
                    image_np[lower_lip_mask] = cv2.addWeighted(image_np[lower_lip_mask].astype(np.float32), alpha, lip_color_expanded.astype(np.float32), 1 - alpha, 0)
                print("full lip")

            elif lipMode == "gradient":
                print("gradient lip")
                # 그라데이션 립 효과 적용
                lip_mask = upper_lip_mask | lower_lip_mask
                dist_transform = cv2.distanceTransform(lip_mask.astype(np.uint8), cv2.DIST_L2, 5)
                grad_mask = cv2.normalize(dist_transform, None, alpha=0, beta=1, norm_type=cv2.NORM_MINMAX)

                # 기존 색상과 그라데이션 마스크를 혼합하여 입술 색상 적용
                for i in range(3):  # BGR 채널
                    image_np[:, :, i] = image_np[:, :, i] * (1 - grad_mask) + lipColor[i] * grad_mask

    return image_np

@app.post("/ai/custom")
async def parse_face(inputImage: UploadFile = File(...), eyebrowColor: str = Form(None), skinColor: str = Form(None), lipColor: str = Form(None), lipMode: str = Form(None)):
    # 색상 값 출력
    # print(f"EyebrowColor: {eyebrowColor}, SkinColor: {skinColor}, LipColor: {lipColor}, LipMode: {lipMode}")
    
    # inputImage가 없거나 비어 있는 경우 예외 처리
    if not inputImage:
        raise CustomHTTPException(code="F02", status_code=400, detail="Inputimage missing")
    try:
        # 이미지를 읽어서 numpy 배열로 변환
        image_bytes = await inputImage.read()
        image = Image.open(io.BytesIO(image_bytes))

        # 이미지가 RGBA 형식일 경우 RGB로 변환
        if image.mode == 'RGBA':
            image = image.convert('RGB')
        image = np.array(image)
        
        # 입력 이미지의 해시 생성
        image_hash = image_hash = hashlib.md5(image_bytes).hexdigest()
        seg_file_path = os.path.join(segmentation_cache_dir, f"{image_hash}_seg.npy")

        # 캐시에 세그멘테이션 결과가 있는지 확인
        if os.path.exists(seg_file_path):
            seg_result = np.load(seg_file_path)
            crop_info_path = os.path.join(segmentation_cache_dir, f"{image_hash}_cropinfo.json")

            # 크롭 정보를 불러옴
            with open(crop_info_path, "r") as f:
                crop_info = json.load(f)
            
            x1, y1, width, height = crop_info
            cropped_image = image[y1:y1 + height, x1:x1 + width]

            # 크롭된 이미지를 고정된 크기(500x500)로 리사이즈
            image = cv2.resize(cropped_image, (500, 500), interpolation=cv2.INTER_AREA)
            print("Segmentation result and crop info loaded from cache")
        else:
            # 얼굴 탐지 및 자르기
            image, crop_info = detect_and_crop_largest_face(image)

            # 얼굴 감지 및 세그멘테이션
            image_tensor = facer.hwc2bchw(torch.from_numpy(image).to(torch.float32)).to(device=device)
            face_detector = facer.face_detector('retinaface/mobilenet', device=device)

            with torch.inference_mode():
                faces = face_detector(image_tensor)
            
            # 얼굴 세그멘테이션
            face_parser = facer.face_parser('farl/lapa/448', device=device)
            with torch.inference_mode():
                faces = face_parser(image_tensor, faces)
            
            # 세그멘테이션 맵 추출
            seg_logits = faces['seg']['logits']
            seg_probs = seg_logits.softmax(dim=1)
            seg_result = seg_probs.argmax(dim=1).squeeze().cpu().numpy()
            
            # 세그멘테이션 결과를 캐시에 저장
            save_segmentation_result(image_hash, seg_result, crop_info)

    except UnidentifiedImageError:
        # 이미지 형식이 잘못된 경우 예외 처리
        raise CustomHTTPException(code="F00", status_code=400, detail="Invalid input image format")
    except Exception as e:
        logger.error(f"Error processing image: {e}")
        # 기타 예외 처리
        raise CustomHTTPException(code="G00", status_code=500, detail="Error during face segmentation")

    try:
        # 색상 값을 처리
        eyebrowColor = np.array([int(x) for x in eyebrowColor.split(',')], dtype=np.float32) if eyebrowColor else None
        skinColor = np.array([int(x) for x in skinColor.split(',')], dtype=np.float32) if skinColor else None
        lipColor = np.array([int(x) for x in lipColor.split(',')], dtype=np.float32) if lipColor else None

        # 색상 변경 적용
        colored_image = apply_color_changes(image, seg_result, eyebrowColor, skinColor, lipColor, lipMode)

        # 이미지 BGR -> RGB로 변환 및 base64로 인코딩
        colored_image = colored_image.astype(np.uint8)
        pil_image = Image.fromarray(colored_image)
        buffer = io.BytesIO()
        pil_image.save(buffer, format="JPEG")
        img_str = base64.b64encode(buffer.getvalue()).decode("utf-8")
    
        return create_response("A00", {"makeupImage": img_str}, status_code=200)

    except ValueError as ve:
        # 색상 값 변환 오류 처리
        raise CustomHTTPException(code="G03", status_code=400, detail="Invalid color value")
    
    except Exception as e:
        # 일반적인 처리 중 오류가 발생했을 때 예외 처리
        logger.error(f"Error processing image: {e}")
        raise CustomHTTPException(code="G00", status_code=500, detail="Error applying makeup")