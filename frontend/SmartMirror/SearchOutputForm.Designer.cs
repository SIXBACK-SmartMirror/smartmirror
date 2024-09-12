namespace SmartMirror
{
    partial class SearchOutputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchOutputForm));
            pictureBox1 = new PictureBox();
            title = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            label1 = new Label();
            textBox1 = new TextBox();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Bottom;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(339, 1438);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(949, 1120);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // title
            // 
            title.Anchor = AnchorStyles.Top;
            title.Font = new Font("맑은 고딕", 30F);
            title.ForeColor = SystemColors.ControlLightLight;
            title.Location = new Point(-3, 9);
            title.Name = "title";
            title.Size = new Size(1576, 136);
            title.TabIndex = 1;
            title.Text = "필요한 상품을 검색해 주세요";
            title.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top;
            panel1.BackColor = SystemColors.ControlLightLight;
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(pictureBox2);
            panel1.Location = new Point(187, 365);
            panel1.Name = "panel1";
            panel1.Size = new Size(1237, 147);
            panel1.TabIndex = 2;
            panel1.Paint += panel1_Paint;
            // 
            // panel2
            // 
            panel2.Controls.Add(label1);
            panel2.Controls.Add(textBox1);
            panel2.Location = new Point(23, 14);
            panel2.Name = "panel2";
            panel2.Size = new Size(1072, 120);
            panel2.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 17F);
            label1.Location = new Point(29, 30);
            label1.Name = "label1";
            label1.Size = new Size(391, 62);
            label1.TabIndex = 3;
            label1.Text = "상품, 브랜드 검색";
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.White;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("맑은 고딕", 17F);
            textBox1.ForeColor = Color.White;
            textBox1.Location = new Point(28, 32);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(1039, 61);
            textBox1.TabIndex = 3;
            textBox1.TextChanged += textBox1_TextChanged_1;
            textBox1.KeyDown += textBox1_KeyDown;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(1113, 25);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(90, 91);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.BackColor = Color.MediumSeaGreen;
            label2.Font = new Font("맑은 고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label2.ForeColor = Color.White;
            label2.Location = new Point(859, 284);
            label2.Name = "label2";
            label2.Size = new Size(619, 45);
            label2.TabIndex = 3;
            label2.Text = "키보드 검색시 Enter를 누르면 검색됩니다";
            // 
            // SearchOutputForm
            // 
            AutoScaleDimensions = new SizeF(192F, 192F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            BackColor = Color.Black;
            ClientSize = new Size(1600, 2560);
            Controls.Add(label2);
            Controls.Add(panel1);
            Controls.Add(title);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "SearchOutputForm";
            Text = " ";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label title;
        private Panel panel1;
        private PictureBox pictureBox2;
        private Label label1;
        public TextBox textBox1;
        private Panel panel2;
        private Label label2;
    }
}