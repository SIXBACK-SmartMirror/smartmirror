namespace SmartMirror
{
    public partial class MirrorOutputForm : Form
    {
        private MirrorInputForm mirrorInput;
        private System.Windows.Forms.Timer timer;

        public MirrorOutputForm()
        {
            InitializeComponent();
            //this.DoubleBuffered = true;
            //this.FormBorderStyle = FormBorderStyle.None;

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // 1초마다 실행 (1000 밀리초)
            timer.Tick += new EventHandler(OnTimerTick);
            timer.Start();
        }

        private void MirrorOutputForm_Load(object sender, EventArgs e)
        {
            if (mirrorInput == null || mirrorInput.IsDisposed)
            {
                mirrorInput = new MirrorInputForm();
            }

            mirrorInput.Show(); // outputForm을 보여줌
            mirrorInput.BringToFront(); // outputForm을 최상위로 포커스
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            title.Visible = !title.Visible;
            pictureBox1.Visible = !pictureBox1.Visible;
        }
    }
}
