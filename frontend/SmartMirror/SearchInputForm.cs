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
using System.Diagnostics;

namespace SmartMirror
{
    public partial class SearchInputForm : Form
    {
        public SearchInputForm()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            // 둥근 모서리 반지름 설정
            int cornerRadius = 15;

            // 패널의 크기
            int panelWidth = panel2.Width;
            int panelHeight = panel2.Height;

            // GraphicsPath를 사용해 둥근 모서리 경로를 생성
            GraphicsPath path = new GraphicsPath();
            path.AddArc(new Rectangle(0, 0, cornerRadius, cornerRadius), 180, 90);  // 좌상단
            path.AddArc(new Rectangle(panelWidth - cornerRadius, 0, cornerRadius, cornerRadius), 270, 90); // 우상단
            path.AddArc(new Rectangle(panelWidth - cornerRadius, panelHeight - cornerRadius, cornerRadius, cornerRadius), 0, 90); // 우하단
            path.AddArc(new Rectangle(0, panelHeight - cornerRadius, cornerRadius, cornerRadius), 90, 90); // 좌하단
            path.CloseFigure();

            // 패널의 모양을 둥근 모서리로 설정
            panel2.Region = new Region(path);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            // 둥근 모서리 반지름 설정
            int cornerRadius = 15;

            // 패널의 크기
            int panelWidth = panel3.Width;
            int panelHeight = panel3.Height;

            // GraphicsPath를 사용해 둥근 모서리 경로를 생성
            GraphicsPath path = new GraphicsPath();
            path.AddArc(new Rectangle(0, 0, cornerRadius, cornerRadius), 180, 90);  // 좌상단
            path.AddArc(new Rectangle(panelWidth - cornerRadius, 0, cornerRadius, cornerRadius), 270, 90); // 우상단
            path.AddArc(new Rectangle(panelWidth - cornerRadius, panelHeight - cornerRadius, cornerRadius, cornerRadius), 0, 90); // 우하단
            path.AddArc(new Rectangle(0, panelHeight - cornerRadius, cornerRadius, cornerRadius), 90, 90); // 좌하단
            path.CloseFigure();

            // 패널의 모양을 둥근 모서리로 설정
            panel3.Region = new Region(path);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // 둥근 모서리 반지름 설정
            int cornerRadius = 15;

            // 패널의 크기
            int panelWidth = panel1.Width;
            int panelHeight = panel1.Height;

            // GraphicsPath를 사용해 둥근 모서리 경로를 생성
            GraphicsPath path = new GraphicsPath();
            path.AddArc(new Rectangle(0, 0, cornerRadius, cornerRadius), 180, 90);  // 좌상단
            path.AddArc(new Rectangle(panelWidth - cornerRadius, 0, cornerRadius, cornerRadius), 270, 90); // 우상단
            path.AddArc(new Rectangle(panelWidth - cornerRadius, panelHeight - cornerRadius, cornerRadius, cornerRadius), 0, 90); // 우하단
            path.AddArc(new Rectangle(0, panelHeight - cornerRadius, cornerRadius, cornerRadius), 90, 90); // 좌하단
            path.CloseFigure();

            // 패널의 모양을 둥근 모서리로 설정
            panel1.Region = new Region(path);
        }

        private void ShowOnScreenKeyboard()
        {
            Process.Start("osk.exe");
        }

        private void CloseOnScreenKeyboard()
        {
            foreach (var process in Process.GetProcessesByName("osk"))
            {
                process.Kill();
            }
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            ShowOnScreenKeyboard();
        }
    }
}
