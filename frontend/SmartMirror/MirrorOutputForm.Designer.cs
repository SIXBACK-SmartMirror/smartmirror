namespace SmartMirror
{
    partial class MirrorOutputForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MirrorOutputForm));
            pictureBox1 = new PictureBox();
            title = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Bottom;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(476, 1367);
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
            title.Text = "찾으시는 제품 있으면 말씀해 주세요";
            title.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MirrorOutputForm
            // 
            AutoScaleDimensions = new SizeF(192F, 192F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1574, 2489);
            Controls.Add(title);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MirrorOutputForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Form1";
            WindowState = FormWindowState.Maximized;
            Load += MirrorOutputForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Label title;
    }
}
