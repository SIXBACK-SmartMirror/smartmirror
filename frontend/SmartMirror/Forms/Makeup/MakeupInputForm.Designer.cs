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
            pictureBox6 = new PictureBox();
            label10 = new Label();
            mirror = new Panel();
            pictureBox5 = new PictureBox();
            label8 = new Label();
            panel8 = new Panel();
            pictureBox3 = new PictureBox();
            label6 = new Label();
            panel6 = new Panel();
            label4 = new Label();
            panel4 = new Panel();
            panel7 = new Panel();
            filmingBtn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            usingBtn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            panel5.SuspendLayout();
            home.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            mirror.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            panel6.SuspendLayout();
            panel7.SuspendLayout();
            SuspendLayout();
            // 
            // filmingBtn
            // 
            filmingBtn.BackColor = Color.FromArgb(64, 64, 64);
            filmingBtn.Controls.Add(pictureBox2);
            filmingBtn.Controls.Add(label2);
            filmingBtn.Location = new Point(226, 222);
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
            // 
            // usingBtn
            // 
            usingBtn.BackColor = Color.FromArgb(130, 220, 40);
            usingBtn.Controls.Add(pictureBox4);
            usingBtn.Controls.Add(label3);
            usingBtn.Location = new Point(701, 222);
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
            // 
            // panel5
            // 
            panel5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel5.BackColor = Color.FromArgb(130, 220, 40);
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
            home.BackColor = Color.FromArgb(224, 224, 224);
            home.Controls.Add(pictureBox6);
            home.Controls.Add(label10);
            home.Location = new Point(2, 184);
            home.Margin = new Padding(2);
            home.Name = "home";
            home.Size = new Size(100, 100);
            home.TabIndex = 6;
            home.Click += home_Click;
            // 
            // pictureBox6
            // 
            pictureBox6.Anchor = AnchorStyles.Left;
            pictureBox6.Image = (Image)resources.GetObject("pictureBox6.Image");
            pictureBox6.Location = new Point(11, 41);
            pictureBox6.Margin = new Padding(2);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(20, 20);
            pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox6.TabIndex = 1;
            pictureBox6.TabStop = false;
            // 
            // label10
            // 
            label10.Anchor = AnchorStyles.Left;
            label10.AutoSize = true;
            label10.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            label10.ForeColor = Color.FromArgb(64, 64, 64);
            label10.Location = new Point(36, 37);
            label10.Margin = new Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new Size(32, 28);
            label10.TabIndex = 0;
            label10.Text = "홈";
            // 
            // mirror
            // 
            mirror.BackColor = Color.FromArgb(224, 224, 224);
            mirror.Controls.Add(pictureBox5);
            mirror.Controls.Add(label8);
            mirror.Location = new Point(2, 284);
            mirror.Margin = new Padding(2);
            mirror.Name = "mirror";
            mirror.Size = new Size(100, 100);
            mirror.TabIndex = 5;
            mirror.Click += mirror_Click;
            // 
            // pictureBox5
            // 
            pictureBox5.Anchor = AnchorStyles.Left;
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(11, 41);
            pictureBox5.Margin = new Padding(2);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(20, 20);
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox5.TabIndex = 1;
            pictureBox5.TabStop = false;
            pictureBox5.Click += mirror_Click;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Left;
            label8.AutoSize = true;
            label8.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            label8.ForeColor = Color.FromArgb(64, 64, 64);
            label8.Location = new Point(34, 37);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(52, 28);
            label8.TabIndex = 0;
            label8.Text = "거울";
            label8.Click += mirror_Click;
            // 
            // panel8
            // 
            panel8.BackColor = Color.White;
            panel8.Controls.Add(pictureBox3);
            panel8.Controls.Add(label6);
            panel8.Location = new Point(2, 84);
            panel8.Margin = new Padding(2);
            panel8.Name = "panel8";
            panel8.Size = new Size(100, 100);
            panel8.TabIndex = 4;
            // 
            // pictureBox3
            // 
            pictureBox3.Anchor = AnchorStyles.Left;
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(11, 41);
            pictureBox3.Margin = new Padding(2);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(20, 20);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 1;
            pictureBox3.TabStop = false;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            label6.Location = new Point(34, 37);
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
            panel6.Controls.Add(filmingBtn);
            panel6.Controls.Add(usingBtn);
            panel6.Location = new Point(100, 148);
            panel6.Margin = new Padding(2);
            panel6.Name = "panel6";
            panel6.Size = new Size(1373, 848);
            panel6.TabIndex = 6;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.BackColor = Color.White;
            label4.Font = new Font("맑은 고딕", 15F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(255, 120, 120);
            label4.Location = new Point(1250, 18);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(209, 41);
            label4.TabIndex = 2;
            label4.Text = "올브영 구미점";
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(255, 120, 120);
            panel4.Location = new Point(13, 4);
            panel4.Margin = new Padding(2);
            panel4.Name = "panel4";
            panel4.Size = new Size(13, 18);
            panel4.TabIndex = 4;
            // 
            // panel7
            // 
            panel7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel7.BackColor = Color.FromArgb(130, 220, 40);
            panel7.Controls.Add(panel4);
            panel7.Location = new Point(1217, 21);
            panel7.Margin = new Padding(2);
            panel7.Name = "panel7";
            panel7.Size = new Size(32, 35);
            panel7.TabIndex = 7;
            // 
            // MakeupInputForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(1470, 1000);
            Controls.Add(panel7);
            Controls.Add(label4);
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
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            mirror.ResumeLayout(false);
            mirror.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            panel6.ResumeLayout(false);
            panel7.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel filmingBtn;
        private Label label2;
        private Panel usingBtn;
        private Label label3;
        private Panel panel5;
        private Panel panel6;
        private Label label6;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
        private PictureBox pictureBox4;
        private Label label4;
        private Panel panel4;
        private Panel panel7;
        private Panel panel8;
        private Panel mirror;
        private PictureBox pictureBox1;
        private Label label9;
        private Label label1;
        private PictureBox pictureBox5;
        private Label label8;
        private Panel home;
        private PictureBox pictureBox6;
        private Label label10;
    }
}