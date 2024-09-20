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
    public partial class StyleInputForm : Form
    {
        int makeupStyle = -1;
        public StyleInputForm()
        {
            InitializeComponent();
        }

        private void StyleInputForm_Load(object sender, EventArgs e)
        {
            // api 호출해서 메이크업 스타일 사진 받아오기
            // makeup img 변경
            // img url, 스타일 명, 상뭎 리스트와 페이지

        }

        private void style1_Click(object sender, EventArgs e)
        {
            // MaininputForm 숨기기
            this.Hide();

            this.synthaticOutputStart(1);
        }

        private void synthaticOutputStart(int styleNum)
        {
            int outputMonitorIndex = 1;
            int inputMonitorIndex = 0;

            Screen output = Screen.AllScreens[outputMonitorIndex];
            SyntheticOutput syntheticOutput = new SyntheticOutput(styleNum); // 나중에 수정 해야함
            syntheticOutput.StartPosition = FormStartPosition.Manual;
            syntheticOutput.Location = output.Bounds.Location;
            syntheticOutput.Show();

            Screen input = Screen.AllScreens[inputMonitorIndex]; 
            SyntheticInput syntheticInput = new SyntheticInput(styleNum); // 나중에 수정 해야함
            syntheticInput.StartPosition = FormStartPosition.Manual;
            syntheticInput.Location = input.Bounds.Location;
            syntheticInput.Show();

        }
    }
}
