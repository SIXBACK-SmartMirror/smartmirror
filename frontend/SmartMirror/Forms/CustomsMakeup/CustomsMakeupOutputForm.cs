using Newtonsoft.Json.Linq;
using SmartMirror.Config;
using SmartMirror.Models;
using System.Net.Http.Headers;

namespace SmartMirror
{
    public partial class CustomsMakeupOutputForm : Form
    {
        public string syntheticPath = null;
        public string qrImg = null;
        public CustomsMakeupOutputForm()
        {
            InitializeComponent();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = 0x02000000;
                return cp;
            }
        }

        public async void apiRequest(CustomsGoodsData[] chooseGoodsList)
        {
            string lipColor = null;
            string eyebrowColor = null;
            string skinColor = null;

            if (chooseGoodsList[0] != null)
            {
                lipColor = chooseGoodsList[0].optionColor;
            }

            if (chooseGoodsList[1] != null)
            {
                eyebrowColor = chooseGoodsList[1].optionColor;
            }

            if (chooseGoodsList[2] != null)
            {
                skinColor = chooseGoodsList[2].optionColor;
            }


            // 프로젝트 실행 경로를 가져옴
            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            // 프로젝트 내의 'capture' 폴더를 경로에 추가
            string captureFolder = Path.Combine(basePath, "capture");

            // 파일 경로 설정
            string filePath = Path.Combine(captureFolder, "captured_image.png");
            string apiUrl = $"{ApiConfig.url}/1/custom";

            using (var client = new HttpClient())
            using (var form = new MultipartFormDataContent())
            {
                // 파일을 ByteArrayContent로 변환
                var fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes(filePath));
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                form.Add(fileContent, "inputImage", Path.GetFileName(filePath));

                if (lipColor != null)
                {
                    form.Add(new StringContent(lipColor.ToString()), "lipColor");
                }

                if (eyebrowColor != null)
                {
                    form.Add(new StringContent(eyebrowColor.ToString()), "eyebrowColor");
                }

                if (skinColor != null)
                {
                    form.Add(new StringContent(skinColor.ToString()), "skinColor");
                }


                // POST 요청 전송
                HttpResponseMessage response = await client.PostAsync(apiUrl, form);


                // 응답 처리
                if (response.IsSuccessStatusCode)
                {

                    Console.WriteLine("전송 성공");

                    string responseBody = await response.Content.ReadAsStringAsync();
                    JObject responseJson = JObject.Parse(responseBody);

                    syntheticPath = responseJson["data"]["makeupImage"].ToString();
                    syntheticImg.Image = GetUrlImage(syntheticPath);
                    Console.WriteLine("이미지 로드 성공");


                }
                else
                {
                    Console.WriteLine($"전송 실패: {response.StatusCode}");
                    // 다시 선택해 주세요 만들기
                    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                }
            }


            // goodsOptionList 패널 초기화
            goodsOptionList1.Controls.Clear(); // 패널을 초기화 (기존 아이템 제거)

            int panelIndex = 0;


