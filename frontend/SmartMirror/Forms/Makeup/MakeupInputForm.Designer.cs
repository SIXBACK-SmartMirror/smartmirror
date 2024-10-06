namespace SmartMirror
{
    partial class MakeupInputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MakeupInputForm));
            filmingBtn = new Panel();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            usingBtn = new Panel();
            pictureBox4 = new PictureBox();
            label3 = new Label();
            panel5 = new Panel();
            home = new Panel();
            label10 = new Label();
            mirror = new Panel();
            label8 = new Label();
            panel8 = new Panel();
            label6 = new Label();
            panel6 = new Panel();
            panel1 = new Panel();
            pictureBox3 = new PictureBox();
            pictureBox1 = new PictureBox();
            filmingBtn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            usingBtn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            panel5.SuspendLayout();
            home.SuspendLayout();
            mirror.SuspendLayout();
            panel8.SuspendLayout();
            panel6.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // filmingBtn
            // 
            filmingBtn.BackColor = Color.FromArgb(64, 64, 64);
            filmingBtn.Controls.Add(pictureBox2);
            filmingBtn.Controls.Add(label2);
            filmingBtn.Location = new Point(188, 343);
            filmingBtn.Margin = new Padding(2);
            filmingBtn.Name = "filmingBtn";
            filmingBtn.Size = new Size(471, 362);
            filmingBtn.TabIndex = 2;
            filmingBtn.Click += filmingBtn_Click;
            filmingBtn.Paint += panel_Paint;
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
            pictureBox2.Click += filmingBtn_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 20F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Location = new Point(170, 214);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(143, 54);
            label2.TabIndex = 1;
            label2.Text = "재촬영";
            label2.Click += filmingBtn_Click;
            // 
            // usingBtn
            // 
            usingBtn.BackColor = Color.FromArgb(255, 158, 158);
            usingBtn.Controls.Add(pictureBox4);
            usingBtn.Controls.Add(label3);
            usingBtn.Location = new Point(681, 343);
            usingBtn.Margin = new Padding(2);
            usingBtn.Name = "usingBtn";
            usingBtn.Size = new Size(471, 362);
            usingBtn.TabIndex = 3;
            usingBtn.Click += usingBtn_Click;
            usingBtn.Paint += panel_Paint;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(177, 101);
            pictureBox4.Margin = new Padding(2);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(100, 100);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 6;
            pictureBox4.TabStop = false;
            pictureBox4.Click += usingBtn_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 20F, FontStyle.Bold);
            label3.ForeColor = Color.White;
            label3.Location = new Point(189, 214);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(103, 54);
            label3.TabIndex = 2;
            label3.Text = "선택";
            label3.Click += usingBtn_Click;
            // 
            // panel5
            // 
            panel5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel5.BackColor = Color.FromArgb(115, 210, 44);
            panel5.Controls.Add(home);
            panel5.Controls.Add(mirror);
            panel5.Controls.Add(panel8);
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
            home.TabIndex = 6;
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
            // mirror
            // 
            mirror.BackColor = Color.FromArgb(213, 250, 183);
            mirror.Controls.Add(label8);
            mirror.Location = new Point(2, 284);
            mirror.Margin = new Padding(2);
            mirror.Name = "mirror";
            mirror.Size = new Size(100, 100);
            mirror.TabIndex = 5;
            mirror.Click += mirror_Click;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Left;
            label8.AutoSize = true;
            label8.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            label8.ForeColor = Color.FromArgb(115, 210, 44);
            label8.Location = new Point(25, 37);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(52, 28);
            label8.TabIndex = 0;
            label8.Text = "거울";
            label8.Click += mirror_Click;
            // 
            // panel8
            // 
            panel8.BackColor = Color.FromArgb(115, 210, 44);
            panel8.Controls.Add(label6);
            panel8.Location = new Point(2, 84);
            panel8.Margin = new Padding(2);
            panel8.Name = "panel8";
            panel8.Size = new Size(100, 100);
            panel8.TabIndex = 4;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            label6.ForeColor = Color.FromArgb(213, 250, 183);
            label6.Location = new Point(24, 37);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(52, 28);
            label6.TabIndex = 0;
            label6.Text = "메뉴";
            // 
            // panel6
            // 
            panel6.Anchor = AnchorStyles.None;
            panel6.BackColor = Color.White;
            panel6.Controls.Add(panel1);
            panel6.Controls.Add(pictureBox1);
            panel6.Controls.Add(filmingBtn);
            panel6.Controls.Add(usingBtn);
            panel6.Location = new Point(100, -2);
            panel6.Margin = new Padding(2);
            panel6.Name = "panel6";
            panel6.Size = new Size(1373, 998);
            panel6.TabIndex = 6;
            // 
            // panel1
            // 
            panel1.Controls.Add(pictureBox3);
            panel1.Location = new Point(27, 26);
            panel1.Name = "panel1";
            panel1.Size = new Size(425, 158);
            panel1.TabIndex = 8;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(3, 3);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(334, 135);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 7;
            pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(171, 720);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(411, 109);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // MakeupInputForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(1470, 1000);
            Controls.Add(panel6);
            Controls.Add(panel5);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            Name = "MakeupInputForm";
            Text = "Form1";
            filmingBtn.ResumeLayout(false);
            filmingBtn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            usingBtn.ResumeLayout(false);
            usingBtn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            panel5.ResumeLayout(false);
            home.ResumeLayout(false);
            home.PerformLayout();
            mirror.ResumeLayout(false);
            mirror.PerformLayout();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            panel6.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel filmingBtn;
        private Label label2;
        private Panel usingBtn;
        private Label label3;
        private Panel panel5;
        private Panel panel6;
        private Label label6;
        private PictureBox pictureBox2;
        private PictureBox pictureBox4;
        private Panel panel8;
        private Panel mirror;
        private PictureBox pictureBox1;
        private Label label9;
        private Label label1;
        private Label label8;
        private Panel home;
        private Label label10;
        private PictureBox pictureBox3;
        private Panel panel1;
    }
}