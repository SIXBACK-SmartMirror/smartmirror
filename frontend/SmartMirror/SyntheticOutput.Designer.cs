namespace SmartMirror
{
    partial class SyntheticOutput
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
            syntheticImg = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)syntheticImg).BeginInit();
            SuspendLayout();
            // 
            // syntheticImg
            // 
            syntheticImg.Anchor = AnchorStyles.None;
            syntheticImg.Location = new Point(292, 341);
            syntheticImg.Name = "syntheticImg";
            syntheticImg.Size = new Size(1000, 794);
            syntheticImg.SizeMode = PictureBoxSizeMode.AutoSize;
            syntheticImg.TabIndex = 4;
            syntheticImg.TabStop = false;
            syntheticImg.UseWaitCursor = true;
            // 
            // SyntheticOutput
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1600, 2122);
            Controls.Add(syntheticImg);
            ForeColor = Color.White;
            Name = "SyntheticOutput";
            Load += SyntheticOutput_Load;
            ((System.ComponentModel.ISupportInitialize)syntheticImg).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox syntheticImg;
    }
}