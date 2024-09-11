namespace SmartMirror
{
    partial class MainOutputForm
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainOutputForm));
            pictureBox1 = new PictureBox();
            title = new Label();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Bottom;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(485, 1438);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(676, 1120);
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
            title.Text = "메뉴를 선택해 주세요";
            title.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom;
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 30F);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(1049, 1303);
            label1.Name = "label1";
            label1.Size = new Size(365, 106);
            label1.TabIndex = 2;
            label1.Text = "화장하기";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom;
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 30F);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(261, 1303);
            label2.Name = "label2";
            label2.Size = new Size(365, 106);
            label2.TabIndex = 3;
            label2.Text = "검색하기";
            // 
            // MainOutputForm
            // 
            AutoScaleDimensions = new SizeF(192F, 192F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1600, 2560);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(title);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MainOutputForm";
            Text = "Form1";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label title;
        private Label label1;
        private Label label2;
    }
}