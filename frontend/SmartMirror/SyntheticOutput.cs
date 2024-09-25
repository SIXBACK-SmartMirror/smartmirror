using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Security.Policy;
using static OpenCvSharp.XImgProc.CvXImgProc;
using Newtonsoft.Json.Linq;



namespace SmartMirror
{
    public partial class SyntheticOutput : Form
    {
        public int styleNum;
        StyleInputForm openStyleInputForm = Application.OpenForms["StyleInputForm"] as StyleInputForm;  

        public SyntheticOutput(int styleNum)
        {
            InitializeComponent();
            this.styleNum = styleNum;

        }

        private async void SyntheticOutput_Load(object sender, EventArgs e)
        {
            await Synthetic(styleNum);

        }

        // Synthetic 메서드 내에서 goodsOptionList UI 구성 부분 추가
        public async Task Synthetic(int styleNum)
        {
            Console.WriteLine("요청 전송");
            string filePath = @"C:\Users\SSAFY\Desktop\capture\captured_image.png";
            string apiUrl = "http://192.168.100.147:8080/smartmirrorApi/market/1/styles";

            using (var client = new HttpClient())
            using (var form = new MultipartFormDataContent())
            {
                // 파일을 ByteArrayContent로 변환
                var fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes(filePath));
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                form.Add(fileContent, "inputImage", Path.GetFileName(filePath));
                form.Add(new StringContent(styleNum.ToString()), "styleId");

                // POST 요청 전송
                HttpResponseMessage response = await client.PostAsync(apiUrl, form);

                // 응답 처리



            if (response.IsSuccessStatusCode)
                {
                    if (openStyleInputForm.SyntheticResponseList[styleNum] == null)
                    {
                        Console.WriteLine("전송 성공");

                        string responseBody = await response.Content.ReadAsStringAsync();
                        JObject responseJson = JObject.Parse(responseBody);
                        //Console.WriteLine(responseJson["data"]["goodsOptionList"]);
                        //Console.WriteLine(responseJson["data"]["makeupImage"]);
                        //openStyleInputForm.imgUrmList[styleNum] = responseJson["data"]["makeupImage"].ToString();
                        openStyleInputForm.SyntheticResponseList[styleNum] = responseJson.ToString();


                        var syntheticPath = responseJson["data"]["makeupImage"];
                        syntheticImg.Image = GetUrlImage(syntheticPath.ToString());
                        Console.WriteLine("이미지 로드 성공");

                        // goodsOptionList 패널 초기화
                        goodsOptionList.Controls.Clear(); // 패널을 초기화 (기존 아이템 제거)

                        int panelIndex = 0;
                        foreach (var goodsOption in responseJson["data"]["goodsOptionList"])
                        {
                            var goodsName = goodsOption["goods_name"].ToString();
                            var optionName = goodsOption["option_name"].ToString();
                            var optionImage = goodsOption["option_image"].ToString();

                            // goodsOption을 표시할 패널 생성
                            Panel goodsOptionItemPanel = CreateGoodsOptionPanel(goodsName, optionName, optionImage);
                            goodsOptionItemPanel.Location = new Point(0, panelIndex * 150); // 패널 위치 (세로로 쌓임)
                            goodsOptionList.Controls.Add(goodsOptionItemPanel);

                            panelIndex++;
                        }
                    }
                    else
                    {

                        Console.WriteLine("이미지 이미 있는데요?");
                    }

                }
                else
                {
                    Console.WriteLine($"전송 실패: {response.StatusCode}");
                }
            }



            }

            // goodsOptionList의 각 항목을 패널로 생성하는 메서드
            private Panel CreateGoodsOptionPanel(string goodsName, string optionName, string optionImage)
        {
            Panel panel = new Panel();
            panel.Size = new Size(300, 140); // 패널 크기 설정
            panel.BorderStyle = BorderStyle.FixedSingle;

            // 옵션 이미지 PictureBox 생성
            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = new Size(100, 100);
            pictureBox.Location = new Point(10, 10);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            // 이미지 로드 (예외 처리 포함)
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var imageBytes = client.GetByteArrayAsync(optionImage).Result;
                    using (var ms = new System.IO.MemoryStream(imageBytes))
                    {
                        pictureBox.Image = Image.FromStream(ms); // 이미지 로드
                    }
                }
            }
            catch (Exception)
            {
                pictureBox.Image = Image.FromFile("placeholder.png"); // 기본 이미지 설정
            }

            // 상품명 Label 생성
            Label goodsNameLabel = new Label();
            goodsNameLabel.Text = goodsName;
            goodsNameLabel.Location = new Point(120, 10);
            goodsNameLabel.Size = new Size(160, 25);
            goodsNameLabel.Font = new Font(goodsNameLabel.Font.FontFamily, 10, FontStyle.Bold);

            // 옵션명 Label 생성
            Label optionNameLabel = new Label();
            optionNameLabel.Text = optionName;
            optionNameLabel.Location = new Point(120, 50);
            optionNameLabel.Size = new Size(160, 25);
            optionNameLabel.Font = new Font(optionNameLabel.Font.FontFamily, 9, FontStyle.Italic);

            // 패널에 컨트롤 추가
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(goodsNameLabel);
            panel.Controls.Add(optionNameLabel);

            return panel;
        }

        private Image GetUrlImage(string url)
        {

            byte[] imageBytes = Convert.FromBase64String(url);


            using (MemoryStream memstr = new MemoryStream(imageBytes))
            {
                Image img = Image.FromStream(memstr);
                return img;
            }
        }
    }
}
