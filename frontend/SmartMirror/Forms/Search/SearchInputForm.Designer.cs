using System.IO;

namespace SmartMirror
{
    partial class SearchInputForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchInputForm));
            voice = new Panel();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            keybaord = new Panel();
            pictureBox4 = new PictureBox();
            label3 = new Label();
            panel5 = new Panel();
            home = new Panel();
            label10 = new Label();
            panel2 = new Panel();
            label12 = new Label();
            mirror = new Panel();
            label11 = new Label();
            panel6 = new Panel();
            pictureBox5 = new PictureBox();
            pictureBox1 = new PictureBox();
            voice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            keybaord.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            panel5.SuspendLayout();
            home.SuspendLayout();
            panel2.SuspendLayout();
            mirror.SuspendLayout();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // voice
            // 
            voice.BackColor = Color.FromArgb(64, 64, 64);
            voice.Controls.Add(pictureBox2);
            voice.Controls.Add(label2);
            voice.Location = new Point(188, 343);
            voice.Margin = new Padding(2);
            voice.Name = "voice";
            voice.Size = new Size(471, 362);
            voice.TabIndex = 2;
            voice.Click += voice_Click;
            voice.Paint += panel_Paint;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(190, 101);
            pictureBox2.Margin = new Padding(2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(100, 100);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 5;
            pictureBox2.TabStop = false;
            pictureBox2.Click += voice_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 20F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Location = new Point(139, 214);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(197, 54);
            label2.TabIndex = 1;
            label2.Text = "음성 검색";
            label2.Click += voice_Click;
            // 
            // keybaord
            // 
            keybaord.BackColor = Color.FromArgb(140, 217, 77);
            keybaord.Controls.Add(pictureBox4);
            keybaord.Controls.Add(label3);
            keybaord.Location = new Point(681, 343);
            keybaord.Margin = new Padding(2);
            keybaord.Name = "keybaord";
            keybaord.Size = new Size(471, 362);
            keybaord.TabIndex = 3;
            keybaord.Click += panel3_Click;
            keybaord.Paint += panel_Paint;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(190, 101);
            pictureBox4.Margin = new Padding(2);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(100, 100);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 6;
            pictureBox4.TabStop = false;
            pictureBox4.Click += panel3_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 20F, FontStyle.Bold);
            label3.ForeColor = Color.White;
            label3.Location = new Point(137, 214);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(237, 54);
            label3.TabIndex = 2;
            label3.Text = "키보드 검색";
            label3.Click += panel3_Click;
            // 
            // panel5
            // 
            panel5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel5.BackColor = Color.FromArgb(115, 210, 44);
            panel5.Controls.Add(home);
            panel5.Controls.Add(panel2);
            panel5.Controls.Add(mirror);
            panel5.Location = new Point(-2, -2);
            panel5.Margin = new Padding(2);
            panel5.Name = "panel5";
            panel5.Size = new Size(99, 1015);
            panel5.TabIndex = 5;
            // 
            // home
            // 
            home.BackColor = Color.FromArgb(213, 250, 183);
            home.Controls.Add(label10);
            home.Location = new Point(2, 184);
            home.Margin = new Padding(2);
            home.Name = "home";
            home.Size = new Size(100, 100);
            home.TabIndex = 13;
            home.Click += home_Click;
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Left;
            label10.AutoSize = true;
            label10.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            label10.ForeColor = Color.FromArgb(115, 210, 44);
            label10.Location = new Point(33, 37);
            label10.Margin = new Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new Size(32, 28);
            label10.TabIndex = 0;
            label10.Text = "홈";
            label10.Click += home_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(115, 210, 44);
            panel2.Controls.Add(label12);
            panel2.ForeColor = Color.FromArgb(213, 250, 183);
            panel2.Location = new Point(2, 84);
            panel2.Margin = new Padding(2);
            panel2.Name = "panel2";
            panel2.Size = new Size(100, 100);
            panel2.TabIndex = 11;
            // 
            // label12
            // 
            label12.Anchor = AnchorStyles.Left;
            label12.AutoSize = true;
            label12.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            label12.Location = new Point(24, 37);
            label12.Margin = new Padding(2, 0, 2, 0);
            label12.Name = "label12";
            label12.Size = new Size(52, 28);
            label12.TabIndex = 0;
            label12.Text = "메뉴";
            // 
            // mirror
            // 
            mirror.BackColor = Color.FromArgb(213, 250, 183);
            mirror.Controls.Add(label11);
            mirror.Location = new Point(2, 284);
            mirror.Margin = new Padding(2);
            mirror.Name = "mirror";
            mirror.Size = new Size(100, 100);
            mirror.TabIndex = 12;
            mirror.Click += mirror_Click;
            // 
            // label11
            // 
            label11.Anchor = AnchorStyles.Left;
            label11.AutoSize = true;
            label11.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            label11.ForeColor = Color.FromArgb(115, 210, 44);
            label11.Location = new Point(25, 37);
            label11.Margin = new Padding(2, 0, 2, 0);
            label11.Name = "label11";
            label11.Size = new Size(52, 28);
            label11.TabIndex = 0;
            label11.Text = "거울";
            label11.Click += mirror_Click;
            // 
            // panel6
            // 
            panel6.Anchor = AnchorStyles.None;
            panel6.BackColor = Color.White;
            panel6.Controls.Add(pictureBox5);
            panel6.Controls.Add(pictureBox1);
            panel6.Controls.Add(voice);
            panel6.Controls.Add(keybaord);
            panel6.Location = new Point(100, -2);
            panel6.Margin = new Padding(2);
            panel6.Name = "panel6";
            panel6.Size = new Size(1373, 998);
            panel6.TabIndex = 6;
            // 
            // pictureBox5
            // 
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(38, 43);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(250, 116);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 9;
            pictureBox5.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(112, 725);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(430, 120);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // SearchInputForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(1470, 1000);
            Controls.Add(panel6);
            Controls.Add(panel5);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            Name = "SearchInputForm";
            Text = "Form1";
            voice.ResumeLayout(false);
            voice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            keybaord.ResumeLayout(false);
            keybaord.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            panel5.ResumeLayout(false);
            home.ResumeLayout(false);
            home.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            mirror.ResumeLayout(false);
            mirror.PerformLayout();
            panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel voice;
        private Label label2;
        private Panel keybaord;
        private Label label3;
        private Panel panel5;
        private Panel panel6;
        private PictureBox pictureBox2;
        private PictureBox pictureBox4;
        private Panel home;
        private Label label10;
        private Panel panel2;
        private Label label12;
        private Panel mirror;
        private Label label11;
        private PictureBox pictureBox1;
        private PictureBox pictureBox5;
    }
}