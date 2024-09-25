namespace SmartMirror
{
    partial class MakeupInputForm
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
            HomeBtn = new Panel();
            pictureBox1 = new PictureBox();
            label9 = new Label();
            label1 = new Label();
            filmingBtn = new Panel();
            label5 = new Label();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            usingBtn = new Panel();
            label7 = new Label();
            pictureBox4 = new PictureBox();
            label3 = new Label();
            panel5 = new Panel();
            panel6 = new Panel();
            panel9 = new Panel();
            label8 = new Label();
            pictureBox3 = new PictureBox();
            label6 = new Label();
            label4 = new Label();
            panel4 = new Panel();
            panel7 = new Panel();
            panel8 = new Panel();
            HomeBtn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            filmingBtn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            usingBtn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            panel6.SuspendLayout();
            panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            panel7.SuspendLayout();
            panel8.SuspendLayout();
            SuspendLayout();
            // 
            // HomeBtn
            // 
            HomeBtn.BackColor = Color.FromArgb(176, 98, 193);
            HomeBtn.Controls.Add(pictureBox1);
            HomeBtn.Controls.Add(label9);
            HomeBtn.Controls.Add(label1);
            HomeBtn.Location = new Point(240, 87);
            HomeBtn.Margin = new Padding(2);
            HomeBtn.Name = "HomeBtn";
            HomeBtn.Size = new Size(179, 154);
            HomeBtn.TabIndex = 0;
            HomeBtn.Paint += panel_Paint;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.free_sticker_computer_7943655;
            pictureBox1.InitialImage = null;
            pictureBox1.Location = new Point(111, 91);
            pictureBox1.Margin = new Padding(2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(57, 55);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("맑은 고딕", 7.875F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label9.ForeColor = Color.White;
            label9.Location = new Point(1, 48);
            label9.Margin = new Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new Size(178, 19);
            label9.TabIndex = 8;
            label9.Text = "선택한 상품으로 화장 하기";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label1.ForeColor = Color.White;
            label1.Location = new Point(5, 11);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(142, 32);
            label1.TabIndex = 1;
            label1.Text = "커스텀 화장";
            // 
            // filmingBtn
            // 
            filmingBtn.BackColor = Color.FromArgb(85, 171, 217);
            filmingBtn.Controls.Add(label5);
            filmingBtn.Controls.Add(pictureBox2);
            filmingBtn.Controls.Add(label2);
            filmingBtn.Location = new Point(28, 87);
            filmingBtn.Margin = new Padding(2);
            filmingBtn.Name = "filmingBtn";
            filmingBtn.Size = new Size(179, 154);
            filmingBtn.TabIndex = 2;
            filmingBtn.Click += filmingBtn_Click;
            filmingBtn.Paint += panel_Paint;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("맑은 고딕", 7.875F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label5.ForeColor = Color.White;
            label5.Location = new Point(14, 48);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(70, 19);
            label5.TabIndex = 6;
            label5.Text = "사진 촬영";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.free_icon_photo_11943305;
            pictureBox2.Location = new Point(111, 91);
            pictureBox2.Margin = new Padding(2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(57, 55);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 5;
            pictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label2.ForeColor = Color.White;
            label2.Location = new Point(14, 11);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(118, 32);
            label2.TabIndex = 1;
            label2.Text = "다시 촬영";
            // 
            // usingBtn
            // 
            usingBtn.BackColor = Color.FromArgb(155, 234, 240);
            usingBtn.Controls.Add(label7);
            usingBtn.Controls.Add(pictureBox4);
            usingBtn.Controls.Add(label3);
            usingBtn.Location = new Point(453, 87);
            usingBtn.Margin = new Padding(2);
            usingBtn.Name = "usingBtn";
            usingBtn.Size = new Size(179, 154);
            usingBtn.TabIndex = 3;
            usingBtn.Click += usingBtn_Click;
            usingBtn.Paint += panel_Paint;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("맑은 고딕", 7.875F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label7.ForeColor = Color.White;
            label7.Location = new Point(17, 48);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(159, 19);
            label7.TabIndex = 7;
            label7.Text = "현재 사진으로 합성하기";
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources.free_icon_makeup_5732023;
            pictureBox4.Location = new Point(111, 91);
            pictureBox4.Margin = new Padding(2);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(57, 55);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.TabIndex = 6;
            pictureBox4.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label3.ForeColor = Color.White;
            label3.Location = new Point(10, 11);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(110, 32);
            label3.TabIndex = 2;
            label3.Text = "합성하기";
            // 
            // panel5
            // 
            panel5.Anchor = AnchorStyles.Top;
            panel5.BackColor = Color.FromArgb(130, 220, 40);
            panel5.Location = new Point(-2, -2);
            panel5.Margin = new Padding(2);
            panel5.Name = "panel5";
            panel5.Size = new Size(89, 512);
            panel5.TabIndex = 5;
            // 
            // panel6
            // 
            panel6.BackColor = Color.White;
            panel6.Controls.Add(panel9);
            panel6.Controls.Add(HomeBtn);
            panel6.Controls.Add(filmingBtn);
            panel6.Controls.Add(usingBtn);
            panel6.Location = new Point(88, 55);
            panel6.Margin = new Padding(2);
            panel6.Name = "panel6";
            panel6.Size = new Size(684, 445);
            panel6.TabIndex = 6;
            // 
            // panel9
            // 
            panel9.BackColor = Color.FromArgb(224, 224, 224);
            panel9.Controls.Add(label8);
            panel9.Location = new Point(47, 272);
            panel9.Margin = new Padding(2);
            panel9.Name = "panel9";
            panel9.Size = new Size(584, 131);
            panel9.TabIndex = 4;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("맑은 고딕", 15F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label8.ForeColor = Color.White;
            label8.Location = new Point(176, 52);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(233, 35);
            label8.TabIndex = 2;
            label8.Text = "여기에는 광고 넣기";
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
            label6.Size = new Size(115, 35);
            label6.TabIndex = 0;
            label6.Text = "화장하기";
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
            // MakeupInputForm
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
            Name = "MakeupInputForm";
            Text = "Form1";
            HomeBtn.ResumeLayout(false);
            HomeBtn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            filmingBtn.ResumeLayout(false);
            filmingBtn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            usingBtn.ResumeLayout(false);
            usingBtn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            panel6.ResumeLayout(false);
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            panel7.ResumeLayout(false);
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel HomeBtn;
        private Label label1;
        private Panel filmingBtn;
        private Label label2;
        private Panel usingBtn;
        private Label label3;
        private PictureBox pictureBox1;
        private Panel panel5;
        private Panel panel6;
        private Label label6;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
        private PictureBox pictureBox4;
        private Label label4;
        private Panel panel4;
        private Panel panel7;
        private Panel panel8;
        private Label label5;
        private Label label7;
        private Panel panel9;
        private Label label8;
        private Label label9;
    }
}