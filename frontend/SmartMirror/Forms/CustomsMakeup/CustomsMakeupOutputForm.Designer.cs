﻿namespace SmartMirror
{
    partial class CustomsMakeupOutputForm
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
            goodsOptionList1 = new Panel();
            panel1 = new Panel();
            QRpicture = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)syntheticImg).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)QRpicture).BeginInit();
            SuspendLayout();
            // 
            // syntheticImg
            // 
            syntheticImg.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            syntheticImg.Location = new Point(210, 41);
            syntheticImg.Margin = new Padding(3, 4, 3, 4);
            syntheticImg.Name = "syntheticImg";
            syntheticImg.Size = new Size(808, 908);
            syntheticImg.SizeMode = PictureBoxSizeMode.CenterImage;
            syntheticImg.TabIndex = 4;
            syntheticImg.TabStop = false;
            syntheticImg.UseWaitCursor = true;
            // 
            // goodsOptionList1
            // 
            goodsOptionList1.Anchor = AnchorStyles.None;
            goodsOptionList1.Location = new Point(46, 1011);
            goodsOptionList1.Margin = new Padding(3, 4, 3, 4);
            goodsOptionList1.Name = "goodsOptionList1";
            goodsOptionList1.Size = new Size(1124, 1250);
            goodsOptionList1.TabIndex = 5;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(QRpicture);
            panel1.Controls.Add(syntheticImg);
            panel1.Controls.Add(goodsOptionList1);
            panel1.Location = new Point(291, 212);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1214, 2311);
            panel1.TabIndex = 6;
            // 
            // QRpicture
            // 
            QRpicture.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            QRpicture.Location = new Point(210, 41);
            QRpicture.Margin = new Padding(3, 4, 3, 4);
            QRpicture.Name = "QRpicture";
            QRpicture.Size = new Size(808, 908);
            QRpicture.SizeMode = PictureBoxSizeMode.CenterImage;
            QRpicture.TabIndex = 6;
            QRpicture.TabStop = false;
            QRpicture.UseWaitCursor = true;
            QRpicture.Visible = false;
            // 
            // CustomsMakeupOutputForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1778, 2652);
            Controls.Add(panel1);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "CustomsMakeupOutputForm";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)syntheticImg).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)QRpicture).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public PictureBox syntheticImg;
        private Panel goodsOptionList1;
        private Panel panel1;
        public PictureBox QRpicture;
    }
}