import os
import numpy as np
import torch
import facer
from fastapi import FastAPI, File, UploadFile
from fastapi.responses import JSONResponse
import io
from PIL import Image
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
print(device)
# 주피터 노트북에서 실행
# os.environ["CUDA_DEVICE_ORDER"] = "PCI_BUS_ID"
# os.environ["CUDA_VISIBLE_DEVICES"] = "6"

@app.post("/ai/custom")
async def parse_face(inputImage: UploadFile = File(...)):
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

    print("hello")
    # 얼굴 세그멘테이션
    face_parser = facer.face_parser('farl/lapa/448', device=device)
    print("hello2")

    with torch.inference_mode():
        faces = face_parser(image_tensor, faces)
    print("hello3")

    # 세그멘테이션 맵 추출
    seg_logits = faces['seg']['logits']
    seg_probs = seg_logits.softmax(dim=1)
    seg_result = seg_probs.argmax(dim=1).squeeze().cpu().numpy()
    print("hello4")

    # 세그멘테이션 결과를 JSON으로 반환(너무 커서 docs로 테스트 시 반환값 보이지 않음)
    return JSONResponse(content={"segmentation": seg_result.tolist()})
    print(f"Segmentation result shape: {seg_result.shape}")

    # test1: 세그멘테이션 결과의 일부분만 반환
    # return create_response("A00", {"segImage": seg_result[:5, :5].tolist()}, status_code=200)
    
    # test2: 파일로 저장 후 반환
    # 세그멘테이션 결과를 파일로 저장
    save_path = "segmentation_result.json"
    with open(save_path, "w") as f:
        json.dump({"segmentation": seg_result.tolist()}, f)

    # 파일 경로 반환
    # return create_response("A00", {"message": "Segmentation result saved", "file_url": f"/path/to/{save_path}"}, status_code=200)