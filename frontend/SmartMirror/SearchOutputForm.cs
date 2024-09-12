using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartMirror
{
    public partial class SearchOutputForm : Form
    {
        public SearchOutputForm()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // 둥근 모서리 반지름 설정
            int cornerRadius = 30;

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

            // 초록색 테두리 그리기
            Pen greenPen = new Pen(Color.Green, 5); // 초록색, 두께 5의 테두리
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias; // 테두리의 부드러운 렌더링
            e.Graphics.DrawPath(greenPen, path);
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            label1.Text = textBox1.Text; // TextBox의 내용을 Label에 반영
        }

        // Enter 키를 감지하는 KeyDown 이벤트 핸들러
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CloseOnScreenKeyboard(); // Enter 키를 누르면 가상 키보드를 종료
                e.Handled = true; // 이벤트 처리 완료
                e.SuppressKeyPress = true; // Enter 키 입력을 텍스트 박스에 전달하지 않음
            }
        }

        private void CloseOnScreenKeyboard()
        {
            foreach (var process in Process.GetProcessesByName("osk"))
            {
                process.Kill();
            }
        }
    }
}
