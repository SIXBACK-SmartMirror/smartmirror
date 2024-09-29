import torch
import facer
import matplotlib.pyplot as plt

# GPU가 있다면 사용하고, 없다면 CPU 사용
device = 'cuda' if torch.cuda.is_available() else 'cpu'

# 이미지 로드 (자신의 이미지 경로로 변경 필요)
image_path = 's1.png'  # 여기에 이미지 파일 경로를 넣으세요.
image = facer.hwc2bchw(facer.read_hwc(image_path)).to(device=device)

# 얼굴 검출 모델 초기화 (retinaface/mobilenet 사용)
face_detector = facer.face_detector('retinaface/mobilenet', device=device)

# 얼굴 검출 수행
with torch.inference_mode():
    faces = face_detector(image)

# 얼굴 검출 결과를 시각화
facer.show_bchw(facer.draw_bchw(image, faces))

# 혹은 Matplotlib을 사용하여 이미지 시각화
plt.imshow(facer.draw_bchw(image, faces)[0].permute(1, 2, 0).cpu().numpy())
plt.axis('off')
plt.show()
