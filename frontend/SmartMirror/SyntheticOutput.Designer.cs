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
            goodsOptionList = new Panel();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)syntheticImg).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // syntheticImg
            // 
            syntheticImg.Anchor = AnchorStyles.None;
            syntheticImg.Location = new Point(29, 88);
            syntheticImg.Name = "syntheticImg";
            syntheticImg.Size = new Size(1000, 794);
            syntheticImg.SizeMode = PictureBoxSizeMode.AutoSize;
            syntheticImg.TabIndex = 4;
            syntheticImg.TabStop = false;
            syntheticImg.UseWaitCursor = true;
            // 
            // goodsOptionList
            // 
            goodsOptionList.Anchor = AnchorStyles.None;
            goodsOptionList.Location = new Point(29, 957);
            goodsOptionList.Name = "goodsOptionList";
            goodsOptionList.Size = new Size(1000, 272);
            goodsOptionList.TabIndex = 5;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(syntheticImg);
            panel1.Controls.Add(goodsOptionList);
            panel1.Location = new Point(263, 293);
            panel1.Name = "panel1";
            panel1.Size = new Size(1093, 1292);
            panel1.TabIndex = 6;
            // 
            // SyntheticOutput
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1600, 2122);
            Controls.Add(panel1);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.None;
            Name = "SyntheticOutput";
            WindowState = FormWindowState.Maximized;
            Load += SyntheticOutput_Load;
            ((System.ComponentModel.ISupportInitialize)syntheticImg).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox syntheticImg;
        private Panel goodsOptionList;
        private Panel panel1;
    }
}