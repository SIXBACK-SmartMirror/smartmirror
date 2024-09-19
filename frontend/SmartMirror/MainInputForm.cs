using System.Drawing.Drawing2D;

namespace SmartMirror
{
    public partial class MainInputForm : Form
    {
        private MainOutputForm mainOutputForm;
        private bool isClose;
        private int outputMonitor = 1;
        private int inputMonitor = 2;

        public MainInputForm(MainOutputForm mainOutputForm)
        {
            InitializeComponent();
            this.mainOutputForm = mainOutputForm;
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            // 현재 MirrorInputForm을 숨김
            this.Hide();

            // 연결된 모니터 리스트 가져오기
            Screen[] screens = Screen.AllScreens;

            if (screens.Length == 2)
            {
                inputMonitor = 1;
            }

            // MirrorOutputForm을 MainOutputForm으로 변경
            SearchOutputForm searchOutputForm = new SearchOutputForm();

            // MainOutputForm을 두 번째 모니터에 표시
            Screen secondaryScreen = Screen.AllScreens[outputMonitor];
            searchOutputForm.StartPosition = FormStartPosition.Manual;
            searchOutputForm.Location = secondaryScreen.Bounds.Location;
            searchOutputForm.Size = new Size(secondaryScreen.Bounds.Width, secondaryScreen.Bounds.Height);

            // MainInputForm을 생성하고 표시
            SearchInputForm searchInputForm = new SearchInputForm(searchOutputForm);

            // 메인 인풋 폼을 특정 모니터에 표시 (예: 첫 번째 모니터)
            Screen primaryScreen = Screen.AllScreens[inputMonitor];
            searchInputForm.StartPosition = FormStartPosition.Manual;
            searchInputForm.Location = primaryScreen.Bounds.Location;
            searchInputForm.Size = new Size(primaryScreen.Bounds.Width, primaryScreen.Bounds.Height);
            searchInputForm.Show();

            // 이미 떠있는 MainOutputForm을 숨김
            if (mainOutputForm != null && !mainOutputForm.IsDisposed)
            {
                mainOutputForm.Hide(); // MainOutputForm 숨기기
            }

            // SearchOutputForm 표시
            searchOutputForm.Show();
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            // 둥근 모서리 반지름 설정
            int cornerRadius = 15;

            // 패널의 크기
            int panelWidth = panel2.Width;
            int panelHeight = panel2.Height;

            // GraphicsPath를 사용해 둥근 모서리 경로를 생성
            GraphicsPath path = new GraphicsPath();
            path.AddArc(new Rectangle(0, 0, cornerRadius, cornerRadius), 180, 90);  // 좌상단
            path.AddArc(new Rectangle(panelWidth - cornerRadius, 0, cornerRadius, cornerRadius), 270, 90); // 우상단
            path.AddArc(new Rectangle(panelWidth - cornerRadius, panelHeight - cornerRadius, cornerRadius, cornerRadius), 0, 90); // 우하단
            path.AddArc(new Rectangle(0, panelHeight - cornerRadius, cornerRadius, cornerRadius), 90, 90); // 좌하단
            path.CloseFigure();

            // 패널의 모양을 둥근 모서리로 설정
            panel1.Region = new Region(path);
            panel2.Region = new Region(path);
            panel3.Region = new Region(path);
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            mainOutputForm.panel1.Dock = DockStyle.Fill;
            
            if (!isClose)
            {
                label9.Text = "화면 켜기";
                label1.Text = "거울 OFF";
                mainOutputForm.panel1.Visible = true;
            }
            else
            {
                label9.Text = "화면 끄기";
                label1.Text = "거울 ON";
                mainOutputForm.panel1.Visible = false;
            }
            isClose = !isClose;
        }
    }
}
