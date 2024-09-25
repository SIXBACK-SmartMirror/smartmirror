using SmartMirror.Helpers;
using System.Drawing.Drawing2D;

namespace SmartMirror
{
    public partial class MainInputForm : Form
    {
        private MainOutputForm outputForm;
        private bool isClose;
        private int outputMonitor = 1;
        private int inputMonitor = 2;
        private Screen[] screens = Screen.AllScreens;

        public MainInputForm(MainOutputForm outputForm)
        {
            InitializeComponent();
            this.outputForm = outputForm;
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            this.Hide();

            var screens = Screen.AllScreens;
            var (primaryScreen, secondaryScreen) = FormHelper.SetupScreens(outputMonitor, ref inputMonitor, screens);

            SearchOutputForm searchOutputForm = new SearchOutputForm();
            SearchInputForm searchInputForm = new SearchInputForm(searchOutputForm);

            FormHelper.SwitchToForm(searchInputForm, searchOutputForm, primaryScreen, secondaryScreen);

            if (outputForm != null && !outputForm.IsDisposed)
            {
                outputForm.Hide();
            }
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            int panelWidth = search.Width;
            int panelHeight = search.Height;

            GraphicsPath path = BoarderStyle.RoundSquare(panelWidth, panelHeight);
        
            mirror.Region = new Region(path);
            search.Region = new Region(path);
            makeup.Region = new Region(path);
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            outputForm.panel1.Dock = DockStyle.Fill;

            if (!isClose)
            {
                label9.Text = "화면 켜기";
                label1.Text = "거울 OFF";
                mirror.BackColor = Color.Gray;
                outputForm.panel1.Visible = true;
            }
            else
            {
                label9.Text = "화면 끄기";
                label1.Text = "거울 ON";
                mirror.BackColor = Color.FromArgb(232, 89, 173);
                outputForm.panel1.Visible = false;
            }
            isClose = !isClose;
        }

        private void makeup_Click(object sender, EventArgs e)
        {
            int monitorIndex = 1;
            MakeupOutputForm outputForm = new MakeupOutputForm();

            Screen mirror = Screen.AllScreens[monitorIndex];

            outputForm.StartPosition = FormStartPosition.Manual;
            outputForm.Location = mirror.Bounds.Location;

            // MakeupInform show
            Console.WriteLine("연결");
            outputForm.Show();

            // MaininputForm 숨기기
            this.Hide();

            MakeupInputForm inputForm = new MakeupInputForm(outputForm);
            //inputForm.Owner = this;
            inputForm.Show();
        }
    }
}
