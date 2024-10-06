namespace SmartMirror
{
    partial class MakeupOutputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MakeupOutputForm));
            streamingBox = new PictureBox();
            pictureBox1 = new PictureBox();
            topComent = new Label();
            captureImg = new PictureBox();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)streamingBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)captureImg).BeginInit();
            SuspendLayout();
            // 
            // streamingBox
            // 
            streamingBox.Anchor = AnchorStyles.None;
            streamingBox.Location = new Point(340, 355);
            streamingBox.Margin = new Padding(3, 4, 3, 4);
            streamingBox.Name = "streamingBox";
            streamingBox.Size = new Size(1111, 992);
            streamingBox.TabIndex = 0;
            streamingBox.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.None;
            pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(266, 1593);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(341, 511);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // topComent
            // 
            topComent.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            topComent.Font = new Font("맑은 고딕", 30F);
            topComent.ForeColor = SystemColors.ButtonHighlight;
            topComent.Location = new Point(311, 11);
            topComent.Name = "topComent";
            topComent.Size = new Size(1182, 102);
            topComent.TabIndex = 2;
            topComent.Text = "카메라를 봐주세요";
            topComent.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // captureImg
            // 
            captureImg.Anchor = AnchorStyles.None;
            captureImg.Location = new Point(340, 355);
            captureImg.Margin = new Padding(3, 4, 3, 4);
            captureImg.Name = "captureImg";
            captureImg.Size = new Size(1111, 992);
            captureImg.TabIndex = 3;
            captureImg.TabStop = false;
            captureImg.Visible = false;
            // 
            // panel1
            // 
            panel1.Location = new Point(167, 187);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(292, 133);
            panel1.TabIndex = 5;
            panel1.Visible = false;
            // 
            // MakeupOutputForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1778, 2530);
            Controls.Add(panel1);
            Controls.Add(captureImg);
            Controls.Add(topComent);
            Controls.Add(streamingBox);
            Controls.Add(pictureBox1);
            ForeColor = Color.White;
            Margin = new Padding(3, 4, 3, 4);
            Name = "MakeupOutputForm";
            WindowState = FormWindowState.Maximized;
            Load += MakeupOutputForm_Load;
            ((System.ComponentModel.ISupportInitialize)streamingBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)captureImg).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox streamingBox;
        private PictureBox pictureBox1;
        public Label topComent;
        private PictureBox captureImg;
        public Panel panel1;
    }
}