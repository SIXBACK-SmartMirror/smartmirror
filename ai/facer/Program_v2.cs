using System;
using System.Diagnostics;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Python 스크립트 경로
        string scriptPath = "path_to_your_script.py";
        string imagePath = "path_to_input_image.jpg";

        // 색상 (BGR) 설정
        string eyebrowColor = "[96, 89, 83]";  // 눈썹 색상 예시
        string skinColor = "[239, 204, 172]";  // 피부 색상 예시
        string lipColor = "[151, 58, 68]";     // 입술 색상 예시

        // 입술 모드 (full 또는 gradient)
        string lipMode = "full";

        // Python 스크립트를 실행할 명령어 구성
        string command = $"{scriptPath} {imagePath} {eyebrowColor} {skinColor} {lipColor} {lipMode}";

        // Process를 사용하여 Python 스크립트 실행
        ProcessStartInfo psi = new ProcessStartInfo();
        psi.FileName = "python";  // Python 실행 파일
        psi.Arguments = command;
        psi.UseShellExecute = false;
        psi.RedirectStandardOutput = true;
        psi.RedirectStandardError = true;

        // Process 실행
        Process process = Process.Start(psi);
        StreamReader reader = process.StandardOutput;
        string resultBase64 = reader.ReadToEnd();
        process.WaitForExit();

        // Base64 결과 출력
        Console.WriteLine("Base64 Encoded Image: ");
        Console.WriteLine(resultBase64);

        // Base64 결과를 이미지 파일로 저장하는 로직 (선택 사항)
        byte[] imageBytes = Convert.FromBase64String(resultBase64);
        File.WriteAllBytes("output_image_from_base64.jpg", imageBytes);
        Console.WriteLine("Image saved as output_image_from_base64.jpg");
    }
}
