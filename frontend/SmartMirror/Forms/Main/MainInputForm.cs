﻿using SmartMirror.Helpers;
using System.Drawing.Drawing2D;

namespace SmartMirror
{
    public partial class MainInputForm : Form
    {
        private MainOutputForm outputForm;
        private bool isClose;
        private int outputMonitor = 1;
        private int inputMonitor = 2;
        private Screen[] screens = Screen.AllScreens;

        public MainInputForm(MainOutputForm outputForm)
        {
            InitializeComponent();
            this.outputForm = outputForm;
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            this.Hide();

            var screens = Screen.AllScreens;
            var (primaryScreen, secondaryScreen) = FormHelper.SetupScreens(outputMonitor, ref inputMonitor, screens);

            SearchOutputForm searchOutputForm = new SearchOutputForm();
            SearchInputForm searchInputForm = new SearchInputForm(searchOutputForm);

            FormHelper.SwitchToForm(searchInputForm, searchOutputForm, primaryScreen, secondaryScreen);

            if (outputForm != null && !outputForm.IsDisposed)
            {
                outputForm.Hide();
            }
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            int panelWidth = search.Width;
            int panelHeight = search.Height;

            GraphicsPath path = BoarderStyle.RoundSquare(panelWidth, panelHeight);
        
            mirror.Region = new Region(path);
            search.Region = new Region(path);
            makeup.Region = new Region(path);
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            outputForm.panel1.Dock = DockStyle.Fill;

            if (!isClose)
            {
                label9.Text = "화면 켜기";
                label1.Text = "거울 OFF";
                mirror.BackColor = Color.Gray;
                label9.ForeColor = Color.White;
                label1.ForeColor = Color.White;
                outputForm.panel1.Visible = true;
            }
            else
            {
                label9.Text = "화면 끄기";
                label1.Text = "거울 ON";
                mirror.BackColor = Color.White;
                label9.ForeColor = Color.Black;
                label1.ForeColor = Color.Black;
                outputForm.panel1.Visible = false;
            }
            isClose = !isClose;
        }

        private void makeup_Click(object sender, EventArgs e)
        {
            MakeupOutputForm openMakeupOutputForm = Application.OpenForms["MakeupOutputForm"] as MakeupOutputForm;
            if (openMakeupOutputForm == null)
            {
                int monitorIndex = 1;
                MakeupOutputForm makeupOutputForm = new MakeupOutputForm();

                Screen mirror = Screen.AllScreens[monitorIndex];

                makeupOutputForm.StartPosition = FormStartPosition.Manual;
                makeupOutputForm.Location = mirror.Bounds.Location;

                // MakeupInform show
                Console.WriteLine("연결");
                makeupOutputForm.Show();

                // MaininputForm 숨기기
                this.Hide();
                // MainoutForm 숨기기
                outputForm.Hide();

                MakeupInputForm inputForm = new MakeupInputForm(makeupOutputForm);
                //inputForm.Owner = this;
                inputForm.Show();
            }
            else
            {
                // MaininputForm 숨기기
                this.Hide();
                // MainoutForm 숨기기
                outputForm.Hide();
                // makeupout  
                openMakeupOutputForm.Show();
                // makeupinput  
                MakeupInputForm openMakeupInputForm = Application.OpenForms["MakeupInputForm"] as MakeupInputForm;
                openMakeupInputForm.Show();
            }
        }
    }
}
