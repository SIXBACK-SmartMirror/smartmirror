using System;
using Python.Runtime;

class Program_v1
{
    static void Main(string[] args)
    {
        // Python.NET을 초기화
        PythonEngine.Initialize();

        try
        {
            using (Py.GIL())  // Global Interpreter Lock 사용
            {
                // Python 스크립트 경로
                string scriptPath = "path_to_your_script.py";
                string imagePath = "path_to_input_image.jpg";

                // 색상 (BGR) 설정
                string eyebrowColor = "[96, 89, 83]";  // 예시 눈썹 색상
                string skinColor = "[239, 204, 172]";  // 예시 피부 색상
                string lipColor = "[151, 58, 68]";  // 예시 입술 색상

                // 입술 모드 (full/gradient)
                string lipMode = "full";

                // Python 명령어 실행
                string command = $"python {scriptPath} {imagePath} {eyebrowColor} {skinColor} {lipColor} {lipMode}";
                dynamic result = PythonEngine.Exec(command);

                Console.WriteLine(result);
            }
        }
        finally
        {
            PythonEngine.Shutdown();  // Python 종료
        }
    }
}
