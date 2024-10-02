namespace SmartMirror
{
    partial class MainInputForm
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
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainInputForm));
            mirror = new Panel();
            pictureBox1 = new PictureBox();
            label9 = new Label();
            label1 = new Label();
            search = new Panel();
            label5 = new Label();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            makeup = new Panel();
            label7 = new Label();
            pictureBox4 = new PictureBox();
            label3 = new Label();
            panel5 = new Panel();
            panel8 = new Panel();
            pictureBox3 = new PictureBox();
            label6 = new Label();
            panel6 = new Panel();
            panel9 = new Panel();
            label8 = new Label();
            label4 = new Label();
            panel4 = new Panel();
            panel7 = new Panel();
            mirror.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            search.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            makeup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            panel5.SuspendLayout();
            panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            panel6.SuspendLayout();
            panel9.SuspendLayout();
            panel7.SuspendLayout();
            SuspendLayout();
            // 
            // mirror
            // 
            mirror.BackColor = Color.White;
            mirror.Controls.Add(pictureBox1);
            mirror.Controls.Add(label9);
            mirror.Controls.Add(label1);
            mirror.Location = new Point(59, 75);
            mirror.Margin = new Padding(2);
            mirror.Name = "mirror";
            mirror.Size = new Size(364, 330);
            mirror.TabIndex = 0;
            mirror.Click += panel1_Click;
            mirror.Paint += panel_Paint;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(254, 220);
            pictureBox1.Margin = new Padding(2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(69, 69);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("맑은 고딕", 13F, FontStyle.Bold);
            label9.ForeColor = Color.Black;
            label9.Location = new Point(30, 80);
            label9.Margin = new Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new Size(128, 36);
            label9.TabIndex = 8;
            label9.Text = "화면 끄기";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 20F, FontStyle.Bold);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(17, 14);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(180, 54);
            label1.TabIndex = 1;
            label1.Text = "거울 ON";
            // 
            // search
            // 
            search.BackColor = Color.FromArgb(130, 220, 40);
            search.Controls.Add(label5);
            search.Controls.Add(pictureBox2);
            search.Controls.Add(label2);
            search.Location = new Point(490, 75);
            search.Margin = new Padding(2);
            search.Name = "search";
            search.Size = new Size(364, 330);
            search.TabIndex = 2;
            search.Click += panel2_Click;
            search.Paint += panel_Paint;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("맑은 고딕", 13F, FontStyle.Bold);
            label5.ForeColor = Color.White;
            label5.Location = new Point(30, 80);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(283, 36);
            label5.TabIndex = 6;
            label5.Text = "상품, 브랜드 이름 검색";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(254, 220);
            pictureBox2.Margin = new Padding(2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(69, 69);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 5;
            pictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 20F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Location = new Point(17, 14);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(197, 54);
            label2.TabIndex = 1;
            label2.Text = "상품 찾기";
            // 
            // makeup
            // 
            makeup.BackColor = Color.White;
            makeup.Controls.Add(label7);
            makeup.Controls.Add(pictureBox4);
            makeup.Controls.Add(label3);
            makeup.Location = new Point(924, 75);
            makeup.Margin = new Padding(2);
            makeup.Name = "makeup";
            makeup.Size = new Size(364, 330);
            makeup.TabIndex = 3;
            makeup.Click += makeup_Click;
            makeup.Paint += panel_Paint;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("맑은 고딕", 13F, FontStyle.Bold);
            label7.ForeColor = Color.Black;
            label7.Location = new Point(30, 80);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(189, 36);
            label7.TabIndex = 7;
            label7.Text = "AI로 화장 체험";
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(254, 220);
            pictureBox4.Margin = new Padding(2);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(69, 69);
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.TabIndex = 6;
            pictureBox4.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 20F, FontStyle.Bold);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(17, 14);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(197, 54);
            label3.TabIndex = 2;
            label3.Text = "가상 화장";
            // 
            // panel5
            // 
            panel5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel5.BackColor = Color.FromArgb(130, 220, 40);
            panel5.Controls.Add(panel8);
            panel5.Location = new Point(-2, -2);
            panel5.Margin = new Padding(2);
            panel5.Name = "panel5";
            panel5.Size = new Size(99, 1015);
            panel5.TabIndex = 5;
            // 
            // panel8
            // 
            panel8.BackColor = Color.White;
            panel8.Controls.Add(pictureBox3);
            panel8.Controls.Add(label6);
            panel8.Location = new Point(2, 84);
            panel8.Margin = new Padding(2);
            panel8.Name = "panel8";
            panel8.Size = new Size(100, 100);
            panel8.TabIndex = 4;
            // 
            // pictureBox3
            // 
            pictureBox3.Anchor = AnchorStyles.Left;
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(13, 41);
            pictureBox3.Margin = new Padding(2);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(20, 20);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 1;
            pictureBox3.TabStop = false;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Font = new Font("맑은 고딕", 10F, FontStyle.Bold);
            label6.Location = new Point(33, 37);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(52, 28);
            label6.TabIndex = 0;
            label6.Text = "메뉴";
            // 
            // panel6
            // 
            panel6.Anchor = AnchorStyles.None;
            panel6.BackColor = Color.FromArgb(248, 248, 248);
            panel6.Controls.Add(panel9);
            panel6.Controls.Add(mirror);
            panel6.Controls.Add(search);
            panel6.Controls.Add(makeup);
            panel6.Location = new Point(100, 148);
            panel6.Margin = new Padding(2);
            panel6.Name = "panel6";
            panel6.Size = new Size(1373, 848);
            panel6.TabIndex = 6;
            // 
            // panel9
            // 
            panel9.BackColor = Color.FromArgb(224, 224, 224);
            panel9.Controls.Add(label8);
            panel9.Location = new Point(59, 476);
            panel9.Margin = new Padding(2);
            panel9.Name = "panel9";
            panel9.Size = new Size(1230, 286);
            panel9.TabIndex = 4;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("맑은 고딕", 15F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label8.ForeColor = Color.White;
            label8.Location = new Point(497, 131);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(280, 41);
            label8.TabIndex = 2;
            label8.Text = "여기에는 광고 넣기";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.BackColor = Color.White;
            label4.Font = new Font("맑은 고딕", 15F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(255, 120, 120);
            label4.Location = new Point(1250, 18);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(209, 41);
            label4.TabIndex = 2;
            label4.Text = "올브영 구미점";
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(255, 120, 120);
            panel4.Location = new Point(13, 4);
            panel4.Margin = new Padding(2);
            panel4.Name = "panel4";
            panel4.Size = new Size(13, 18);
            panel4.TabIndex = 4;
            // 
            // panel7
            // 
            panel7.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panel7.BackColor = Color.FromArgb(130, 220, 40);
            panel7.Controls.Add(panel4);
            panel7.Location = new Point(1217, 21);
            panel7.Margin = new Padding(2);
            panel7.Name = "panel7";
            panel7.Size = new Size(32, 35);
            panel7.TabIndex = 7;
            // 
            // MainInputForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(1470, 1000);
            Controls.Add(panel7);
            Controls.Add(label4);
            Controls.Add(panel6);
            Controls.Add(panel5);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            Name = "MainInputForm";
            Text = "Form1";
            mirror.ResumeLayout(false);
            mirror.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            search.ResumeLayout(false);
            search.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            makeup.ResumeLayout(false);
            makeup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            panel5.ResumeLayout(false);
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            panel6.ResumeLayout(false);
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
            panel7.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel mirror;
        private Label label1;
        private Panel search;
        private Label label2;
        private Panel makeup;
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