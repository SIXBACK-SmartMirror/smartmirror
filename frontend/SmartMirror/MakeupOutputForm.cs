﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;


namespace SmartMirror
{
    public partial class MakeupOutputForm : Form
    {
        VideoCapture _capture = new VideoCapture(1);
        Mat _image = new Mat();

        private System.Windows.Forms.Timer timer;
        private int time = 3;
        private bool is_taken = false;


        public MakeupOutputForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        Thread thread;



        private void MakeupOutputForm_Load(object sender, EventArgs e)
        {
            thread = new Thread(new ThreadStart(CaptureVideo));
            thread.Start();
        }

        public void CaptureVideo()
        {
            streamingBox.SizeMode = PictureBoxSizeMode.Zoom;
            Console.WriteLine("화면송출");
            while (true)
            {
                _capture.Read(_image);

                // 좌우 반전
                Cv2.Flip(_image, _image, FlipMode.Y);

                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(() => streamingBox.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(_image)));
                }
                else
                {
                    pictureBox1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(_image);
                }
            }
        }

        //public void VideoClosing(object sender, EventArgs e)
        //{
        //    thread.Abort();
        //}
        public void CaptureImage()
        {
            if (is_taken)
            {
                Console.WriteLine("송출 키고, 사진 뺌");
                this.captureImg.Image.Dispose();
                this.captureImg.Image = null;
                this.streamingBox.Visible = true;
                this.captureImg.Visible = false;
            }

            timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(count_down);
            timer.Interval = 1000;
            timer.Start();

        }

        private void count_down(object sender, EventArgs e)
        {
            if (time == 0)
            {
                Console.WriteLine(time);
                timer.Stop();

                _capture.Read(_image);
                if (!_image.Empty()) // 이미지가 비어있지 않으면
                {
                    this.topComent.Text = "촬칵";
                    is_taken = true;
                    string outputPath = Path.Combine(@"C:\Users\SSAFY\Desktop\capture", "captured_image.png");

                    // 좌우 반전
                    Cv2.Flip(_image, _image, FlipMode.Y);

                    // 이미지를 저장
                    Cv2.ImWrite(outputPath, _image);
                    Console.WriteLine("이미지 저장 완료");
                    this.captureImg.Image = Image.FromFile(outputPath);
                    this.captureImg.Location = this.streamingBox.Location;
                    this.captureImg.Size = this.streamingBox.Size;
                    this.captureImg.SizeMode = this.streamingBox.SizeMode;
                    this.streamingBox.Visible = false;
                    this.captureImg.Visible = true;



                    //MessageBox.Show($"이미지가 {outputPath}에 저장되었습니다.");
                    //streamingBox.Image = Image.FromFile(outputPath);
                }
                else
                {
                    Console.WriteLine("이미지 실패");
                    //MessageBox.Show("캡처할 이미지가 없습니다.");
                }
                time = 3;

            }
            else if (0 < time)
            {
                Console.WriteLine(time);
                this.topComent.Text = time.ToString();
                time--;
            }
        }


    }
}
