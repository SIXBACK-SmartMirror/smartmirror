import numpy as np
import cv2
import sys

# 이미지 저장
def apply_color_changes(image_path, eyebrow_color, skin_color, lip_color, lip_mode):
    # 이미지 로드
    image = cv2.imread(image_path)
    
    # 세그멘테이션 맵은 이미 생성되었다고 가정
    seg_result = ...  # 이 부분에서 세그멘테이션 결과를 가져옴 (numpy 배열)

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
        image[skin_mask] = cv2.addWeighted(image[skin_mask].astype(np.float32), alpha_face, skin_color_expanded.astype(np.float32), 1 - alpha_face, 0)
        
        # 코 색상 변경
        image[nose_mask] = cv2.addWeighted(image[nose_mask].astype(np.float32), alpha_face, nose_color_expanded.astype(np.float32), 1 - alpha_face, 0)

    # 눈썹 색상 변경
    if eyebrow_color is not None:
        left_eyebrow_class = 2  # 왼쪽 눈썹 클래스 번호
        right_eyebrow_class = 3  # 오른쪽 눈썹 클래스 번호
        left_eyebrow_mask = seg_result == left_eyebrow_class
        right_eyebrow_mask = seg_result == right_eyebrow_class

        # 기존 색과 새로운 색을 혼합 (alpha=0.5)
        alpha = 0.5
        eyebrow_color_expanded = np.tile(eyebrow_color, (np.count_nonzero(left_eyebrow_mask), 1))
        image[left_eyebrow_mask] = cv2.addWeighted(image[left_eyebrow_mask].astype(np.float32), alpha, eyebrow_color_expanded.astype(np.float32), 1 - alpha, 0)
        
        eyebrow_color_expanded = np.tile(eyebrow_color, (np.count_nonzero(right_eyebrow_mask), 1))
        image[right_eyebrow_mask] = cv2.addWeighted(image[right_eyebrow_mask].astype(np.float32), alpha, eyebrow_color_expanded.astype(np.float32), 1 - alpha, 0)

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
            image[upper_lip_mask] = cv2.addWeighted(image[upper_lip_mask].astype(np.float32), alpha, lip_color_expanded.astype(np.float32), 1 - alpha, 0)

            lip_color_expanded = np.tile(lip_color, (np.count_nonzero(lower_lip_mask), 1))
            image[lower_lip_mask] = cv2.addWeighted(image[lower_lip_mask].astype(np.float32), alpha, lip_color_expanded.astype(np.float32), 1 - alpha, 0)

        elif lip_mode == "gradient":
            # 그라데이션 립 효과 적용
            lip_mask = upper_lip_mask | lower_lip_mask
            dist_transform = cv2.distanceTransform(lip_mask.astype(np.uint8), cv2.DIST_L2, 5)
            grad_mask = cv2.normalize(dist_transform, None, alpha=0, beta=1, norm_type=cv2.NORM_MINMAX)

            # 기존 색상과 그라데이션 마스크를 혼합하여 입술 색상 적용
            for i in range(3):  # BGR 채널
                image[:, :, i] = image[:, :, i] * (1 - grad_mask) + lip_color[i] * grad_mask

    # 처리된 이미지 저장
    output_path = "output_image.jpg"
    cv2.imwrite(output_path, image)

    return output_path

if __name__ == "__main__":
    # 파라미터로 받은 인자들 처리
    image_path = sys.argv[1]
    eyebrow_color = eval(sys.argv[2])  # 예시: '[96, 89, 83]' 같은 형식
    skin_color = eval(sys.argv[3])  # 예시: '[239, 204, 172]' 같은 형식
    lip_color = eval(sys.argv[4])  # 예시: '[151, 58, 68]' 같은 형식
    lip_mode = sys.argv[5]  # "full" 또는 "gradient"
    
    # 색상 변경 함수 호출
    output_image = apply_color_changes(image_path, eyebrow_color, skin_color, lip_color, lip_mode)
    print(f"Image saved at: {output_image}")
