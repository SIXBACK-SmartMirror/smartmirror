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
            pictureBox3 = new PictureBox();
            total = new Label();
            panel8 = new Panel();
            label7 = new Label();
            panel6 = new Panel();
            label6 = new Label();
            research = new Panel();
            label11 = new Label();
            mirror = new Panel();
            label10 = new Label();
            panel1 = new Panel();
            label12 = new Label();
            panel2 = new Panel();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            panel8.SuspendLayout();
            research.SuspendLayout();
            mirror.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel5
            // 
            panel5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel5.BackColor = Color.FromArgb(115, 210, 44);
            panel5.Controls.Add(panel1);
            panel5.Controls.Add(panel2);
            panel5.Controls.Add(mirror);
            panel5.Controls.Add(research);
            panel5.Location = new Point(-2, -2);
            panel5.Margin = new Padding(2);
            panel5.Name = "panel5";
            panel5.Size = new Size(99, 1015);
            panel5.TabIndex = 5;
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
            // label6
            // 
            label6.Anchor = AnchorStyles.None;
            label6.AutoSize = true;
            label6.Font = new Font("맑은 고딕", 10F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label6.ForeColor = Color.FromArgb(115, 210, 44);
            label6.Location = new Point(16, 35);
            label6.Name = "label6";
            label6.Size = new Size(72, 28);
            label6.TabIndex = 0;
            label6.Text = "재검색";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            label6.Click += research_Click;
            // 
            // research
            // 
            research.BackColor = Color.FromArgb(213, 250, 183);
            research.Controls.Add(label6);
            research.Location = new Point(2, 384);
            research.Name = "research";
            research.Size = new Size(100, 100);
            research.TabIndex = 8;
            research.Click += research_Click;
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
            // mirror
            // 
            mirror.BackColor = Color.FromArgb(213, 250, 183);
            mirror.Controls.Add(label11);
            mirror.Location = new Point(2, 284);
            mirror.Margin = new Padding(2);
            mirror.Name = "mirror";
            mirror.Size = new Size(100, 100);
            mirror.TabIndex = 15;
            mirror.Click += mirror_Click;
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
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(213, 250, 183);
            panel1.Controls.Add(label10);
            panel1.Location = new Point(2, 184);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(100, 100);
            panel1.TabIndex = 16;
            panel1.Click += home_Click;
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
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(115, 210, 44);
            panel2.Controls.Add(label12);
            panel2.ForeColor = Color.FromArgb(213, 250, 183);
            panel2.Location = new Point(2, 84);
            panel2.Margin = new Padding(2);
            panel2.Name = "panel2";
            panel2.Size = new Size(100, 100);
            panel2.TabIndex = 14;
            // 
            // SearchInfoInputForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(1470, 1000);
            Controls.Add(label7);
            Controls.Add(panel8);
            Controls.Add(panel6);
            Controls.Add(panel5);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            Name = "SearchInfoInputForm";
            Text = "Form1";
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            research.ResumeLayout(false);
            research.PerformLayout();
            mirror.ResumeLayout(false);
            mirror.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel5;
        private Label total;
        private PictureBox pictureBox3;
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
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label5;
        private PictureBox pictureBox2;
        private Label label7;
        private PictureBox pictureBox4;
        private Panel panel6;
        private Panel panel1;
        private Label label10;
        private Panel panel2;
        private Label label12;
        private Panel mirror;
        private Label label11;
        private Panel research;
        private Label label6;
    }
}