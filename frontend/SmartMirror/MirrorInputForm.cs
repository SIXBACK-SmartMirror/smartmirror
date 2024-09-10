namespace SmartMirror
{
    public partial class MirrorInputForm : Form
    {
        private MirrorOutputForm mirrorOutputForm;
        private int monitorIndex = 1;

        public MirrorInputForm(MirrorOutputForm outputForm)
        {
            InitializeComponent();
            this.mirrorOutputForm = outputForm;
        }

        private void MirrorInputForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 현재 MirrorInputForm을 숨김
            this.Hide();

            // MainInputForm을 생성하고 표시
            MainInputForm mainInputForm = new MainInputForm();

            // 메인 인풋 폼을 특정 모니터에 표시 (예: 첫 번째 모니터)
            Screen primaryScreen = Screen.AllScreens[0];
            mainInputForm.StartPosition = FormStartPosition.Manual;
            mainInputForm.Location = primaryScreen.Bounds.Location;
            //mainInputForm.Size = new Size(primaryScreen.Bounds.Width, primaryScreen.Bounds.Height);
            mainInputForm.Show();

            // MirrorOutputForm을 MainOutputForm으로 변경
            MainOutputForm mainOutputForm = new MainOutputForm();

            // MainOutputForm을 두 번째 모니터에 표시
            Screen secondaryScreen = Screen.AllScreens[monitorIndex];
            mainOutputForm.StartPosition = FormStartPosition.Manual;
            mainOutputForm.Location = secondaryScreen.Bounds.Location;
            mainOutputForm.Size = new Size(secondaryScreen.Bounds.Width, secondaryScreen.Bounds.Height);

            mirrorOutputForm.Hide(); // MirrorOutputForm을 숨기고
            mainOutputForm.Show();   // MainOutputForm을 표시
        }
    }
}
