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


namespace SmartMirror
{
    public partial class SyntheticOutput : Form
    {
        public int styleNum;
        public SyntheticOutput(int styleNum)
        {
            InitializeComponent();
            this.styleNum = styleNum;
        }

        private async void SyntheticOutput_Load(object sender, EventArgs e)
        {
            await Synthetic(styleNum);

        }

        public async Task Synthetic(int styleNum)
        {
            Console.WriteLine("요청 전송");
            string filePath = @"C:\Users\SSAFY\Desktop\capture\captured_image.png";
            //string apiUrl = "http://localhost:8080/smartmirrorApi/market/1/styles/test";
            string apiUrl = "http://192.168.100.147:8080/smartmirrorApi/market/1/styles";


            using (var client = new HttpClient())
            using (var form = new MultipartFormDataContent())
            {
                // 파일을 ByteArrayContent로 변환
                var fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes(filePath));
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                form.Add(fileContent, "inputImage", Path.GetFileName(filePath));
                form.Add(new StringContent(styleNum.ToString()), "styleId");
                Console.WriteLine(styleNum.ToString());

                // POST 요청 전송
                HttpResponseMessage response = await client.PostAsync(apiUrl, form);

                // 응답 처리
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("전송 성공");
                    Console.WriteLine(await response.Content.ReadAsStringAsync());

                    string responseBody = await response.Content.ReadAsStringAsync();

                    JsonDocument jsonResponse = JsonDocument.Parse(responseBody);
                    //string path = jsonResponse.RootElement.GetProperty("data").GetProperty("qrImage").GetString();
                    string path = jsonResponse.RootElement.GetProperty("data").GetProperty("makeupImage").GetString();

                    Console.WriteLine(path);

                    syntheticImg.Image = GetUrlImage(path);


                }
                else
                {
                    Console.WriteLine($"전송 실패: {response.StatusCode}");
                }
            }


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
