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

            // ����� �ε����� ����
            int monitorIndex = 1;

            // MirrorOutputForm�� ���� ����
            MirrorOutputForm mirrorOutputForm = new MirrorOutputForm();

            Screen mirror = Screen.AllScreens[monitorIndex];

            // MirrorOutputForm�� ��ġ �� ũ�⸦ ����
            mirrorOutputForm.StartPosition = FormStartPosition.Manual;
            mirrorOutputForm.Location = mirror.Bounds.Location;
            mirrorOutputForm.Size = new Size(mirror.Bounds.Width, mirror.Bounds.Height);

            mirrorOutputForm.Show();

            // MirrorInputForm�� �����ϸ鼭 MirrorOutputForm�� ����
            MirrorInputForm mirrorInputForm = new MirrorInputForm(mirrorOutputForm);
            Application.Run(mirrorInputForm);
        }
    }
}