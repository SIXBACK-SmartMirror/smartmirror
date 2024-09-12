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
            outputForm.textBox1.KeyDown += textBox1_KeyDown;
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

        // Enter 키를 감지하는 KeyDown 이벤트 핸들러
        public void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CloseOnScreenKeyboard(); // Enter 키를 누르면 가상 키보드를 종료
                e.Handled = true; // 이벤트 처리 완료
                e.SuppressKeyPress = true; // Enter 키 입력을 텍스트 박스에 전달하지 않음

                change();
            }
        }

        private void change()
        {
            this.Hide();

            SearchInfoOutputForm searchInfoOutputForm = new SearchInfoOutputForm();

            Screen secondaryScreen = Screen.AllScreens[1];
            searchInfoOutputForm.StartPosition = FormStartPosition.Manual;
            searchInfoOutputForm.Location = secondaryScreen.Bounds.Location;
            searchInfoOutputForm.Size = new Size(secondaryScreen.Bounds.Width, secondaryScreen.Bounds.Height);

            if (outputForm != null && !outputForm.IsDisposed)
            {
                outputForm.Hide(); // MainOutputForm 숨기기
            }

            // SearchOutputForm 표시
            searchInfoOutputForm.Show();

            SearchInfoInputForm searchInputForm = new SearchInfoInputForm();

            Screen primaryScreen = Screen.AllScreens[0];
            searchInputForm.StartPosition = FormStartPosition.Manual;
            searchInputForm.Location = primaryScreen.Bounds.Location;
            searchInputForm.Show();
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            ShowOnScreenKeyboard();
        }
    }
}
