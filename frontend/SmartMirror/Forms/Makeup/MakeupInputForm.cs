using System.Drawing.Drawing2D;
using SmartMirror.Helpers;

namespace SmartMirror
{
    public partial class MakeupInputForm : Form
    {
        private MakeupOutputForm outputForm;
        private StyleInputForm styleInputForm;

        private int outputMonitor = 1;
        private int inputMonitor = 2;

        private bool flag = false;

        public MakeupInputForm(MakeupOutputForm outputForm)
        {
            this.outputForm = outputForm;
            InitializeComponent();
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
            StyleInputForm openStyleInputForm = Application.OpenForms["StyleInputForm"] as StyleInputForm;
            if (openStyleInputForm != null && !openStyleInputForm.Visible)
            {
                Console.WriteLine("합성하기 다시 클릭");
                this.Hide();
                openStyleInputForm.Show();
            }
            else
            {
                StyleInputForm styleInputForm = new StyleInputForm();
                this.Hide();
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

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainInputForm openMainInputForm = Application.OpenForms["MainInputForm"] as MainInputForm;
            openMainInputForm.Show();

            MainOutputForm openMainOutputForm = Application.OpenForms["MainOutputForm"] as MainOutputForm;
            openMainOutputForm.Show();
        }

        private void customsMakeup_Click(object sender, EventArgs e)
        {
            CustomsMakeupInputForm openCustomsMakeupInputForm = Application.OpenForms["CustomsMakeupInputForm"] as CustomsMakeupInputForm;
            if (openCustomsMakeupInputForm != null && !openCustomsMakeupInputForm.Visible)
            {
                Console.WriteLine("커스텀 화장 다시 클릭");
                this.Hide();
                openCustomsMakeupInputForm.Show();
            }
            else
            {
                CustomsMakeupInputForm customsMakeupInputForm = new CustomsMakeupInputForm();
                this.Hide();
                customsMakeupInputForm.Show();
            }
        }

        private void mirror_Click(object sender, EventArgs e)
        {
            outputForm.pictureBox1.Visible = flag;
            outputForm.captureImg.Visible = flag;
            outputForm.topComent.Visible = flag;
            outputForm.streamingBox.Visible = flag;
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

            this.Hide();
        }
    }
}