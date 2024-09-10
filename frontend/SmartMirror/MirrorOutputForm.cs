using System.Media;
using System.Windows.Forms;

namespace SmartMirror
{
    public partial class MirrorOutputForm : Form
    {
        private MirrorInputForm mirrorInput;
        private System.Windows.Forms.Timer timer;
        private SoundPlayer player;

        public MirrorOutputForm()
        {
            InitializeComponent();
            //this.DoubleBuffered = true;
            //this.FormBorderStyle = FormBorderStyle.None;

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 2000; // 2�ʸ��� ����
            timer.Tick += new EventHandler(OnTimerTick);
            timer.Start();

            var audioStream = new MemoryStream(Properties.Resources.speak);
            player = new SoundPlayer(audioStream);
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

            if (title.Visible)
            {
                player.Play();
            }
        }
    }
}
