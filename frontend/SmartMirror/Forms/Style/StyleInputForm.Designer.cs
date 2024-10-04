namespace SmartMirror
{
    partial class StyleInputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StyleInputForm));
            panel5 = new Panel();
            panel3 = new Panel();
            pictureBox3 = new PictureBox();
            label2 = new Label();
            home = new Panel();
            pictureBox2 = new PictureBox();
            label10 = new Label();
            mirror = new Panel();
            pictureBox5 = new PictureBox();
            label8 = new Label();
            panel2 = new Panel();
            pictureBox4 = new PictureBox();
            label1 = new Label();
            location = new PictureBox();
            panel6 = new Panel();
            panel9 = new Panel();
            label4 = new Label();
            panel4 = new Panel();
            panel7 = new Panel();
            panel5.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            home.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            mirror.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)location).BeginInit();
            panel6.SuspendLayout();
            panel7.SuspendLayout();
            SuspendLayout();
            // 
            // panel5
            // 
            panel5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel5.BackColor = Color.FromArgb(130, 220, 40);
            panel5.Controls.Add(panel3);
            panel5.Controls.Add(home);
            panel5.Controls.Add(mirror);
            panel5.Controls.Add(panel2);
            panel5.Location = new Point(-2, -2);
            panel5.Margin = new Padding(2);
            panel5.Name = "panel5";
            panel5.Size = new Size(99, 1015);
            panel5.TabIndex = 5;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(224, 224, 224);
            panel3.Controls.Add(pictureBox3);
            panel3.Controls.Add(label2);
            panel3.Location = new Point(2, 384);
            panel3.Margin = new Padding(2);
            panel3.Name = "panel3";
            panel3.Size = new Size(100, 100);
            panel3.TabIndex = 10;
            panel3.Click += camera_Click;
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
            pictureBox3.Click += camera_Click;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(64, 64, 64);
            label2.Location = new Point(34, 37);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(52, 28);
            label2.TabIndex = 0;
            label2.Text = "촬영";
            label2.Click += camera_Click;
            // 
            // home
            // 
            home.BackColor = Color.FromArgb(224, 224, 224);
            home.Controls.Add(pictureBox2);
            home.Controls.Add(label10);
            home.Location = new Point(2, 184);
            home.Margin = new Padding(2);
            home.Name = "home";
            home.Size = new Size(100, 100);
            home.TabIndex = 10;
            home.Click += pictureBox6_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.Left;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(11, 41);
            pictureBox2.Margin = new Padding(2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(20, 20);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox6_Click;
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
            label10.Click += pictureBox6_Click;
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
            mirror.TabIndex = 9;
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
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(pictureBox4);
            panel2.Controls.Add(label1);
            panel2.Location = new Point(2, 84);
            panel2.Margin = new Padding(2);
            panel2.Name = "panel2";
            panel2.Size = new Size(100, 100);
            panel2.TabIndex = 8;
            // 
            // pictureBox4
            // 
            pictureBox4.Anchor = AnchorStyles.Left;
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(11, 41);
            pictureBox4.Margin = new Padding(2);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(20, 20);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.TabIndex = 1;
            pictureBox4.TabStop = false;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            label1.Location = new Point(34, 37);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(52, 28);
            label1.TabIndex = 0;
            label1.Text = "메뉴";
            // 
            // location
            // 
            location.Image = (Image)resources.GetObject("location.Image");
            location.Location = new Point(131, 65);
            location.Margin = new Padding(3, 4, 3, 4);
            location.Name = "location";
            location.Size = new Size(68, 65);
            location.SizeMode = PictureBoxSizeMode.StretchImage;
            location.TabIndex = 2;
            location.TabStop = false;
            location.Visible = false;
            location.Click += location_Click;
            // 
            // panel6
            // 
            panel6.Anchor = AnchorStyles.None;
            panel6.BackColor = Color.White;
            panel6.Controls.Add(panel9);
            panel6.Location = new Point(100, 148);
            panel6.Margin = new Padding(2);
            panel6.Name = "panel6";
            panel6.Size = new Size(1373, 848);
            panel6.TabIndex = 6;
            // 
            // panel9
            // 
            panel9.AutoScroll = true;
            panel9.BackColor = Color.White;
            panel9.Location = new Point(44, 52);
            panel9.Margin = new Padding(2);
            panel9.Name = "panel9";
            panel9.Size = new Size(1265, 746);
            panel9.TabIndex = 4;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.BackColor = Color.White;
            label4.Font = new Font("맑은 고딕", 12F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(255, 120, 120);
            label4.Location = new Point(1293, 18);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(166, 32);
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
            panel7.Location = new Point(1254, 18);
            panel7.Margin = new Padding(2);
            panel7.Name = "panel7";
            panel7.Size = new Size(32, 35);
            panel7.TabIndex = 7;
            // 
            // StyleInputForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(1470, 1000);
            Controls.Add(panel7);
            Controls.Add(label4);
            Controls.Add(location);
            Controls.Add(panel6);
            Controls.Add(panel5);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            Name = "StyleInputForm";
            Text = "Form1";
            WindowState = FormWindowState.Maximized;
            Load += StyleInputForm_Load;
            panel5.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            home.ResumeLayout(false);
            home.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            mirror.ResumeLayout(false);
            mirror.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)location).EndInit();
            panel6.ResumeLayout(false);
            panel7.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel5;
        private Panel panel6;
        private Label label4;
        private Panel panel4;
        private Panel panel7;
        private Panel panel9;
        private PictureBox location;
        private Panel home;
        private PictureBox pictureBox2;
        private Label label10;
        private Panel mirror;
        private PictureBox pictureBox5;
        private Label label8;
        private Panel panel2;
        private PictureBox pictureBox4;
        private Label label1;
        private Panel panel3;
        private PictureBox pictureBox3;
        private Label label2;
    }
}