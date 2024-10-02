using Microsoft.VisualBasic.Devices;
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
            //StyleInputForm openStyleInputForm = Application.OpenForms["StyleInputForm"] as StyleInputForm;
            //if (openStyleInputForm != null)
            //{
            //    openStyleInputForm.SyntheticResponseList = new string[100];
            //}
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
            // 둥근 모서리 반지름 설정
            int cornerRadius = 15;

            // 패널의 크기
            int panelWidth = customsMakeup.Width;
            int panelHeight = customsMakeup.Height;

            // GraphicsPath를 사용해 둥근 모서리 경로를 생성
            GraphicsPath path = new GraphicsPath();
            path.AddArc(new Rectangle(0, 0, cornerRadius, cornerRadius), 180, 90);  // 좌상단
            path.AddArc(new Rectangle(panelWidth - cornerRadius, 0, cornerRadius, cornerRadius), 270, 90); // 우상단
            path.AddArc(new Rectangle(panelWidth - cornerRadius, panelHeight - cornerRadius, cornerRadius, cornerRadius), 0, 90); // 우하단
            path.AddArc(new Rectangle(0, panelHeight - cornerRadius, cornerRadius, cornerRadius), 90, 90); // 좌하단
            path.CloseFigure();

            // 패널의 모양을 둥근 모서리로 설정
            customsMakeup.Region = new Region(path);
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
    }
}
