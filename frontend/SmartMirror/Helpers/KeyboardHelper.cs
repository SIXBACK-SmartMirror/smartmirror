using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SmartMirror.Helpers
{
    public static class KeyboardHelper
    {
        // Windows API 선언
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private const uint SWP_NOZORDER = 0x0004;
        private const uint SWP_NOACTIVATE = 0x0010;

        private static Process oskProcess;

        public static void ShowOnScreenKeyboard(int inputMonitor, Screen[] screens)
        {
            CloseOnScreenKeyboard();

            oskProcess = Process.Start("osk.exe");

            // 가상 키보드가 실행될 시간을 대기
            System.Threading.Thread.Sleep(500);

            // 가상 키보드 창을 특정 모니터로 이동
            MoveOnScreenKeyboardToMonitor(inputMonitor, screens);
        }

        private static void MoveOnScreenKeyboardToMonitor(int inputMonitor, Screen[] screens)
        {
            if (screens.Length == 2)
            {
                inputMonitor = 0; // 2개의 모니터 중 첫 번째로 설정
            }

            Screen selectedMonitor = screens[inputMonitor];
            IntPtr hWnd = FindWindow("IPTip_Main_Window", "keyboard"); // 가상 키보드 창 클래스 이름

            if (hWnd != IntPtr.Zero)
            {
                // 모니터 위치로 가상 키보드 이동
                // SetWindowPos(hWnd, IntPtr.Zero, selectedMonitor.Bounds.X, selectedMonitor.Bounds.Y, selectedMonitor.Bounds.Width, selectedMonitor.Bounds.Height, SWP_NOZORDER | SWP_NOACTIVATE);
            }
        }

        public static void CloseOnScreenKeyboard()
        {
            foreach (var process in Process.GetProcessesByName("osk"))
            {
                process.Kill();
            }
        }
    }
}
