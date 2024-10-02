using System.Media;

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

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 5000; // 5초마다 실행
            timer.Tick += new EventHandler(OnTimerTick);
            timer.Start();

            var audioStream = new MemoryStream(Properties.Resources.speak);
            player = new SoundPlayer(audioStream);
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            title.Visible = !title.Visible;
            pictureBox1.Visible = !pictureBox1.Visible;
            pictureBox3.Visible = !pictureBox3.Visible;

            if (title.Visible)
            {
                player.Play();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
