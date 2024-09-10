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
            timer.Interval = 2000; // 2초마다 실행
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

            mirrorInput.Show(); // outputForm을 보여줌
            mirrorInput.BringToFront(); // outputForm을 최상위로 포커스
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
