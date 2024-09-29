using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;

public class FaceSegmentationProcessor
{
    public void ProcessSegmentation(string segmentationJson, Bitmap originalImage)
    {
        // JSON 데이터를 파싱하여 세그멘테이션 결과를 배열로 변환
        var segmentation = JsonConvert.DeserializeObject<int[,]>(segmentationJson);

        // 입술, 눈썹, 피부 등의 색상 변경 로직 적용
        ChangeLipColor(segmentation, originalImage, Color.FromArgb(151, 58, 68));  // 입술 색상
        ChangeEyebrowColor(segmentation, originalImage, Color.FromArgb(96, 89, 83));  // 눈썹 색상
        ChangeSkinColor(segmentation, originalImage, Color.FromArgb(239, 204, 172));  // 피부 색상
    }

    private void ChangeLipColor(int[,] segmentation, Bitmap image, Color lipColor)
    {
        for (int y = 0; y < segmentation.GetLength(0); y++)
        {
            for (int x = 0; x < segmentation.GetLength(1); x++)
            {
                if (segmentation[y, x] == 7 || segmentation[y, x] == 9) // 윗입술과 아랫입술 클래스
                {
                    // 입술의 색상을 새로운 색으로 변경
                    image.SetPixel(x, y, lipColor);
                }
            }
        }
    }

    private void ChangeEyebrowColor(int[,] segmentation, Bitmap image, Color eyebrowColor)
    {
        for (int y = 0; y < segmentation.GetLength(0); y++)
        {
            for (int x = 0; x < segmentation.GetLength(1); x++)
            {
                if (segmentation[y, x] == 2 || segmentation[y, x] == 3) // 왼쪽, 오른쪽 눈썹 클래스
                {
                    image.SetPixel(x, y, eyebrowColor);
                }
            }
        }
    }

    private void ChangeSkinColor(int[,] segmentation, Bitmap image, Color skinColor)
    {
        for (int y = 0; y < segmentation.GetLength(0); y++)
        {
            for (int x = 0; x < segmentation.GetLength(1); x++)
            {
                if (segmentation[y, x] == 1 || segmentation[y, x] == 6) // 얼굴, 코 클래스
                {
                    image.SetPixel(x, y, skinColor);
                }
            }
        }
    }

    # 기본 색상 변경
    private void ChangeLipColorBasic(int[,] segmentation, Bitmap image, Color lipColor)
    {
        for (int y = 0; y < segmentation.GetLength(0); y++)
        {
            for (int x = 0; x < segmentation.GetLength(1); x++)
            {
                if (segmentation[y, x] == 7 || segmentation[y, x] == 9) // 입술 클래스
                {
                    image.SetPixel(x, y, lipColor);
                }
            }
        }
    }

    # 그라데이션 모드
    private void ChangeLipColorGradient(int[,] segmentation, Bitmap image, Color lipColor)
    {
        // 윗입술과 아랫입술을 하나의 마스크로 병합
        for (int y = 0; y < segmentation.GetLength(0); y++)
        {
            for (int x = 0; x < segmentation.GetLength(1); x++)
            {
                if (segmentation[y, x] == 7 || segmentation[y, x] == 9) // 입술 클래스
                {
                    // 그라데이션 효과 적용 (입술 중심에서 외곽으로 갈수록 색상이 바뀌도록 구현)
                    // 구현 예시: 중심에서 거리 계산 후 색상 혼합
                    // image.SetPixel(x, y, lipColor); 
                }
            }
        }
    }


}
