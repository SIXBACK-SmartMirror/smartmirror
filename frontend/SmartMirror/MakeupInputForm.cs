using Microsoft.VisualBasic.Devices;
using SmartMirror.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartMirror
{
    public partial class MakeupInputForm : Form
    {
        private MakeupOutputForm outputForm;
        private StyleInputForm styleInputForm;

        public MakeupInputForm(MakeupOutputForm outputForm)
        {
            this.outputForm = outputForm;
            InitializeComponent();
        }

        private void filmingBtn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("필름 버튼 클릭 성공");
            outputForm.CaptureImage();
        }

        private void MakeupInputForm_Load(object sender, EventArgs e)
        {

        }

        private void usingBtn_Click(object sender, EventArgs e)
        {
            StyleInputForm styleInputForm = new StyleInputForm();
            this.Hide();
            styleInputForm.Show();
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            int panelWidth = HomeBtn.Width;
            int panelHeight = HomeBtn.Height;

            GraphicsPath path = BoarderStyle.RoundSquare(panelWidth, panelHeight);

            HomeBtn.Region = new Region(path);
            filmingBtn.Region = new Region(path);
            usingBtn.Region = new Region(path);
        }
    }
}
