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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyntheticOutput));
            syntheticImg = new PictureBox();
            goodsOptionList1 = new Panel();
            goodsOptionList2 = new Panel();
            panel1 = new Panel();
            locationImg = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)syntheticImg).BeginInit();
            goodsOptionList1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)locationImg).BeginInit();
            SuspendLayout();
            // 
            // syntheticImg
            // 
            syntheticImg.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            syntheticImg.Location = new Point(189, 33);
            syntheticImg.Name = "syntheticImg";
            syntheticImg.Size = new Size(727, 726);
            syntheticImg.SizeMode = PictureBoxSizeMode.CenterImage;
            syntheticImg.TabIndex = 4;
            syntheticImg.TabStop = false;
            syntheticImg.UseWaitCursor = true;
            // 
            // goodsOptionList1
            // 
            goodsOptionList1.Anchor = AnchorStyles.None;
            goodsOptionList1.Controls.Add(goodsOptionList2);
            goodsOptionList1.Location = new Point(41, 809);
            goodsOptionList1.Name = "goodsOptionList1";
            goodsOptionList1.Size = new Size(1012, 1000);
            goodsOptionList1.TabIndex = 5;
            // 
            // goodsOptionList2
            // 
            goodsOptionList2.Anchor = AnchorStyles.None;
            goodsOptionList2.Location = new Point(0, 0);
            goodsOptionList2.Name = "goodsOptionList2";
            goodsOptionList2.Size = new Size(1012, 1000);
            goodsOptionList2.TabIndex = 6;
            goodsOptionList2.Visible = false;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(syntheticImg);
            panel1.Controls.Add(goodsOptionList1);
            panel1.Location = new Point(262, 170);
            panel1.Name = "panel1";
            panel1.Size = new Size(1093, 1849);
            panel1.TabIndex = 6;
            // 
            // locationImg
            // 
            locationImg.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            locationImg.Image = (Image)resources.GetObject("locationImg.Image");
            locationImg.Location = new Point(160, 180);
            locationImg.Name = "locationImg";
            locationImg.Size = new Size(1309, 749);
            locationImg.SizeMode = PictureBoxSizeMode.StretchImage;
            locationImg.TabIndex = 7;
            locationImg.TabStop = false;
            locationImg.Visible = false;
            // 
            // SyntheticOutput
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1600, 2122);
            Controls.Add(locationImg);
            Controls.Add(panel1);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.None;
            Name = "SyntheticOutput";
            WindowState = FormWindowState.Maximized;
            Load += SyntheticOutput_Load;
            ((System.ComponentModel.ISupportInitialize)syntheticImg).EndInit();
            goodsOptionList1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)locationImg).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox syntheticImg;
        private Panel goodsOptionList1;
        private Panel panel1;
        private Panel goodsOptionList2;
        private PictureBox locationImg;
    }
}