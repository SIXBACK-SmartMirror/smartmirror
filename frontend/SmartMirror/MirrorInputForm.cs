using System.Windows.Forms;

namespace SmartMirror
{
    public partial class MirrorInputForm : Form
    {
        private MirrorOutputForm mirrorOutputForm;
        private Screen[] screens = Screen.AllScreens;
        private int outputMonitor = 1;
        private int inputMonitor = 2;

        public MirrorInputForm(MirrorOutputForm outputForm)
        {
            InitializeComponent();
            this.mirrorOutputForm = outputForm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 현재 MirrorInputForm을 숨김
            this.Hide();


            if (screens.Length == 2)
            {
                inputMonitor = 0;
            }

            // MirrorOutputForm을 MainOutputForm으로 변경
            MainOutputForm mainOutputForm = new MainOutputForm();

            // MainOutputForm을 두 번째 모니터에 표시
            Screen secondaryScreen = Screen.AllScreens[outputMonitor];
            mainOutputForm.StartPosition = FormStartPosition.Manual;
            mainOutputForm.Location = secondaryScreen.Bounds.Location;
            mainOutputForm.Size = new Size(secondaryScreen.Bounds.Width, secondaryScreen.Bounds.Height);

            mirrorOutputForm.Hide(); // MirrorOutputForm을 숨기고
            mainOutputForm.Show();   // MainOutputForm을 표시

            // MainInputForm을 생성하고 표시
            MainInputForm mainInputForm = new MainInputForm(mainOutputForm);

            // 메인 인풋 폼을 특정 모니터에 표시
            Screen primaryScreen = Screen.AllScreens[inputMonitor];
            mainInputForm.StartPosition = FormStartPosition.Manual;
            mainInputForm.Location = primaryScreen.Bounds.Location;
            mainInputForm.Size = new Size(primaryScreen.Bounds.Width, primaryScreen.Bounds.Height);
            mainInputForm.Show();
        }
    }
}
