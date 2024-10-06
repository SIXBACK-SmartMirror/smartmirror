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
            button1 = new Button();
            panel3 = new Panel();
            img = new PictureBox();
            panel8 = new Panel();
            comboBox1 = new ComboBox();
            discountPrice = new Label();
            price = new Label();
            name = new Label();
            brand = new Label();
            panel1 = new Panel();
            optionPrice = new Label();
            optionStock = new Label();
            optionName = new Label();
            optionImg = new PictureBox();
            panel5.SuspendLayout();
            panel6.SuspendLayout();
            panel14.SuspendLayout();
            mirror.SuspendLayout();
            panel18.SuspendLayout();
            panel17.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)img).BeginInit();
            panel8.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)optionImg).BeginInit();
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
            panel17.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel17.Controls.Add(button1);
            panel17.Controls.Add(panel3);
            panel17.Controls.Add(panel1);
            panel17.Location = new Point(130, 113);
            panel17.Name = "panel17";
            panel17.Size = new Size(1311, 684);
            panel17.TabIndex = 4;
            // 
            // button1
            // 
            button1.Font = new Font("맑은 고딕", 14F, FontStyle.Regular, GraphicsUnit.Point, 129);
            button1.Location = new Point(978, 489);
            button1.Name = "button1";
            button1.Size = new Size(291, 131);
            button1.TabIndex = 4;
            button1.Text = "위치 끄기";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            panel3.Controls.Add(img);
            panel3.Controls.Add(panel8);
            panel3.Location = new Point(32, 18);
            panel3.Name = "panel3";
            panel3.Size = new Size(1261, 452);
            panel3.TabIndex = 3;
            // 
            // img
            // 
            img.Location = new Point(20, 20);
            img.Name = "img";
            img.Size = new Size(345, 406);
            img.SizeMode = PictureBoxSizeMode.StretchImage;
            img.TabIndex = 0;
            img.TabStop = false;
            // 
            // panel8
            // 
            panel8.Controls.Add(comboBox1);
            panel8.Controls.Add(discountPrice);
            panel8.Controls.Add(price);
            panel8.Controls.Add(name);
            panel8.Controls.Add(brand);
            panel8.Location = new Point(392, 20);
            panel8.Name = "panel8";
            panel8.Size = new Size(845, 406);
            panel8.TabIndex = 1;
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("맑은 고딕", 20F);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(30, 312);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(484, 62);
            comboBox1.TabIndex = 4;
            comboBox1.Text = "상품을 선택해주세요";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // discountPrice
            // 
            discountPrice.AutoSize = true;
            discountPrice.Font = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.Point, 129);
            discountPrice.ForeColor = Color.FromArgb(236, 0, 6);
            discountPrice.Location = new Point(204, 146);
            discountPrice.Name = "discountPrice";
            discountPrice.Size = new Size(217, 54);
            discountPrice.TabIndex = 3;
            discountPrice.Text = "18,000원~";
            // 
            // price
            // 
            price.AutoSize = true;
            price.Font = new Font("맑은 고딕", 15F, FontStyle.Strikeout, GraphicsUnit.Point, 129);
            price.ForeColor = Color.Gray;
            price.Location = new Point(30, 158);
            price.Name = "price";
            price.Size = new Size(161, 41);
            price.TabIndex = 2;
            price.Text = "10,000원~";
            // 
            // name
            // 
            name.AutoSize = true;
            name.Font = new Font("맑은 고딕", 20F, FontStyle.Bold, GraphicsUnit.Point, 129);
            name.Location = new Point(30, 66);
            name.Name = "name";
            name.Size = new Size(183, 54);
            name.TabIndex = 1;
            name.Text = "립글로스";
            // 
            // brand
            // 
            brand.AutoSize = true;
            brand.Font = new Font("맑은 고딕", 15F, FontStyle.Regular, GraphicsUnit.Point, 129);
            brand.Location = new Point(30, 19);
            brand.Name = "brand";
            brand.Size = new Size(168, 41);
            brand.TabIndex = 0;
            brand.Text = "이니스프리";
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(optionPrice);
            panel1.Controls.Add(optionStock);
            panel1.Controls.Add(optionName);
            panel1.Controls.Add(optionImg);
            panel1.Location = new Point(52, 489);
            panel1.Name = "panel1";
            panel1.Size = new Size(892, 131);
            panel1.TabIndex = 2;
            // 
            // optionPrice
            // 
            optionPrice.AutoSize = true;
            optionPrice.Font = new Font("맑은 고딕", 15F, FontStyle.Bold);
            optionPrice.Location = new Point(706, 42);
            optionPrice.Name = "optionPrice";
            optionPrice.Size = new Size(141, 41);
            optionPrice.TabIndex = 6;
            optionPrice.Text = "18,000원";
            // 
            // optionStock
            // 
            optionStock.AutoSize = true;
            optionStock.Font = new Font("맑은 고딕", 15F, FontStyle.Bold);
            optionStock.ForeColor = Color.FromArgb(130, 220, 40);
            optionStock.Location = new Point(459, 46);
            optionStock.Name = "optionStock";
            optionStock.Size = new Size(65, 41);
            optionStock.TabIndex = 5;
            optionStock.Text = "3개";
            // 
            // optionName
            // 
            optionName.AutoSize = true;
            optionName.Font = new Font("맑은 고딕", 15F, FontStyle.Bold, GraphicsUnit.Point, 129);
            optionName.Location = new Point(135, 46);
            optionName.Name = "optionName";
            optionName.Size = new Size(179, 41);
            optionName.TabIndex = 4;
            optionName.Text = "클래식 레드";
            // 
            // optionImg
            // 
            optionImg.Location = new Point(28, 26);
            optionImg.Name = "optionImg";
            optionImg.Size = new Size(80, 80);
            optionImg.SizeMode = PictureBoxSizeMode.StretchImage;
            optionImg.TabIndex = 3;
            optionImg.TabStop = false;
            // 
            // SearchDetailInputForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(1470, 1000);
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
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)img).EndInit();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)optionImg).EndInit();
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
        private Panel panel3;
        private PictureBox img;
        private Panel panel8;
        private ComboBox comboBox1;
        private Label discountPrice;
        private Label price;
        private Label name;
        private Label brand;
        private Panel panel1;
        private Label optionPrice;
        private Label optionStock;
        private Label optionName;
        private PictureBox optionImg;
        private Button button1;
        private Panel panel6;
        private Label label10;
        private Panel panel14;
        private Label label12;
        private Panel mirror;
        private Label label11;
        private Panel panel18;
        private Label label6;
    }
}