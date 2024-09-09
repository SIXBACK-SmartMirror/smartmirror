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

            ApplicationConfiguration.Initialize();
            Application.Run(new MirrorOutputForm());
        }
    }
}