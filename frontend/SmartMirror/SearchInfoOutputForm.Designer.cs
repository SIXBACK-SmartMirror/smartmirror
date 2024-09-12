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
            panel2 = new Panel();
            label1 = new Label();
            pictureBox2 = new PictureBox();
            panelProducts = new Panel();
            label2 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panelProducts.SuspendLayout();
            SuspendLayout();
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
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox2);
            panel1.Location = new Point(145, 218);
            panel1.Name = "panel1";
            panel1.Size = new Size(1237, 147);
            panel1.TabIndex = 2;
            panel1.Paint += panel1_Paint;
            // 
            // panel2
            // 
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(400, 200);
            panel2.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 17F);
            label1.Location = new Point(100, 44);
            label1.Name = "label1";
            label1.Size = new Size(0, 62);
            label1.TabIndex = 3;
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
            // panelProducts
            // 
            panelProducts.BackColor = Color.White;
            panelProducts.Controls.Add(label2);
            panelProducts.Dock = DockStyle.Bottom;
            panelProducts.Location = new Point(0, 503);
            panelProducts.Name = "panelProducts";
            panelProducts.Size = new Size(1600, 2057);
            panelProducts.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 10F);
            label2.ForeColor = SystemColors.ControlText;
            label2.Location = new Point(31, 20);
            label2.Name = "label2";
            label2.Size = new Size(95, 37);
            label2.TabIndex = 0;
            label2.Text = "총 0개";
            // 
            // SearchInfoOutputForm
            // 
            AutoScaleDimensions = new SizeF(192F, 192F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1600, 2560);
            Controls.Add(panelProducts);
            Controls.Add(panel1);
            Controls.Add(title);
            FormBorderStyle = FormBorderStyle.None;
            Name = "SearchInfoOutputForm";
            Text = "Form1";
            WindowState = FormWindowState.Maximized;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panelProducts.ResumeLayout(false);
            panelProducts.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label title;
        private Panel panel1;
        private PictureBox pictureBox2;
        private Label label1;
        private Panel panel2;
        private Panel panelProducts;
        private Label label2;
    }
}