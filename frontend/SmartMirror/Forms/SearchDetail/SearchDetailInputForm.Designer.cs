namespace SmartMirror
{
    partial class SearchDetailInputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchDetailInputForm));
            panel5 = new Panel();
            panel6 = new Panel();
            label10 = new Label();
            panel14 = new Panel();
            label12 = new Label();
            mirror = new Panel();
            label11 = new Label();
            panel18 = new Panel();
            label6 = new Label();
            panel17 = new Panel();
            label1 = new Label();
            panel1 = new Panel();
            button1 = new Button();
            brand = new Label();
            name = new Label();
            pictureBox5 = new PictureBox();
            pictureBox1 = new PictureBox();
            panel5.SuspendLayout();
            panel6.SuspendLayout();
            panel14.SuspendLayout();
            mirror.SuspendLayout();
            panel18.SuspendLayout();
            panel17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel5
            // 
            panel5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel5.BackColor = Color.FromArgb(115, 210, 44);
            panel5.Controls.Add(panel6);
            panel5.Controls.Add(panel14);
            panel5.Controls.Add(mirror);
            panel5.Controls.Add(panel18);
            panel5.Location = new Point(-2, -2);
            panel5.Margin = new Padding(2);
            panel5.Name = "panel5";
            panel5.Size = new Size(99, 1015);
            panel5.TabIndex = 5;
            // 
            // panel6
            // 
            panel6.BackColor = Color.FromArgb(213, 250, 183);
            panel6.Controls.Add(label10);
            panel6.Location = new Point(2, 184);
            panel6.Margin = new Padding(2);
            panel6.Name = "panel6";
            panel6.Size = new Size(100, 100);
            panel6.TabIndex = 20;
            panel6.Click += home_Click;
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
            // panel14
            // 
            panel14.BackColor = Color.FromArgb(115, 210, 44);
            panel14.Controls.Add(label12);
            panel14.ForeColor = Color.FromArgb(213, 250, 183);
            panel14.Location = new Point(2, 84);
            panel14.Margin = new Padding(2);
            panel14.Name = "panel14";
            panel14.Size = new Size(100, 100);
            panel14.TabIndex = 18;
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
            mirror.TabIndex = 19;
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
            // panel18
            // 
            panel18.BackColor = Color.FromArgb(213, 250, 183);
            panel18.Controls.Add(label6);
            panel18.Location = new Point(2, 384);
            panel18.Name = "panel18";
            panel18.Size = new Size(100, 100);
            panel18.TabIndex = 17;
            panel18.Click += reasearch_Click;
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
            label6.Click += reasearch_Click;
            // 
            // panel17
            // 
            panel17.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel17.Controls.Add(label1);
            panel17.Controls.Add(panel1);
            panel17.Controls.Add(button1);
            panel17.Controls.Add(brand);
            panel17.Controls.Add(name);
            panel17.Location = new Point(133, 195);
            panel17.Name = "panel17";
            panel17.Size = new Size(1311, 721);
            panel17.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label1.Location = new Point(474, 36);
            label1.Name = "label1";
            label1.Size = new Size(62, 32);
            label1.TabIndex = 6;
            label1.Text = "옵션";
            // 
            // panel1
            // 
            panel1.Location = new Point(474, 84);
            panel1.Name = "panel1";
            panel1.Size = new Size(777, 551);
            panel1.TabIndex = 5;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ControlLight;
            button1.Font = new Font("맑은 고딕", 14F, FontStyle.Regular, GraphicsUnit.Point, 129);
            button1.Location = new Point(50, 504);
            button1.Name = "button1";
            button1.Size = new Size(291, 131);
            button1.TabIndex = 4;
            button1.Text = "위치 끄기";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // brand
            // 
            brand.AutoSize = true;
            brand.Font = new Font("맑은 고딕", 15F, FontStyle.Regular, GraphicsUnit.Point, 129);
            brand.Location = new Point(50, 36);
            brand.Name = "brand";
            brand.Size = new Size(168, 41);
            brand.TabIndex = 0;
            brand.Text = "이니스프리";
            // 
            // name
            // 
            name.AutoSize = true;
            name.Font = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.Point, 129);
            name.Location = new Point(84, 87);
            name.Name = "name";
            name.Size = new Size(183, 54);
            name.TabIndex = 1;
            name.Text = "립글로스";
            // 
            // pictureBox5
            // 
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(133, 31);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(250, 116);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 10;
            pictureBox5.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(392, 52);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(390, 83);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // SearchDetailInputForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(1470, 1000);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox5);
            Controls.Add(panel17);
            Controls.Add(panel5);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            Name = "SearchDetailInputForm";
            Text = "Form1";
            Load += SearchDetailInputForm_Load;
            panel5.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            panel14.ResumeLayout(false);
            panel14.PerformLayout();
            mirror.ResumeLayout(false);
            mirror.PerformLayout();
            panel18.ResumeLayout(false);
            panel18.PerformLayout();
            panel17.ResumeLayout(false);
            panel17.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel5;
        private Panel panel13;
        private Panel panel12;
        private Panel panel11;
        private Panel panel10;
        private Panel panel9;
        private Label goodsDiscountPrice;
        private Label goodsPrice;
        private Label goodsName;
        private Label brandNameKr;
        private PictureBox pictureBox2;
        private PictureBox pictureBox4;
        private Panel panel17;
        private Label name;
        private Label brand;
        private Button button1;
        private Panel panel6;
        private Label label10;
        private Panel panel14;
        private Label label12;
        private Panel mirror;
        private Label label11;
        private Panel panel18;
        private Label label6;
        private PictureBox pictureBox5;
        private PictureBox pictureBox1;
        private Panel panel1;
        private Label label1;
    }
}