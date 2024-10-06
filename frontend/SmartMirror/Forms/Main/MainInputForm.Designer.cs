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
            search = new Panel();
            label5 = new Label();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            makeup = new Panel();
            label7 = new Label();
            pictureBox4 = new PictureBox();
            label3 = new Label();
            panel5 = new Panel();
            panel6 = new Panel();
            panel2 = new Panel();
            pictureBox7 = new PictureBox();
            pictureBox6 = new PictureBox();
            custom = new Panel();
            label10 = new Label();
            pictureBox5 = new PictureBox();
            label11 = new Label();
            mirror = new Panel();
            pictureBox1 = new PictureBox();
            label9 = new Label();
            label1 = new Label();
            search.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            makeup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            panel6.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            custom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            mirror.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // search
            // 
            search.BackColor = Color.FromArgb(140, 217, 77);
            search.Controls.Add(label5);
            search.Controls.Add(pictureBox2);
            search.Controls.Add(label2);
            search.Location = new Point(571, 46);
            search.Margin = new Padding(2);
            search.Name = "search";
            search.Size = new Size(471, 426);
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
            label5.Click += panel2_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(334, 279);
            pictureBox2.Margin = new Padding(2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(100, 100);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 5;
            pictureBox2.TabStop = false;
            pictureBox2.Click += panel2_Click;
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
            label2.Click += panel2_Click;
            // 
            // makeup
            // 
            makeup.BackColor = Color.FromArgb(255, 158, 158);
            makeup.Controls.Add(label7);
            makeup.Controls.Add(pictureBox4);
            makeup.Controls.Add(label3);
            makeup.Location = new Point(59, 533);
            makeup.Margin = new Padding(2);
            makeup.Name = "makeup";
            makeup.Size = new Size(471, 426);
            makeup.TabIndex = 3;
            makeup.Click += makeup_Click;
            makeup.Paint += panel_Paint;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("맑은 고딕", 13F, FontStyle.Bold);
            label7.ForeColor = Color.White;
            label7.Location = new Point(30, 80);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(276, 36);
            label7.TabIndex = 7;
            label7.Text = "정해진 화장 적용 기능";
            label7.Click += makeup_Click;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(334, 284);
            pictureBox4.Margin = new Padding(2);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(100, 100);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 6;
            pictureBox4.TabStop = false;
            pictureBox4.Click += makeup_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 20F, FontStyle.Bold);
            label3.ForeColor = Color.White;
            label3.Location = new Point(17, 14);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(237, 54);
            label3.TabIndex = 2;
            label3.Text = "원클릭 화장";
            label3.Click += makeup_Click;
            // 
            // panel5
            // 
            panel5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel5.BackColor = Color.FromArgb(115, 210, 44);
            panel5.Location = new Point(-2, -2);
            panel5.Margin = new Padding(2);
            panel5.Name = "panel5";
            panel5.Size = new Size(99, 1015);
            panel5.TabIndex = 5;
            // 
            // panel6
            // 
            panel6.Anchor = AnchorStyles.None;
            panel6.BackColor = Color.White;
            panel6.Controls.Add(panel2);
            panel6.Controls.Add(custom);
            panel6.Controls.Add(mirror);
            panel6.Controls.Add(search);
            panel6.Controls.Add(makeup);
            panel6.Location = new Point(101, -2);
            panel6.Margin = new Padding(2);
            panel6.Name = "panel6";
            panel6.Size = new Size(1373, 1002);
            panel6.TabIndex = 6;
            // 
            // panel2
            // 
            panel2.Controls.Add(pictureBox7);
            panel2.Controls.Add(pictureBox6);
            panel2.Location = new Point(1062, 48);
            panel2.Name = "panel2";
            panel2.Size = new Size(271, 911);
            panel2.TabIndex = 8;
            // 
            // pictureBox7
            // 
            pictureBox7.Image = (Image)resources.GetObject("pictureBox7.Image");
            pictureBox7.Location = new Point(0, 0);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(265, 911);
            pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox7.TabIndex = 8;
            pictureBox7.TabStop = false;
            pictureBox7.Visible = false;
            // 
            // pictureBox6
            // 
            pictureBox6.Image = (Image)resources.GetObject("pictureBox6.Image");
            pictureBox6.Location = new Point(12, 3);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(253, 908);
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.TabIndex = 0;
            pictureBox6.TabStop = false;
            // 
            // custom
            // 
            custom.BackColor = Color.FromArgb(134, 147, 250);
            custom.Controls.Add(label10);
            custom.Controls.Add(pictureBox5);
            custom.Controls.Add(label11);
            custom.Location = new Point(571, 533);
            custom.Margin = new Padding(2);
            custom.Name = "custom";
            custom.Size = new Size(471, 426);
            custom.TabIndex = 8;
            custom.Click += makeup_Click;
            custom.Paint += panel_Paint;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("맑은 고딕", 13F, FontStyle.Bold);
            label10.ForeColor = Color.White;
            label10.Location = new Point(30, 80);
            label10.Margin = new Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new Size(302, 36);
            label10.TabIndex = 7;
            label10.Text = "직접 선택하는 화장 기능";
            // 
            // pictureBox5
            // 
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(334, 284);
            pictureBox5.Margin = new Padding(2);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(100, 100);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 6;
            pictureBox5.TabStop = false;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("맑은 고딕", 20F, FontStyle.Bold);
            label11.ForeColor = Color.White;
            label11.Location = new Point(17, 14);
            label11.Margin = new Padding(2, 0, 2, 0);
            label11.Name = "label11";
            label11.Size = new Size(237, 54);
            label11.TabIndex = 2;
            label11.Text = "커스텀 화장";
            // 
            // mirror
            // 
            mirror.BackColor = Color.FromArgb(224, 224, 224);
            mirror.Controls.Add(pictureBox1);
            mirror.Controls.Add(label9);
            mirror.Controls.Add(label1);
            mirror.ForeColor = SystemColors.ControlText;
            mirror.Location = new Point(59, 46);
            mirror.Margin = new Padding(2);
            mirror.Name = "mirror";
            mirror.Size = new Size(471, 426);
            mirror.TabIndex = 0;
            mirror.Click += panel1_Click;
            mirror.Paint += panel_Paint;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(160, 69);
            pictureBox1.Margin = new Padding(2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(130, 130);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            pictureBox1.Click += panel1_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("맑은 고딕", 13F, FontStyle.Bold);
            label9.ForeColor = Color.White;
            label9.Location = new Point(160, 316);
            label9.Margin = new Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new Size(128, 36);
            label9.TabIndex = 8;
            label9.Text = "화면 켜기";
            label9.Click += panel1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 20F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(131, 243);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(190, 54);
            label1.TabIndex = 1;
            label1.Text = "거울 OFF";
            label1.Click += panel1_Click;
            // 
            // MainInputForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(1470, 1000);
            Controls.Add(panel6);
            Controls.Add(panel5);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(2);
            Name = "MainInputForm";
            Text = "Form1";
            search.ResumeLayout(false);
            search.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            makeup.ResumeLayout(false);
            makeup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            panel6.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            custom.ResumeLayout(false);
            custom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            mirror.ResumeLayout(false);
            mirror.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel search;
        private Label label2;
        private Panel makeup;
        private Label label3;
        private Panel panel5;
        private Panel panel6;
        private PictureBox pictureBox2;
        private PictureBox pictureBox4;
        private Label label5;
        private Label label7;
        private Panel custom;
        private Label label10;
        private PictureBox pictureBox5;
        private Label label11;
        private Panel mirror;
        private PictureBox pictureBox1;
        private Label label9;
        private Label label1;
        private PictureBox pictureBox7;
        private PictureBox pictureBox6;
        private Panel panel2;
    }
}