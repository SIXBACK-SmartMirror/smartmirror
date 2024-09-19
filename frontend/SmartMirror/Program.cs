using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SmartMirror
{
    internal static class Program
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        static void DisableAnimation(Form form)
        {
            const int WS_EX_NOANIMATION = 0x04000000;
            const int GWL_EX_STYLE = -20;

            int style = GetWindowLong(form.Handle, GWL_EX_STYLE);
            style |= WS_EX_NOANIMATION;
            SetWindowLong(form.Handle, GWL_EX_STYLE, style);
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 모니터 인덱스를 설정
            int outputMonitor = 1;
            int inputMonitor = 2;

            Screen[] screens = Screen.AllScreens;

            if (screens.Length == 2)
            {
                inputMonitor = 1;
            }

            // MirrorOutputForm을 먼저 생성
            MirrorOutputForm mirrorOutputForm = new MirrorOutputForm();

            Screen mirror = Screen.AllScreens[outputMonitor];

            // MirrorOutputForm의 위치 및 크기를 설정
            mirrorOutputForm.StartPosition = FormStartPosition.Manual;
            mirrorOutputForm.Location = mirror.Bounds.Location;
            mirrorOutputForm.Size = new Size(mirror.Bounds.Width, mirror.Bounds.Height);

            mirrorOutputForm.Show();

            // MirrorInputForm을 생성하면서 MirrorOutputForm을 전달
            Screen inputScreen = Screen.AllScreens[inputMonitor];

            // MirrorInputForm을 생성하면서 위치를 설정하고 MirrorOutputForm을 전달
            MirrorInputForm mirrorInputForm = new MirrorInputForm(mirrorOutputForm);
            mirrorInputForm.StartPosition = FormStartPosition.Manual;
            mirrorInputForm.Location = inputScreen.Bounds.Location;
            mirrorInputForm.Size = new Size(inputScreen.Bounds.Width, inputScreen.Bounds.Height);
            Application.Run(mirrorInputForm);
        }
    }
}