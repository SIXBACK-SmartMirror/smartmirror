namespace SmartMirror
{
    public partial class MainInputForm : Form
    {
        private Form mainOutputForm;

        public MainInputForm(Form mainOutputForm)
        {
            InitializeComponent();
            this.mainOutputForm = mainOutputForm;
        }

        private void MainInputForm_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Click(object sender, EventArgs e)
        {
            // 현재 MirrorInputForm을 숨김
            this.Hide();

            // MainInputForm을 생성하고 표시
            SearchInputForm searchInputForm = new SearchInputForm();

            // 메인 인풋 폼을 특정 모니터에 표시 (예: 첫 번째 모니터)
            Screen primaryScreen = Screen.AllScreens[0];
            searchInputForm.StartPosition = FormStartPosition.Manual;
            searchInputForm.Location = primaryScreen.Bounds.Location;
            //mainInputForm.Size = new Size(primaryScreen.Bounds.Width, primaryScreen.Bounds.Height);
            searchInputForm.Show();

            // MirrorOutputForm을 MainOutputForm으로 변경
            SearchOutputForm searchOutputForm = new SearchOutputForm();

            // MainOutputForm을 두 번째 모니터에 표시
            Screen secondaryScreen = Screen.AllScreens[1];
            searchOutputForm.StartPosition = FormStartPosition.Manual;
            searchOutputForm.Location = secondaryScreen.Bounds.Location;
            searchOutputForm.Size = new Size(secondaryScreen.Bounds.Width, secondaryScreen.Bounds.Height);

            // 이미 떠있는 MainOutputForm을 숨김
            if (mainOutputForm != null && !mainOutputForm.IsDisposed)
            {
                mainOutputForm.Hide(); // MainOutputForm 숨기기
            }

            // SearchOutputForm 표시
            searchOutputForm.Show();
        }
    }
}
