﻿namespace SmartMirror
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
            label1 = new Label();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
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
            title.Text = "필요한 상품을 검색해 주세요";
            title.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top;
            panel1.BackColor = SystemColors.ControlLightLight;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox2);
            panel1.Location = new Point(145, 218);
            panel1.Name = "panel1";
            panel1.Size = new Size(1237, 147);
            panel1.TabIndex = 2;
            panel1.Paint += panel1_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 17F);
            label1.Location = new Point(100, 44);
            label1.Name = "label1";
            label1.Size = new Size(391, 62);
            label1.TabIndex = 3;
            label1.Text = "상품, 브랜드 검색";
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
            // SearchOutputForm
            // 
            AutoScaleDimensions = new SizeF(192F, 192F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1600, 2560);
            Controls.Add(panel1);
            Controls.Add(title);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "SearchOutputForm";
            Text = "Form1";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Label title;
        private Panel panel1;
        private PictureBox pictureBox2;
        private Label label1;
    }
}