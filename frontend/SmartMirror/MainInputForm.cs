using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartMirror
{
    public partial class MainInputForm : Form
    {
        public MainInputForm()
        {
            InitializeComponent();
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

        private void MainInputForm_Load(object sender, EventArgs e)
        {

        }
    }
}
