using SmartMirror.Helpers;
using System.Drawing.Drawing2D;

namespace SmartMirror
{
    public partial class CustomInputForm : Form
    {
        private MakeupOutputForm outputForm;
        private StyleInputForm styleInputForm;

        private int outputMonitor = 1;
        private int inputMonitor = 2;

        private bool flag = false;

        public CustomInputForm(MakeupOutputForm outputForm)
        {
            InitializeComponent();

            this.outputForm = outputForm;
            outputForm.panel1.Dock = DockStyle.Fill;
        }

        private void filmingBtn_Click(object sender, EventArgs e)
        {
            StyleInputForm openStyleInputForm = Application.OpenForms["StyleInputForm"] as StyleInputForm;

            if (openStyleInputForm != null)
            {
                openStyleInputForm.arrayRest();
            }

            Console.WriteLine("필름 버튼 클릭 성공");
            outputForm.CaptureImage();
        }

        private void usingBtn_Click(object sender, EventArgs e)
        {
            var screens = Screen.AllScreens;

            if (screens.Length == 2)
            {
                inputMonitor = 0; // 2개의 모니터 중 첫 번째로 설정
            }

            Screen primaryScreen = screens[inputMonitor];


            StyleInputForm openStyleInputForm = Application.OpenForms["StyleInputForm"] as StyleInputForm;

            if (openStyleInputForm != null && !openStyleInputForm.Visible)
            {
                openStyleInputForm.StartPosition = FormStartPosition.Manual;
                openStyleInputForm.Location = primaryScreen.Bounds.Location;
                openStyleInputForm.Size = new Size(primaryScreen.Bounds.Width, primaryScreen.Bounds.Height);

                Console.WriteLine("합성하기 다시 클릭");
                Hide();
                openStyleInputForm.Show();
            }
            else
            {
                outputForm.topComent.Text = "메이크업 스타일을 선택해 주세요";
                StyleInputForm styleInputForm = new StyleInputForm();
                styleInputForm.StartPosition = FormStartPosition.Manual;
                styleInputForm.Location = primaryScreen.Bounds.Location;
                styleInputForm.Size = new Size(primaryScreen.Bounds.Width, primaryScreen.Bounds.Height);

                Hide();
                styleInputForm.Show();
            }
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            int panelWidth = filmingBtn.Width;
            int panelHeight = filmingBtn.Height;

            GraphicsPath path = BoarderStyle.RoundSquare(panelWidth, panelHeight);

            filmingBtn.Region = new Region(path);
            usingBtn.Region = new Region(path);
        }

        private void customsMakeup_Click(object sender, EventArgs e)
        {
            CustomsMakeupInputForm openCustomsMakeupInputForm = Application.OpenForms["CustomsMakeupInputForm"] as CustomsMakeupInputForm;
            if (openCustomsMakeupInputForm != null && !openCustomsMakeupInputForm.Visible)
            {
                Console.WriteLine("커스텀 화장 다시 클릭");
                Hide();
                openCustomsMakeupInputForm.Show();
            }
            else
            {
                CustomsMakeupInputForm customsMakeupInputForm = new CustomsMakeupInputForm();
                Hide();
                customsMakeupInputForm.Show();
            }
        }

        private void mirror_Click(object sender, EventArgs e)
        {
            outputForm.panel1.Visible = flag;
            flag = !flag;
        }

        private void home_Click(object sender, EventArgs e)
        {
            var screens = Screen.AllScreens;
            var (primaryScreen, secondaryScreen) = FormHelper.SetupScreens(outputMonitor, ref inputMonitor, screens);

            MainOutputForm mainOutputForm = new MainOutputForm();
            MainInputForm inputForm = new MainInputForm(mainOutputForm);

            FormHelper.SwitchToForm(inputForm, mainOutputForm, primaryScreen, secondaryScreen);

            if (outputForm != null && !outputForm.IsDisposed)
            {
                outputForm.Hide();
            }

            Hide();
        }
    }
}
