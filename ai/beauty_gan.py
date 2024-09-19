import dlib
import tensorflow as tf
import numpy as np
from fastapi import FastAPI, UploadFile, File, Form, HTTPException, Request
from fastapi.responses import JSONResponse
from pydantic import BaseModel
from PIL import Image, UnidentifiedImageError
import io
import uvicorn
import base64
import requests

# FastAPI 인스턴스 생성
app = FastAPI()

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

# 전역 에러 처리 함수
@app.exception_handler(CustomHTTPException)
async def custom_http_exception_handler(request: Request, exc: CustomHTTPException):
    return create_response(exc.code, None, status_code=exc.status_code)

# 얼굴 탐지기 및 랜드마크 모델 로드
detector = dlib.get_frontal_face_detector()
sp = dlib.shape_predictor('models/shape_predictor_5_face_landmarks.dat')

# TensorFlow 세션 및 모델 로드
sess = tf.Session()
sess.run(tf.global_variables_initializer())

saver = tf.train.import_meta_graph('models/model.meta')
saver.restore(sess, tf.train.latest_checkpoint('models'))
graph = tf.get_default_graph()

X = graph.get_tensor_by_name('X:0')
Y = graph.get_tensor_by_name('Y:0')
Xs = graph.get_tensor_by_name('generator/xs:0')

# 얼굴 정렬 함수
def align_faces(img):
    dets = detector(img, 1)
    objs = dlib.full_object_detections()
    for detection in dets:
        s = sp(img, detection)
        objs.append(s)
    faces = dlib.get_face_chips(img, objs, size=256, padding=0.35)
    return faces

# 이미지 전처리 및 후처리 함수
def preprocess(img):
    return img / 127.5 - 1.0

def postprocess(img):
    return (img + 1.0) * 127.5

# PIL 이미지를 numpy 배열로 변환하는 함수
def pil_to_numpy(pil_img):
    return np.array(pil_img)

# 이미지 URL에서 이미지를 가져오는 함수
def get_image_from_url(url): 
    try:
        # HTTP 요청 처리
        response = requests.get(url, timeout=10)  # 타임아웃 설정
        response.raise_for_status()  # HTTP 에러 발생 시 예외 처리

        # 이미지로 변환 시도
        img = Image.open(io.BytesIO(response.content)).convert("RGB")
        return img

    # 네트워크 또는 URL 관련 예외 처리
    except requests.exceptions.RequestException:
        raise CustomHTTPException(code="G02", status_code=500, detail="Failed to fetch style image")

    # 이미지가 유효하지 않거나 손상된 경우 예외 처리
    except UnidentifiedImageError:
        raise CustomHTTPException(code="G00", status_code=500, detail="Invalid image format or corrupted image")

# FastAPI 엔드포인트
@app.post("/ai/makeup/")
async def makeup(inputImage: UploadFile = File(...), styleImage: str = Form(...)):
    # 입력 파일과 styleImage가 빈 값일 때 처리
    if not inputImage or not styleImage:
        raise CustomHTTPException(code="F02", status_code=400, detail="Input data missing")

    # inputImage를 읽어서 RGB로 변환 (파일 형식이 잘못되었을 때 처리)
    try:
        src_pil = Image.open(io.BytesIO(await inputImage.read())).convert("RGB")
    except UnidentifiedImageError:
        raise CustomHTTPException(code="F00", status_code=400, detail="Invalid input image")

    # styleImage에서 이미지를 가져오는 시도 (URL이 잘못되었거나 서버 문제일 때 처리)
    ref_pil = get_image_from_url(styleImage)

    # PIL 이미지를 numpy로 변환
    src_img = pil_to_numpy(src_pil)
    ref_img = pil_to_numpy(ref_pil)

    # 얼굴 정렬
    src_faces = align_faces(src_img)
    ref_faces = align_faces(ref_img)

    if len(src_faces) == 0 or len(ref_faces) == 0:
        raise CustomHTTPException(code="G00", status_code=500, detail="Face alignment failed")

    # 전처리
    X_img = preprocess(src_faces[0])
    X_img = np.expand_dims(X_img, axis=0)
    Y_img = preprocess(ref_faces[0])
    Y_img = np.expand_dims(Y_img, axis=0)

    # 모델 실행 (AI 처리 시간 초과에 대한 예외 처리)
    try:
        output = sess.run(Xs, feed_dict={X: X_img, Y: Y_img})
        # output = sess.run(Xs, feed_dict={X: X_img, Y: Y_img}, options=tf.RunOptions(timeout_in_ms=5000))  # 5초 타임아웃

    except tf.errors.DeadlineExceededError:
                raise CustomHTTPException(code="G01", status_code=500, detail="AI processing timeout")

    output_img = postprocess(output[0])

    # 이미지를 바이너리로 변환
    output_img_pil = Image.fromarray(output_img.astype(np.uint8))
    img_byte_arr = io.BytesIO()
    output_img_pil.save(img_byte_arr, format='JPEG')
    img_byte_arr.seek(0)

    # 이미지를 base64로 변환
    img_base64 = base64.b64encode(img_byte_arr.getvalue()).decode('utf-8')
    
    # 최종 응답 반환
    return create_response("A00", {"makeupImage": img_base64}, status_code=200)
