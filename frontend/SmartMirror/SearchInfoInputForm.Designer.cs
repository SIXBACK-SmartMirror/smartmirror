namespace SmartMirror
{
    partial class SearchInfoInputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchInfoInputForm));
            panel5 = new Panel();
            home = new Panel();
            label9 = new Label();
            research = new Panel();
            label6 = new Label();
            panel15 = new Panel();
            label8 = new Label();
            pictureBox3 = new PictureBox();
            total = new Label();
            label4 = new Label();
            panel4 = new Panel();
            panel7 = new Panel();
            panel8 = new Panel();
            label7 = new Label();
            panel6 = new Panel();
            panel5.SuspendLayout();
            home.SuspendLayout();
            research.SuspendLayout();
            panel15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            panel7.SuspendLayout();
            panel8.SuspendLayout();
            SuspendLayout();
            // 
            // panel5
            // 
            panel5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel5.BackColor = Color.FromArgb(130, 220, 40);
            panel5.Controls.Add(home);
            panel5.Controls.Add(research);
            panel5.Controls.Add(panel15);
            panel5.Location = new Point(-2, -2);
            panel5.Margin = new Padding(2);
            panel5.Name = "panel5";
            panel5.Size = new Size(99, 1015);
            panel5.TabIndex = 5;
            // 
            // home
            // 
            home.BackColor = Color.FromArgb(224, 224, 224);
            home.Controls.Add(label9);
            home.Location = new Point(2, 371);
            home.Name = "home";
            home.Size = new Size(100, 100);
            home.TabIndex = 9;
            home.Click += home_Click;
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.None;
            label9.AutoSize = true;
            label9.Font = new Font("맑은 고딕", 10F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label9.ForeColor = Color.Black;
            label9.Location = new Point(33, 34);
            label9.Name = "label9";
            label9.Size = new Size(32, 28);
            label9.TabIndex = 0;
            label9.Text = "홈";
            label9.TextAlign = ContentAlignment.MiddleCenter;
            label9.Click += home_Click;
            // 
            // research
            // 
            research.BackColor = Color.FromArgb(224, 224, 224);
            research.Controls.Add(label6);
            research.Location = new Point(0, 271);
            research.Name = "research";
            research.Size = new Size(100, 100);
            research.TabIndex = 8;
            research.Click += research_Click;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.None;
            label6.AutoSize = true;
            label6.Font = new Font("맑은 고딕", 10F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label6.ForeColor = Color.Black;
            label6.Location = new Point(13, 35);
            label6.Name = "label6";
            label6.Size = new Size(72, 28);
            label6.TabIndex = 0;
            label6.Text = "재검색";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            label6.Click += research_Click;
            // 
            // panel15
            // 
            panel15.BackColor = Color.White;
            panel15.Controls.Add(label8);
            panel15.Location = new Point(0, 171);
            panel15.Name = "panel15";
            panel15.Size = new Size(100, 100);
            panel15.TabIndex = 9;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.None;
            label8.AutoSize = true;
            label8.Font = new Font("맑은 고딕", 10F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label8.Location = new Point(24, 37);
            label8.Name = "label8";
            label8.Size = new Size(52, 28);
            label8.TabIndex = 0;
            label8.Text = "결과";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox3
            // 
            pictureBox3.Anchor = AnchorStyles.Left;
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(25, 9);
            pictureBox3.Margin = new Padding(2);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(53, 53);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 1;
            pictureBox3.TabStop = false;
            // 
            // total
            // 
            total.Anchor = AnchorStyles.Left;
            total.AutoSize = true;
            total.Font = new Font("맑은 고딕", 20F, FontStyle.Bold);
            total.Location = new Point(82, 9);
            total.Margin = new Padding(2, 0, 2, 0);
            total.Name = "total";
            total.Size = new Size(433, 54);
            total.TabIndex = 0;
            total.Text = "'롬앤'에 대한 검색결과";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.BackColor = Color.White;
            label4.Font = new Font("맑은 고딕", 15F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(255, 120, 120);
            label4.Location = new Point(1250, 17);
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
            // panel8
            // 
            panel8.BackColor = Color.White;
            panel8.Controls.Add(total);
            panel8.Controls.Add(pictureBox3);
            panel8.Location = new Point(101, 25);
            panel8.Margin = new Padding(2);
            panel8.Name = "panel8";
            panel8.Size = new Size(1101, 72);
            panel8.TabIndex = 4;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label7.Location = new Point(126, 120);
            label7.Name = "label7";
            label7.Size = new Size(84, 32);
            label7.TabIndex = 2;
            label7.Text = "총 2개";
            // 
            // panel6
            // 
            panel6.Anchor = AnchorStyles.None;
            panel6.AutoScroll = true;
            panel6.BackColor = Color.White;
            panel6.Location = new Point(100, 173);
            panel6.Margin = new Padding(2);
            panel6.Name = "panel6";
            panel6.Size = new Size(1408, 823);
            panel6.TabIndex = 6;
            // 
            // SearchInfoInputForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(1470, 1000);
            Controls.Add(label7);
            Controls.Add(panel8);
            Controls.Add(panel7);
            Controls.Add(label4);
            Controls.Add(panel6);
            Controls.Add(panel5);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            Name = "SearchInfoInputForm";
            Text = "Form1";
            panel5.ResumeLayout(false);
            home.ResumeLayout(false);
            home.PerformLayout();
            research.ResumeLayout(false);
            research.PerformLayout();
            panel15.ResumeLayout(false);
            panel15.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            panel7.ResumeLayout(false);
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel5;
        private Label total;
        private PictureBox pictureBox3;
        private Label label4;
        private Panel panel4;
        private Panel panel7;
        private Panel panel8;
        private Panel panel13;
        private Panel panel12;
        private Panel panel11;
        private Panel panel10;
        private Panel panel9;
        private Panel panel3;
        private PictureBox pictureBox1;
        private Label goodsDiscountPrice;
        private Label goodsPrice;
        private Label goodsName;
        private Label brandNameKr;
        private Panel panel2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label5;
        private PictureBox pictureBox2;
        private Label label7;
        private Panel research;
        private Label label6;
        private Panel panel15;
        private Label label8;
        private Panel home;
        private Label label9;
        private PictureBox pictureBox4;
        private Panel panel6;
    }
}