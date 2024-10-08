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
            panel1 = new Panel();
            label3 = new Label();
            panel3 = new Panel();
            label2 = new Label();
            panel8 = new Panel();
            label12 = new Label();
            panel10 = new Panel();
            label11 = new Label();
            location = new PictureBox();
            panel6 = new Panel();
            pictureBox3 = new PictureBox();
            panel9 = new Panel();
            panel5.SuspendLayout();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panel8.SuspendLayout();
            panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)location).BeginInit();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // panel5
            // 
            panel5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel5.BackColor = Color.FromArgb(130, 220, 40);
            panel5.Controls.Add(panel1);
            panel5.Controls.Add(panel3);
            panel5.Controls.Add(panel8);
            panel5.Controls.Add(panel10);
            panel5.Location = new Point(-2, -2);
            panel5.Margin = new Padding(2);
            panel5.Name = "panel5";
            panel5.Size = new Size(99, 1015);
            panel5.TabIndex = 5;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(213, 250, 183);
            panel1.Controls.Add(label3);
            panel1.Location = new Point(2, 184);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(100, 100);
            panel1.TabIndex = 20;
            panel1.Click += home_Click;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(115, 210, 44);
            label3.Location = new Point(33, 37);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(32, 28);
            label3.TabIndex = 0;
            label3.Text = "홈";
            label3.Click += home_Click;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(213, 250, 183);
            panel3.Controls.Add(label2);
            panel3.Location = new Point(2, 384);
            panel3.Margin = new Padding(2);
            panel3.Name = "panel3";
            panel3.Size = new Size(100, 100);
            panel3.TabIndex = 10;
            panel3.Click += camera_Click;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(115, 210, 44);
            label2.Location = new Point(16, 35);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(72, 28);
            label2.TabIndex = 0;
            label2.Text = "재촬영";
            label2.Click += camera_Click;
            // 
            // panel8
            // 
            panel8.BackColor = Color.FromArgb(115, 210, 44);
            panel8.Controls.Add(label12);
            panel8.ForeColor = Color.FromArgb(213, 250, 183);
            panel8.Location = new Point(2, 84);
            panel8.Margin = new Padding(2);
            panel8.Name = "panel8";
            panel8.Size = new Size(100, 100);
            panel8.TabIndex = 18;
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
            // panel10
            // 
            panel10.BackColor = Color.FromArgb(213, 250, 183);
            panel10.Controls.Add(label11);
            panel10.Location = new Point(2, 284);
            panel10.Margin = new Padding(2);
            panel10.Name = "panel10";
            panel10.Size = new Size(100, 100);
            panel10.TabIndex = 19;
            panel10.Click += mirror_Click;
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
            // location
            // 
            location.Image = (Image)resources.GetObject("location.Image");
            location.Location = new Point(977, 40);
            location.Margin = new Padding(3, 4, 3, 4);
            location.Name = "location";
            location.Size = new Size(369, 160);
            location.SizeMode = PictureBoxSizeMode.Zoom;
            location.TabIndex = 2;
            location.TabStop = false;
            location.Visible = false;
            location.Click += location_Click;
            // 
            // panel6
            // 
            panel6.Anchor = AnchorStyles.None;
            panel6.BackColor = Color.White;
            panel6.Controls.Add(location);
            panel6.Controls.Add(pictureBox3);
            panel6.Controls.Add(panel9);
            panel6.Location = new Point(101, -2);
            panel6.Margin = new Padding(2);
            panel6.Name = "panel6";
            panel6.Size = new Size(1373, 998);
            panel6.TabIndex = 6;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(23, 25);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(334, 135);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 8;
            pictureBox3.TabStop = false;
            // 
            // panel9
            // 
            panel9.AutoScroll = true;
            panel9.BackColor = Color.White;
            panel9.Location = new Point(48, 221);
            panel9.Margin = new Padding(2);
            panel9.Name = "panel9";
            panel9.Size = new Size(1265, 746);
            panel9.TabIndex = 4;
            // 
            // StyleInputForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(1470, 1000);
            Controls.Add(panel6);
            Controls.Add(panel5);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            Name = "StyleInputForm";
            Text = "Form1";
            WindowState = FormWindowState.Maximized;
            Load += StyleInputForm_Load;
            panel5.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            panel10.ResumeLayout(false);
            panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)location).EndInit();
            panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel5;
        private Panel panel6;
        private Panel panel9;
        private PictureBox location;
        private Panel panel3;
        private Label label2;
        private Panel panel1;
        private Label label3;
        private Panel panel8;
        private Label label12;
        private Panel panel10;
        private Label label11;
        private PictureBox pictureBox3;
    }
}