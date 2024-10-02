using SmartMirror.Helpers;

namespace SmartMirror
{
    public partial class MirrorInputForm : Form
    {
        private MirrorOutputForm outputForm;
        private Screen[] screens = Screen.AllScreens;
        private int outputMonitor = 1;
        private int inputMonitor = 2;

        public MirrorInputForm(MirrorOutputForm outputForm)
        {
            InitializeComponent();
            this.outputForm = outputForm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            var screens = Screen.AllScreens;
            var (primaryScreen, secondaryScreen) = FormHelper.SetupScreens(outputMonitor, ref inputMonitor, screens);

            MainOutputForm mainOutputForm = new MainOutputForm();
            MainInputForm mainInputForm = new MainInputForm(mainOutputForm);

            FormHelper.SwitchToForm(mainInputForm, mainOutputForm, primaryScreen, secondaryScreen);

            if (outputForm != null && !outputForm.IsDisposed)
            {
                outputForm.Hide();
            }
        }
    }
}