            foreach (var goodsOption in chooseGoodsList)
            {
                if (goodsOption != null)
                {
                var goodsName = goodsOption.goodsName;

                var optionName = goodsOption.optionName;

                var optionImage = goodsOption.optionImage;

                var isInMarket = (bool)goodsOption.isInMarket;

                var location = "null";

                if (isInMarket)
                {
                    location = goodsOption.location;
                }

                var stock = goodsOption.stock;

                var optionColorString = goodsOption.optionColor;

                // goodsOption을 표시할 패널 생성
                Panel goodsOptionItemPanel = CreateGoodsOptionPanel(goodsName, optionName, optionImage, isInMarket, location, stock, optionColorString);
                // 패널의 크기를 미리 설정한 경우 사용
                int panelWidth = goodsOptionItemPanel.Width;
                int panelHeight = goodsOptionItemPanel.Height;

                // 부모 컨트롤(goodsOptionList)의 크기 가져오기
                int parentWidth = goodsOptionList1.Width;
                int parentHeight = goodsOptionList1.Height;

                // X 좌표와 Y 좌표를 부모 컨트롤의 중간에 위치하도록 계산
                int x = (parentWidth - panelWidth) / 2;  // 부모 컨트롤의 중간에 X 좌표 배치
                int y = panelIndex * 150;  // 기존 Y 좌표는 그대로 사용 (세로로 쌓임)

                // 패널 위치 설정
                goodsOptionItemPanel.Location = new Point(x, y);

                // 부모 컨트롤에 패널 추가
                goodsOptionList1.Controls.Add(goodsOptionItemPanel);

                // 패널이 추가된 후 다음 패널을 위한 Y 좌표 증가
                panelIndex++;
            }
                }

        }

        private Panel CreateGoodsOptionPanel(string goodsName, string optionName, string optionImage, bool isInMarket, string location, int stock, string optionColorString)
        {
            //string[] colors = optionColorString.Split(',');
            //int[] optionColor = new int[3];

            //for (int i = 0; i < colors.Length; i++)
            //{
            //    if (int.TryParse(colors[i], out int colorValue))
            //    {
            //        optionColor[i] = colorValue;
            //    }
            //    else
            //    {
            //        Console.WriteLine("Invalid color value.");
            //    }
            //}


            Panel panel = new Panel();
            panel.Size = new Size(700, 140); // 패널 크기 설정
            //panel.BorderStyle = BorderStyle.FixedSingle;
            panel.BorderStyle = BorderStyle.None; // 기본 테두리 제거

            //panel.Paint += (s, e) =>
            //{
            //    var graphics = e.Graphics;
            //    Pen pen = new Pen(Color.Gray, 1); // 상, 하 테두리 색상 및 두께 설정
            //    // 상 테두리
            //    graphics.DrawLine(pen, new Point(0, 0), new Point(panel.Width, 0));
            //    // 하 테두리
            //    graphics.DrawLine(pen, new Point(0, panel.Height - 1), new Point(panel.Width, panel.Height - 1));
            //};


            // 옵션 이미지 PictureBox 생성
            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = new Size(100, 100);
            pictureBox.Location = new Point(20, 20);
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

            //pictureBox.BackColor = Color.FromArgb(optionColor[0], optionColor[1], optionColor[2]);

            // 상품명 Label 생성 (크기 및 위치 조정)
            Label goodsNameLabel = new Label();
            goodsNameLabel.Text = goodsName;
            goodsNameLabel.Location = new Point(140, 10);
            goodsNameLabel.Size = new Size(500, 40);
            goodsNameLabel.Font = new Font(goodsNameLabel.Font.FontFamily, 20, FontStyle.Bold);

            // 옵션명 Label 생성 (크기 및 위치 조정)
            Label optionNameLabel = new Label();
            optionNameLabel.Text = optionName;
            optionNameLabel.Location = new Point(140, 55);
            optionNameLabel.Size = new Size(500, 30);
            optionNameLabel.Font = new Font(optionNameLabel.Font.FontFamily, 15, FontStyle.Bold);

            // 옵션 상세 정보 Label 생성 (크기 및 위치 조정)
            Label optionDetailLabel = new Label();
            if (isInMarket)
            {
                optionDetailLabel.Text = $"재고: {stock}개, 위치: {location} ";
                Console.WriteLine(location);
            }
            else
            {
                optionDetailLabel.Text = "미판매 매장";
            }
            optionDetailLabel.Location = new Point(140, 90);
            optionDetailLabel.Size = new Size(500, 30);
            optionDetailLabel.Font = new Font(optionNameLabel.Font.FontFamily, 15, FontStyle.Bold);



            // 패널에 컨트롤 추가
            panel.Controls.Add(pictureBox);
            panel.Controls.Add(goodsNameLabel);
            panel.Controls.Add(optionNameLabel);
            panel.Controls.Add(optionDetailLabel);


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

        public async void qr_click(CustomsGoodsData[] chooseGoodsList)
        {
            string apiUrl = $"{ApiConfig.url}/1/result/generate-qr ";

            using (var client = new HttpClient())
            using (var form = new MultipartFormDataContent())
            {
                form.Add(new StringContent(syntheticPath), "makeupImage");

                string optionIdList = "";
                for (int i = 0; i < chooseGoodsList.Length; i++)
                {
                    if (chooseGoodsList[i] != null)
                    {
                        if (i + 1 == chooseGoodsList.Length)
                        {
                            optionIdList = optionIdList + $"{chooseGoodsList[i].optionId}";
                        }
                        else
                        {
                            optionIdList = optionIdList + $"{chooseGoodsList[i].optionId},";
                        }
                    }
                }

                form.Add(new StringContent(optionIdList), "optionIdList");

                // POST 요청 전송
                HttpResponseMessage response = await client.PostAsync(apiUrl, form);


                // 응답 처리
                if (response.IsSuccessStatusCode)
                {

                    Console.WriteLine("전송 성공");

                    string responseBody = await response.Content.ReadAsStringAsync();
                    JObject responseJson = JObject.Parse(responseBody);

                    qrImg = responseJson["data"]["qrImage"].ToString();
                    QRpicture.Image = GetUrlImage(qrImg);
                    Console.WriteLine("이미지 로드 성공");
                    QRpicture.Visible = true;


                }
                else
                {
                    Console.WriteLine($"전송 실패: {response.StatusCode}");
                    // 다시 선택해 주세요 만들기
                    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                }
            }

            if (syntheticImg.Visible)
            {
                syntheticImg.Visible = false;
                QRpicture.Visible = true;

            }
            else
            {
                syntheticImg.Visible = true;
                QRpicture.Visible = false;
            }
        }
    }
}
