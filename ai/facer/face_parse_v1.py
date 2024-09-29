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

app = FastAPI()

# CUDA 설정
device = 'cuda' if torch.cuda.is_available() else 'cpu'
# 주피터 노트북에서 실행
# os.environ["CUDA_DEVICE_ORDER"] = "PCI_BUS_ID"
# os.environ["CUDA_VISIBLE_DEVICES"] = "6"
print(device)

@app.post("/ai/custom")
async def parse_face(inputImage: UploadFile = File(...), lip_color: str = "151,58,68"):
    # 이미지를 읽어서 numpy 배열로 변환
    image_bytes = await inputImage.read()
    image = Image.open(io.BytesIO(image_bytes))

    # 이미지가 RGBA 형식일 경우 RGB로 변환
    if image.mode == 'RGBA':
        image = image.convert('RGB')

    image = np.array(image)

    # 얼굴 감지
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

    # 입술 부분 선택
    upper_lip_class = 7
    lower_lip_class = 9
    upper_lip_mask = seg_result == upper_lip_class
    lower_lip_mask = seg_result == lower_lip_class

    # rgb 값을 받기
    lip_color = np.array([int(x) for x in lip_color.split(',')], dtype=np.float32)

    # 입술 색 변경
    alpha = 0.5
    colored_image = image_np.astype(np.float32)
    
    lip_color_expanded = np.tile(lip_color, (np.count_nonzero(upper_lip_mask), 1))
    colored_image[upper_lip_mask] = cv2.addWeighted(image_np[upper_lip_mask].astype(np.float32), alpha, lip_color_expanded, 1 - alpha, 0)

    lip_color_expanded = np.tile(lip_color, (np.count_nonzero(lower_lip_mask), 1))
    colored_image[lower_lip_mask] = cv2.addWeighted(image_np[lower_lip_mask].astype(np.float32), alpha, lip_color_expanded, 1 - alpha, 0)

    # 이미지 BGR -> RGB로 변환 및 base64로 인코딩
    colored_image = cv2.cvtColor(colored_image.astype(np.uint8), cv2.COLOR_BGR2RGB)
    pil_image = Image.fromarray(colored_image)
    buffer = io.BytesIO()
    pil_image.save(buffer, format="JPEG")
    img_str = base64.b64encode(buffer.getvalue()).decode("utf-8")

    return JSONResponse(content={"status": "success", "image": img_str})
