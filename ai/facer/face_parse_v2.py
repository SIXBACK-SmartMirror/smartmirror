import os
import numpy as np
import torch
import facer
from fastapi import FastAPI, File, UploadFile
from fastapi.responses import JSONResponse
import io
from PIL import Image

app = FastAPI()

# CUDA 설정
device = 'cuda' if torch.cuda.is_available() else 'cpu'
# 주피터 노트북에서 실행
# os.environ["CUDA_DEVICE_ORDER"] = "PCI_BUS_ID"
# os.environ["CUDA_VISIBLE_DEVICES"] = "6"

@app.post("/parse-face/")
async def parse_face(inputImage: UploadFile = File(...)):
    # 이미지를 읽어서 numpy 배열로 변환
    image_bytes = await image_file.read()
    image = Image.open(io.BytesIO(image_bytes))
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

    # 세그멘테이션 결과를 JSON으로 반환
    return JSONResponse(content={"segmentation": seg_result.tolist()})
