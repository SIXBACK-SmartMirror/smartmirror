using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace SmartMirror
{
    public partial class SearchInputForm : Form
    {
        private SearchOutputForm outputForm;
        private Process oskProcess;

        public SearchInputForm(SearchOutputForm outputForm)
        {
            InitializeComponent();
            this.outputForm = outputForm;
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            // 둥근 모서리 반지름 설정
            int cornerRadius = 15;

            // 패널의 크기
            int panelWidth = voice.Width;
            int panelHeight = voice.Height;

            // GraphicsPath를 사용해 둥근 모서리 경로를 생성
            GraphicsPath path = new GraphicsPath();
            path.AddArc(new Rectangle(0, 0, cornerRadius, cornerRadius), 180, 90);  // 좌상단
            path.AddArc(new Rectangle(panelWidth - cornerRadius, 0, cornerRadius, cornerRadius), 270, 90); // 우상단
            path.AddArc(new Rectangle(panelWidth - cornerRadius, panelHeight - cornerRadius, cornerRadius, cornerRadius), 0, 90); // 우하단
            path.AddArc(new Rectangle(0, panelHeight - cornerRadius, cornerRadius, cornerRadius), 90, 90); // 좌하단
            path.CloseFigure();

            // 패널의 모양을 둥근 모서리로 설정
            mirror.Region = new Region(path);
            voice.Region = new Region(path);
            keybaord.Region = new Region(path);
        }

        private void ShowOnScreenKeyboard()
        {
            // 이미 실행 중인 가상 키보드가 있다면 종료
            CloseOnScreenKeyboard();

            oskProcess = Process.Start("osk.exe");
            outputForm.textBox1.Focus();
        }

        private void CloseOnScreenKeyboard()
        {
            foreach (var process in Process.GetProcessesByName("osk"))
            {
                process.Kill();
            }
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            ShowOnScreenKeyboard();
        }
    }
}
