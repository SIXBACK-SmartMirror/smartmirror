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
            timer.Interval = 1000; // 1�ʸ��� ���� (1000 �и���)
            timer.Tick += new EventHandler(OnTimerTick);
            timer.Start();
        }

        private void MirrorOutputForm_Load(object sender, EventArgs e)
        {
            if (mirrorInput == null || mirrorInput.IsDisposed)
            {
                mirrorInput = new MirrorInputForm();
            }

            mirrorInput.Show(); // outputForm�� ������
            mirrorInput.BringToFront(); // outputForm�� �ֻ����� ��Ŀ��
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            title.Visible = !title.Visible;
            pictureBox1.Visible = !pictureBox1.Visible;
        }
    }
}
