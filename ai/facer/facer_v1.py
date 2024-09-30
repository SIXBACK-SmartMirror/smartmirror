import os
import numpy as np
import cv2
import torch
import facer
from fastapi import FastAPI, File, UploadFile
from fastapi.responses import JSONResponse
import io
from PIL import Image
import base64
import logging
from pydantic import BaseModel
import json

# 로그 설정: ERROR 이상의 로그만 출력
logging.basicConfig(level=logging.ERROR)
# 로거 생성
logger = logging.getLogger(__name__)  # __name__을 사용해 올바른 로거 생성

# 공통 응답 구조를 정의하는 클래스
class ApiResponse(BaseModel):
    code: str
    data: dict

# 공통 응답 생성 함수 (상태 코드 지정 가능)
def create_response(code: str, data: dict, status_code: int = 200) -> JSONResponse:
    return JSONResponse(content={"code": code, "data": data}, status_code=status_code)

app = FastAPI()

# CUDA 설정
device = 'cuda' if torch.cuda.is_available() else 'cpu'
# 주피터 노트북에서 실행
# os.environ["CUDA_DEVICE_ORDER"] = "PCI_BUS_ID"
# os.environ["CUDA_VISIBLE_DEVICES"] = "6"
print(device)

# 얼굴 세그멘테이션 및 색상 변경 함수
def apply_color_changes(image_np, seg_result, eyebrow_color, skin_color, lip_color, lip_mode):
    # 피부 및 코 색상 변경
    if skin_color is not None:
        skin_class = 1  # 피부 클래스 번호
        nose_class = 6  # 코 클래스 번호
        skin_mask = seg_result == skin_class
        nose_mask = seg_result == nose_class

        # 기존 색과 새로운 색을 혼합 (alpha=0.8)
        alpha_face = 0.8
        skin_color_expanded = np.tile(skin_color, (np.count_nonzero(skin_mask), 1))
        nose_color_expanded = np.tile(skin_color, (np.count_nonzero(nose_mask), 1))

        # 피부 색상 변경
        image_np[skin_mask] = cv2.addWeighted(image_np[skin_mask].astype(np.float32), alpha_face, skin_color_expanded.astype(np.float32), 1 - alpha_face, 0)
        
        # 코 색상 변경
        image_np[nose_mask] = cv2.addWeighted(image_np[nose_mask].astype(np.float32), alpha_face, nose_color_expanded.astype(np.float32), 1 - alpha_face, 0)

    # 눈썹 색상 변경
    if eyebrow_color is not None:
        left_eyebrow_class = 2  # 왼쪽 눈썹 클래스 번호
        right_eyebrow_class = 3  # 오른쪽 눈썹 클래스 번호
        left_eyebrow_mask = seg_result == left_eyebrow_class
        right_eyebrow_mask = seg_result == right_eyebrow_class

        # 기존 색과 새로운 색을 혼합 (alpha=0.5)
        alpha = 0.5
        eyebrow_color_expanded = np.tile(eyebrow_color, (np.count_nonzero(left_eyebrow_mask), 1))
        image_np[left_eyebrow_mask] = cv2.addWeighted(image_np[left_eyebrow_mask].astype(np.float32), alpha, eyebrow_color_expanded.astype(np.float32), 1 - alpha, 0)
        
        eyebrow_color_expanded = np.tile(eyebrow_color, (np.count_nonzero(right_eyebrow_mask), 1))
        image_np[right_eyebrow_mask] = cv2.addWeighted(image_np[right_eyebrow_mask].astype(np.float32), alpha, eyebrow_color_expanded.astype(np.float32), 1 - alpha, 0)

    # 입술 색상 변경 (풀 립 또는 그라데이션)
    if lip_color is not None:
        upper_lip_class = 7  # 윗입술 클래스 번호
        lower_lip_class = 9  # 아랫입술 클래스 번호
        upper_lip_mask = seg_result == upper_lip_class
        lower_lip_mask = seg_result == lower_lip_class

        if lip_mode == "full":
            # 풀 립 색상 변경 (기존 색과 새로운 색을 혼합)
            alpha = 0.5
            lip_color_expanded = np.tile(lip_color, (np.count_nonzero(upper_lip_mask), 1))
            image_np[upper_lip_mask] = cv2.addWeighted(image_np[upper_lip_mask].astype(np.float32), alpha, lip_color_expanded.astype(np.float32), 1 - alpha, 0)

            lip_color_expanded = np.tile(lip_color, (np.count_nonzero(lower_lip_mask), 1))
            image_np[lower_lip_mask] = cv2.addWeighted(image_np[lower_lip_mask].astype(np.float32), alpha, lip_color_expanded.astype(np.float32), 1 - alpha, 0)

        elif lip_mode == "gradient":
            # 그라데이션 립 효과 적용
            lip_mask = upper_lip_mask | lower_lip_mask
            dist_transform = cv2.distanceTransform(lip_mask.astype(np.uint8), cv2.DIST_L2, 5)
            grad_mask = cv2.normalize(dist_transform, None, alpha=0, beta=1, norm_type=cv2.NORM_MINMAX)

            # 기존 색상과 그라데이션 마스크를 혼합하여 입술 색상 적용
            for i in range(3):  # BGR 채널
                image_np[:, :, i] = image_np[:, :, i] * (1 - grad_mask) + lip_color[i] * grad_mask

    return image_np

@app.post("/ai/custom")
async def parse_face(inputImage: UploadFile = File(...), eyebrow_color: str = "96,89,83", skin_color: str = "239,204,172", lip_color: str = "151,58,68", lip_mode: str = "full"):
    # 이미지를 읽어서 numpy 배열로 변환
    image_bytes = await inputImage.read()
    image = Image.open(io.BytesIO(image_bytes))

    # 이미지가 RGBA 형식일 경우 RGB로 변환
    if image.mode == 'RGBA':
        image = image.convert('RGB')

    image = np.array(image)

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

    # 원본 이미지 numpy로 변환 (0~1 범위라면 0~255로 변환)
    image_np = image_tensor.squeeze().permute(1, 2, 0).cpu().numpy()
    if image_np.max() <= 1:
        image_np = (image_np * 255).astype(np.uint8)

    # 색상 값을 처리
    eyebrow_color = np.array([int(x) for x in eyebrow_color.split(',')], dtype=np.float32)
    skin_color = np.array([int(x) for x in skin_color.split(',')], dtype=np.float32)
    lip_color = np.array([int(x) for x in lip_color.split(',')], dtype=np.float32)

    # 색상 변경 적용
    colored_image = apply_color_changes(image_np, seg_result, eyebrow_color, skin_color, lip_color, lip_mode)

    # 이미지 BGR -> RGB로 변환 및 base64로 인코딩
    colored_image = cv2.cvtColor(colored_image.astype(np.uint8), cv2.COLOR_BGR2RGB)
    pil_image = Image.fromarray(colored_image)
    buffer = io.BytesIO()
    pil_image.save(buffer, format="JPEG")
    img_str = base64.b64encode(buffer.getvalue()).decode("utf-8")

    # return JSONResponse(content={"status": "success", "image": img_str})
    return create_response("A00", {"customImage": img_str}, status_code=200)