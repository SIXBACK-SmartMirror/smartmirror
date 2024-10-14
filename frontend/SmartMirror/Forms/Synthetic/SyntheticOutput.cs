
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using SmartMirror.Models;
using SmartMirror.Config;



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
            if (openStyleInputForm.SyntheticResponseList[styleNum] == null)
            {
                openStyleInputForm.SyntheticResponseList[styleNum] = new StyleData();
                openStyleInputForm.SyntheticResponseList[styleNum].goodsOptionsData = new List<GoodsOptionData>();

                Console.WriteLine("요청 전송");
                //string filePath = @"C:\Users\SSAFY\Desktop\capture\captured_image.png";

                // 프로젝트 실행 경로를 가져옴
                string basePath = AppDomain.CurrentDomain.BaseDirectory;

                // 프로젝트 내의 'capture' 폴더를 경로에 추가
                string captureFolder = Path.Combine(basePath, "capture");

                // 폴더가 없으면 생성
                if (!Directory.Exists(captureFolder))
                {
                    Directory.CreateDirectory(captureFolder);
                }

                // 파일 경로 설정
                string filePath = Path.Combine(captureFolder, "captured_image.png");
                string apiUrl = $"{ApiConfig.url}/1/styles";

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

                        Console.WriteLine("전송 성공");

                        string responseBody = await response.Content.ReadAsStringAsync();
                        JObject responseJson = JObject.Parse(responseBody);
                        // 여기서 저장
                        openStyleInputForm.SyntheticResponseList[styleNum].styleId = (int)responseJson["data"]["styleId"];
                        openStyleInputForm.SyntheticResponseList[styleNum].makeupImage = responseJson["data"]["makeupImage"].ToString();



                        var syntheticPath = responseJson["data"]["makeupImage"];
                        syntheticImg.Image = GetUrlImage(syntheticPath.ToString());
                        Console.WriteLine("이미지 로드 성공");

                        // goodsOptionList 패널 초기화
                        goodsOptionList1.Controls.Clear(); // 패널을 초기화 (기존 아이템 제거)

                        int panelIndex = 0;
                        int itemsPerRow = 2;


                        for (int i = 0; i < responseJson["data"]["goodsOptionList"].Count(); i++)
                        {

                            var goodsOption = responseJson["data"]["goodsOptionList"][i];
                            Console.WriteLine(goodsOption["goodsName"] + " 잘 나오나?");


                            GoodsOptionData goodoOptionData = new GoodsOptionData();

                            var goodsName = goodsOption["goodsName"].ToString();
                            goodoOptionData.goodsName = goodsName;

                            var optionName = goodsOption["optionName"].ToString();
                            goodoOptionData.optionName = optionName;

                            var optionImage = goodsOption["optionImage"].ToString();
                            goodoOptionData.optionImage = optionImage;

                            var isInMarket = (bool)goodsOption["isInMarket"];
                            goodoOptionData.isInMarket = isInMarket;

                            var location = "null";
                            goodoOptionData.location = "null";

                            if (isInMarket)
                            {
                                location = goodsOption["location"]["name"].ToString();
                                goodoOptionData.location = location;
                            }

                            var stock = (int)goodsOption["stock"];
                            goodoOptionData.stock = stock;

                            openStyleInputForm.SyntheticResponseList[styleNum].goodsOptionsData.Add(goodoOptionData);

                            // goodsOption을 표시할 패널 생성
                            Panel goodsOptionItemPanel = CreateGoodsOptionPanel(goodsName, optionName, optionImage, isInMarket, location, stock);

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

                            goodsOptionList1.Controls.Add(goodsOptionItemPanel);

                            // 부모 컨트롤에 패널 추가
                            if (i < 5)
                            {
                                goodsOptionList1.Controls.Add(goodsOptionItemPanel);
                            }
                            else
                            {
                                goodsOptionList2.Controls.Add(goodsOptionItemPanel);
                                Console.WriteLine("goodsOptionList2.Controls count: " + goodsOptionList2.Controls.Count);

                            }

                            // 패널이 추가된 후 다음 패널을 위한 Y 좌표 증가
                            panelIndex++;

                        }

                        //foreach (var goodsOption in responseJson["data"]["goodsOptionList"])
                        //{
                        //    GoodsOptionData goodoOptionData = new GoodsOptionData();

                        //    var goodsName = goodsOption["goods_name"].ToString();
                        //    goodoOptionData.goodsName = goodsName;

                        //    var optionName = goodsOption["option_name"].ToString();
                        //    goodoOptionData.optionName = optionName;

                        //    var optionImage = goodsOption["option_image"].ToString();
                        //    goodoOptionData.optionImage = optionImage;

                        //    openStyleInputForm.SyntheticResponseList[styleNum].goodsOptionsData.Add(goodoOptionData);

                        //    // goodsOption을 표시할 패널 생성
                        //    Panel goodsOptionItemPanel = CreateGoodsOptionPanel(goodsName, optionName, optionImage);

                        //    // 패널의 크기를 미리 설정한 경우 사용
                        //    int panelWidth = goodsOptionItemPanel.Width;
                        //    int panelHeight = goodsOptionItemPanel.Height;

                        //    // 부모 컨트롤(goodsOptionList)의 크기 가져오기
                        //    int parentWidth = goodsOptionList1.Width;
                        //    int parentHeight = goodsOptionList1.Height;

                        //    // X 좌표와 Y 좌표를 부모 컨트롤의 중간에 위치하도록 계산
                        //    int x = (parentWidth - panelWidth) / 2;  // 부모 컨트롤의 중간에 X 좌표 배치
                        //    int y = panelIndex * 150;  // 기존 Y 좌표는 그대로 사용 (세로로 쌓임)

                        //    // 패널 위치 설정
                        //    goodsOptionItemPanel.Location = new Point(x, y);

                        //    // 부모 컨트롤에 패널 추가
                        //    goodsOptionList1.Controls.Add(goodsOptionItemPanel);

                        //    // 패널이 추가된 후 다음 패널을 위한 Y 좌표 증가
                        //    panelIndex++;
                        //}
                    }
                    else
                    {
                        Console.WriteLine($"전송 실패: {response.StatusCode}");
                        // 다시 선택해 주세요 만들기
                        // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                    }
                }
            }
            else
            {
                Console.WriteLine("이미지 이미 있는데요?");
                StyleInputForm openStyleInputForm = Application.OpenForms["StyleInputForm"] as StyleInputForm;

                var syntheticPath = openStyleInputForm.SyntheticResponseList[styleNum].makeupImage;
                syntheticImg.Image = GetUrlImage(syntheticPath);
                Console.WriteLine("이미지 로드 성공");
                List<GoodsOptionData> goodsOptionDatas = openStyleInputForm.SyntheticResponseList[styleNum].goodsOptionsData;


                // goodsOptionList 패널 초기화
                goodsOptionList1.Controls.Clear(); // 패널을 초기화 (기존 아이템 제거)

                int panelIndex = 0;
                int itemsPerRow = 2;


                for (int  i = 0; i < goodsOptionDatas.Count(); i++)
                {
                    GoodsOptionData goodsOptionData = goodsOptionDatas[i];

                    // goodsOption을 표시할 패널 생성
                    Panel goodsOptionItemPanel = CreateGoodsOptionPanel(goodsOptionData.goodsName, goodsOptionData.optionName, goodsOptionData.optionImage, goodsOptionData.isInMarket, goodsOptionData.location, goodsOptionData.stock);

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
                    if (i < 5)
                    {
                        goodsOptionList1.Controls.Add(goodsOptionItemPanel);
                    }
                    else
                    {
                        goodsOptionList2.Controls.Add(goodsOptionItemPanel);
                    }

                    // 패널이 추가된 후 다음 패널을 위한 Y 좌표 증가
                    panelIndex++;
                }

                //foreach (GoodsOptionData goodsOptionData in goodsOptionDatas)
                //{
                //    // goodsOption을 표시할 패널 생성
                //    Panel goodsOptionItemPanel = CreateGoodsOptionPanel(goodsOptionData.goodsName, goodsOptionData.optionName, goodsOptionData.optionImage);

                //    // 패널의 크기를 미리 설정한 경우 사용
                //    int panelWidth = goodsOptionItemPanel.Width;
                //    int panelHeight = goodsOptionItemPanel.Height;

                //    // 부모 컨트롤(goodsOptionList)의 크기 가져오기
                //    int parentWidth = goodsOptionList1.Width;
                //    int parentHeight = goodsOptionList1.Height;

                //    // X 좌표와 Y 좌표를 부모 컨트롤의 중간에 위치하도록 계산
                //    int x = (parentWidth - panelWidth) / 2;  // 부모 컨트롤의 중간에 X 좌표 배치
                //    int y = panelIndex * 150;  // 기존 Y 좌표는 그대로 사용 (세로로 쌓임)

                //    // 패널 위치 설정
                //    goodsOptionItemPanel.Location = new Point(x, y);

                //    // 부모 컨트롤에 패널 추가
                //    goodsOptionList1.Controls.Add(goodsOptionItemPanel);

                //    // 패널이 추가된 후 다음 패널을 위한 Y 좌표 증가
                //    panelIndex++;
                //}
            }
        }

        public void changePage (int index)
        {
            if (index == 0)
            {
                Console.WriteLine("index = 0");
                goodsOptionList1.Visible = true;
                goodsOptionList2.Visible = false;
            }
            else if (index == 1)
            {
                Console.WriteLine("index = 1");
                goodsOptionList1.Visible = false;
                goodsOptionList2.Visible = true;
            }
        }

        public void visbleLocation()
        {
            if (locationImg.Visible)
            {
                locationImg.Visible = false;
            }
            else
            {
                locationImg.Visible = true;
            }
        }

        // goodsOptionList의 각 항목을 패널로 생성하는 메서드
        private Panel CreateGoodsOptionPanel(string goodsName, string optionName, string optionImage, bool isInMarket, string location, int stock)
        {
            Panel panel = new Panel();
            panel.Size = new Size(700, 140); // 패널 크기 설정
            panel.BorderStyle = BorderStyle.None; // 기본 테두리 제거

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
    }
}
