namespace SmartMirror
{
    partial class SearchInfoOutputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchInfoOutputForm));
            title = new Label();
            panel1 = new Panel();
            total = new Label();
            label1 = new Label();
            pictureBox2 = new PictureBox();
            panel6 = new Panel();
            label7 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // title
            // 
            title.Anchor = AnchorStyles.Top;
            title.Font = new Font("맑은 고딕", 30F);
            title.ForeColor = SystemColors.ControlLightLight;
            title.Location = new Point(-2, 7);
            title.Margin = new Padding(2, 0, 2, 0);
            title.Name = "title";
            title.Size = new Size(1182, 102);
            title.TabIndex = 1;
            title.Text = "필요한 상품을 선택해 주세요";
            title.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top;
            panel1.BackColor = SystemColors.ControlLightLight;
            panel1.Controls.Add(total);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox2);
            panel1.Location = new Point(109, 164);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(928, 110);
            panel1.TabIndex = 2;
            panel1.Paint += panel1_Paint;
            // 
            // total
            // 
            total.Anchor = AnchorStyles.Left;
            total.AutoSize = true;
            total.Font = new Font("맑은 고딕", 18F, FontStyle.Regular, GraphicsUnit.Point, 129);
            total.Location = new Point(53, 31);
            total.Margin = new Padding(2, 0, 2, 0);
            total.Name = "total";
            total.Size = new Size(92, 48);
            total.TabIndex = 9;
            total.Text = "롬앤";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 17F);
            label1.Location = new Point(75, 33);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(0, 46);
            label1.TabIndex = 3;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(835, 19);
            pictureBox2.Margin = new Padding(2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(68, 68);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            // 
            // panel6
            // 
            panel6.Anchor = AnchorStyles.Top;
            panel6.AutoScroll = true;
            panel6.BackColor = Color.Black;
            panel6.Location = new Point(-100, 553);
            panel6.Margin = new Padding(2);
            panel6.Name = "panel6";
            panel6.Size = new Size(1501, 1603);
            panel6.TabIndex = 7;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label7.ForeColor = Color.White;
            label7.Location = new Point(136, 487);
            label7.Name = "label7";
            label7.Size = new Size(38, 32);
            label7.TabIndex = 8;
            label7.Text = "총";
            // 
            // SearchInfoOutputForm
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1200, 1920);
            Controls.Add(label7);
            Controls.Add(panel6);
            Controls.Add(panel1);
            Controls.Add(title);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            Name = "SearchInfoOutputForm";
            Text = "Form1";
            WindowState = FormWindowState.Maximized;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label title;
        private Panel panel1;
        private PictureBox pictureBox2;
        private Label label1;
        public Panel panel6;
        public Label label7;
        public Label total;
    }
}