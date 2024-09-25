namespace SmartMirror
{
    partial class StyleInputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StyleInputForm));
            panel5 = new Panel();
            pictureBox6 = new PictureBox();
            panel6 = new Panel();
            panel9 = new Panel();
            pictureBox3 = new PictureBox();
            label6 = new Label();
            label4 = new Label();
            panel4 = new Panel();
            panel7 = new Panel();
            panel8 = new Panel();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            panel7.SuspendLayout();
            panel8.SuspendLayout();
            SuspendLayout();
            // 
            // panel5
            // 
            panel5.Anchor = AnchorStyles.Top;
            panel5.BackColor = Color.FromArgb(130, 220, 40);
            panel5.Controls.Add(pictureBox6);
            panel5.Location = new Point(-2, -2);
            panel5.Margin = new Padding(2);
            panel5.Name = "panel5";
            panel5.Size = new Size(89, 512);
            panel5.TabIndex = 5;
            // 
            // pictureBox6
            // 
            pictureBox6.Image = (Image)resources.GetObject("pictureBox6.Image");
            pictureBox6.Location = new Point(14, 14);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(61, 59);
            pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox6.TabIndex = 0;
            pictureBox6.TabStop = false;
            // 
            // panel6
            // 
            panel6.BackColor = Color.White;
            panel6.Controls.Add(panel9);
            panel6.Location = new Point(88, 55);
            panel6.Margin = new Padding(2);
            panel6.Name = "panel6";
            panel6.Size = new Size(684, 445);
            panel6.TabIndex = 6;
            // 
            // panel9
            // 
            panel9.AutoScroll = true;
            panel9.BackColor = Color.WhiteSmoke;
            panel9.Location = new Point(16, 64);
            panel9.Margin = new Padding(2);
            panel9.Name = "panel9";
            panel9.Size = new Size(647, 370);
            panel9.TabIndex = 4;
            // 
            // pictureBox3
            // 
            pictureBox3.Anchor = AnchorStyles.None;
            pictureBox3.Image = Properties.Resources.free_sticker_makeup_11601337;
            pictureBox3.Location = new Point(17, 18);
            pictureBox3.Margin = new Padding(2);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(33, 28);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 1;
            pictureBox3.TabStop = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("맑은 고딕", 15F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label6.Location = new Point(57, 14);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(399, 35);
            label6.TabIndex = 0;
            label6.Text = "메이크업 스타일을 선택해 주세요.";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.White;
            label4.Font = new Font("맑은 고딕", 12F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(255, 120, 120);
            label4.Location = new Point(612, 14);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(139, 28);
            label4.TabIndex = 2;
            label4.Text = "올브영 구미점";
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(255, 120, 120);
            panel4.Location = new Point(12, 3);
            panel4.Margin = new Padding(2);
            panel4.Name = "panel4";
            panel4.Size = new Size(12, 14);
            panel4.TabIndex = 4;
            // 
            // panel7
            // 
            panel7.Anchor = AnchorStyles.Top;
            panel7.BackColor = Color.FromArgb(130, 220, 40);
            panel7.Controls.Add(panel4);
            panel7.Location = new Point(577, 14);
            panel7.Margin = new Padding(2);
            panel7.Name = "panel7";
            panel7.Size = new Size(29, 28);
            panel7.TabIndex = 7;
            // 
            // panel8
            // 
            panel8.BackColor = Color.White;
            panel8.Controls.Add(label6);
            panel8.Controls.Add(pictureBox3);
            panel8.Location = new Point(87, 55);
            panel8.Margin = new Padding(2);
            panel8.Name = "panel8";
            panel8.Size = new Size(684, 60);
            panel8.TabIndex = 4;
            // 
            // StyleInputForm
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(771, 500);
            Controls.Add(panel8);
            Controls.Add(panel7);
            Controls.Add(label4);
            Controls.Add(panel6);
            Controls.Add(panel5);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            Name = "StyleInputForm";
            Text = "Form1";
            Load += StyleInputForm_Load;
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            panel7.ResumeLayout(false);
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel5;
        private Panel panel6;
        private Label label6;
        private PictureBox pictureBox3;
        private Label label4;
        private Panel panel4;
        private Panel panel7;
        private Panel panel8;
        private Panel panel9;
        private PictureBox pictureBox6;
    }
}