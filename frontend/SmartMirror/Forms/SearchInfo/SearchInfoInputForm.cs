using Newtonsoft.Json.Linq;
using SmartMirror.Config;
using SmartMirror.Helpers;
using SmartMirror.Models;


namespace SmartMirror
{
    public partial class SearchInfoInputForm : Form
    {
        private string resultApi;
        private string currentKeyword;
        private int currentPage = 0;
        private int totalPages = 1;
        private FlowLayoutPanel pageButtonsPanel;

        private int outputMonitor = 1;
        private int inputMonitor = 2;
        private Screen[] screens = Screen.AllScreens;
        private SearchInfoOutputForm outputForm;

        private bool flag;

        public SearchInfoInputForm(string apiResponse, SearchInfoOutputForm outputForm)
        {
            InitializeComponent();
            this.outputForm = outputForm;
            this.resultApi = apiResponse;

            // 페이지 버튼 패널 초기화
            InitializePageButtonsPanel();

            Console.Write(apiResponse);

            // API 결과를 처리 및 UI 표시
            DisplayApiResponse(apiResponse);
            DisplayApiResponse2(apiResponse);

            outputForm.panel3.Dock = DockStyle.Fill;
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

        // 페이지 번호 버튼 패널 초기화
        private void InitializePageButtonsPanel()
        {
            pageButtonsPanel = new FlowLayoutPanel();
            pageButtonsPanel.Location = new Point(panel6.Left + 30, panel6.Bottom + 20); // panel6 바로 아래에 위치하도록 설정
            pageButtonsPanel.Size = new Size(panel6.Width, 55); // panel6의 가로 너비에 맞춤
            pageButtonsPanel.FlowDirection = FlowDirection.LeftToRight; // 버튼을 가로로 배치
            pageButtonsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right; // 창 크기 변경에 따라 위치와 크기 조정
            Controls.Add(pageButtonsPanel);
        }

        private List<GoodsData> ApiResponse(string apiResponse)
        {
            List<GoodsData> goodsDataList = new List<GoodsData>();

            // API 응답을 JSON으로 파싱
            JObject jsonResponse = JObject.Parse(apiResponse);

            // API 응답에서 필요한 데이터 추출 (예: 검색된 상품 목록)
            var goodsList = jsonResponse["data"]["goodsList"]["content"];

            // 검색 키워드 추출 및 total 라벨에 반영
            currentKeyword = jsonResponse["data"]["searchKeyword"].ToString();
            outputForm.total.Text = currentKeyword;

            // 총 개수와 페이지 크기 표시 (label7)
            int totalElements = (int)jsonResponse["data"]["goodsList"]["page"]["totalElements"];

            totalPages = (int)jsonResponse["data"]["goodsList"]["page"]["totalPages"];

            label7.Text = $"총 {totalElements}개";
            outputForm.label7.Text = $"총 {totalElements}개";

            foreach (var goods in goodsList)
            {
                GoodsData goodsData = new GoodsData
                {
                    Id = (int)goods["goodsId"],
                    GoodsName = goods["goodsName"].ToString(),
                    GoodsPrice = goods["goodsPrice"].ToString(),
                    GoodsDiscountPrice = goods["goodsDiscountPrice"].ToString(),
                    BrandName = goods["brandNameKr"].ToString(),
                    GoodsImage = goods["goodsImage"].ToString()
                };

                goodsDataList.Add(goodsData);
            }

            return goodsDataList;
        }

        // API 응답 데이터를 처리하여 UI에 표시하는 메서드
        private void DisplayApiResponse(string apiResponse)
        {
            outputForm.panel6.Controls.Clear();

            // 동적으로 패널을 추가하여 상품 정보 표시
            int panelIndex = 0;
            int itemsPerRow = 3;  // 한 줄에 배치할 아이템 수
            int panelSpacing = 15; // 패널 간격 설정

            List<GoodsData> goodsData = ApiResponse(apiResponse);

            foreach (var goods in goodsData)
            {
                // 상품 패널 생성
                Panel productPanel = CreateGoodsPanel(goods);

                // 가로로 배치 (한 줄에 3개씩)
                int xPosition = (panelIndex % itemsPerRow) * (productPanel.Width + panelSpacing);
                int yPosition = (panelIndex / itemsPerRow) * (productPanel.Height + panelSpacing);

                // 패널 위치 지정
                productPanel.Location = new Point(xPosition, yPosition);

                // 패널 추가
                outputForm.panel6.Controls.Add(productPanel);

                panelIndex++;
            }
        }

        // API 응답 데이터를 처리하여 UI에 표시하는 메서드
        private void DisplayApiResponse2(string apiResponse)
        {
            // 기존 상품 목록 및 페이지  버튼 초기화
            panel6.Controls.Clear();
            pageButtonsPanel.Controls.Clear();

            // 동적으로 패널을 추가하여 상품 정보 표시
            int panelIndex = 0;
            int itemsPerRow = 3;  // 한 줄에 배치할 아이템 수
            int panelSpacing = 50; // 패널 간격 설정

            List<GoodsData> goodsData = ApiResponse(apiResponse);

            foreach (var goods in goodsData)
            {
                // 상품 패널 생성
                Panel productPanel = CreateGoodsPanel2(goods);

                // 가로로 배치 (한 줄에 3개씩)
                int xPosition = (panelIndex % itemsPerRow) * (productPanel.Width + panelSpacing);
                int yPosition = (panelIndex / itemsPerRow) * (productPanel.Height + panelSpacing);

                // 패널 위치 지정
                productPanel.Location = new Point(xPosition, yPosition);

                // 패널 추가
                panel6.Controls.Add(productPanel);

                panelIndex++;
            }

            // 페이지 버튼 생성
            CreatePageButtons(totalPages);
        }

        // 페이지 번호에 따라 버튼 생성
        private void CreatePageButtons(int totalPages)
        {
            for (int i = 0; i < totalPages; i++)
            {
                Button pageButton = new Button();
                pageButton.Text = (i + 1).ToString();  // 페이지 번호로 텍스트 설정
                pageButton.Size = new Size(50, 50);    // 버튼 크기 설정
                pageButton.Tag = i;                   // 페이지 번호를 Tag로 저장
                pageButton.Click += PageButton_Click; // 버튼 클릭 이벤트 연결
                pageButtonsPanel.Controls.Add(pageButton);
            }
        }

        // 페이지 번호 버튼 클릭 시 처리하는 메서드
        private async void PageButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            int selectedPage = (int)clickedButton.Tag;

            // 선택된 페이지 데이터를 로드
            await LoadPageData(selectedPage);
        }

        // 페이지 데이터를 다시 불러오는 메서드
        private async Task LoadPageData(int page)
        {
            // 현재 검색어로 해당 페이지의 데이터를 다시 호출
            string apiResponse = await SearchApi.CallSearchApi(currentKeyword, page);

            // 새로운 API 응답을 다시 처리하여 UI에 반영
            if (apiResponse != null)
            {
                currentPage = page; // 현재 페이지 업데이트
                DisplayApiResponse(apiResponse);  // 새로운 데이터를 UI에 반영
                DisplayApiResponse2(apiResponse);
            }
        }

        // 상품 패널을 생성하는 메서드
        private Panel CreateGoodsPanel(GoodsData goodsData)
        {
            Panel panel = new Panel();
            panel.Size = new Size(309, 342);

            // TableLayoutPanel 설정
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Dock = DockStyle.Fill; // TableLayoutPanel을 Panel 크기에 맞게 설정
            tableLayoutPanel.ColumnCount = 1; // 기본적으로 한 열에 배치
            tableLayoutPanel.RowCount = 4; // 이미지, 브랜드 이름, 상품 이름, 가격+할인 가격
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 70F)); // 이미지 크기를 60%로 설정
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F)); // 브랜드 이름 크기 10%
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F)); // 상품 이름 크기 10%
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F)); // 가격 + 할인 가격 크기 20%

            // 상품 이미지 PictureBox 설정
            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = new Size(220, 220); // 이미지 크기를 200x200으로 설정
            pictureBox.Margin = new Padding(0, 20, 0, 20); // 이미지의 여백 설정 (상단 20, 하단 20)
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom; // 이미지 크기 비율에 맞게 설정
            pictureBox.Anchor = AnchorStyles.None; // 중앙에 배치되도록 Anchor 설정

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var imageBytes = client.GetByteArrayAsync(goodsData.GoodsImage).Result;
                    using (var ms = new System.IO.MemoryStream(imageBytes))
                    {
                        pictureBox.Image = Image.FromStream(ms);
                    }
                }
            }
            catch (Exception)
            {
                pictureBox.Image = Image.FromFile("placeholder.png"); // 기본 이미지 설정
            }
            tableLayoutPanel.Controls.Add(pictureBox, 0, 0);

            // 브랜드 이름 Label 설정
            Label brandLabel = new Label();
            brandLabel.Text = goodsData.BrandName;
            brandLabel.Dock = DockStyle.Fill;
            brandLabel.TextAlign = ContentAlignment.MiddleCenter; // 중앙 정렬
            brandLabel.ForeColor = Color.White;
            tableLayoutPanel.Controls.Add(brandLabel, 0, 1);

            // 상품 이름 Label 설정
            Label goodsNameLabel = new Label();
            goodsNameLabel.Text = goodsData.GoodsName;
            goodsNameLabel.Dock = DockStyle.Fill;
            goodsNameLabel.TextAlign = ContentAlignment.MiddleCenter; // 중앙 정렬
            goodsNameLabel.ForeColor = Color.White;
            tableLayoutPanel.Controls.Add(goodsNameLabel, 0, 2);

            // 가격과 할인 가격을 담을 또 다른 TableLayoutPanel 설정
            TableLayoutPanel pricePanel = new TableLayoutPanel();
            pricePanel.ColumnCount = 2; // 가격과 할인 가격을 나란히 배치
            pricePanel.RowCount = 1;
            pricePanel.Dock = DockStyle.Fill;
            pricePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F)); // 가격 열 50%
            pricePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F)); // 할인 가격 열 50%

            // 상품 가격 Label 설정
            Label priceLabel = new Label();
            priceLabel.Text = $"{int.Parse(goodsData.GoodsPrice):N0}원";
            priceLabel.Dock = DockStyle.Fill;
            priceLabel.TextAlign = ContentAlignment.MiddleRight; // 오른쪽 정렬
            priceLabel.ForeColor = Color.Gray;
            priceLabel.Font = new Font(priceLabel.Font, FontStyle.Strikeout);
            pricePanel.Controls.Add(priceLabel, 0, 0); // 첫 번째 열에 배치

            // 할인 가격 Label 설정
            Label discountPriceLabel = new Label();
            discountPriceLabel.Text = $"{int.Parse(goodsData.GoodsDiscountPrice):N0}원";
            discountPriceLabel.Dock = DockStyle.Fill;
            discountPriceLabel.TextAlign = ContentAlignment.MiddleLeft; // 왼쪽 정렬
            discountPriceLabel.ForeColor = Color.Red;
            pricePanel.Controls.Add(discountPriceLabel, 1, 0); // 두 번째 열에 배치

            // 가격 + 할인 가격 Panel을 마지막 행에 추가
            tableLayoutPanel.Controls.Add(pricePanel, 0, 3);

            // TableLayoutPanel을 Panel에 추가
            panel.Controls.Add(tableLayoutPanel);

            // 클릭 이벤트 연결
            pictureBox.Click += (sender, e) => Panel_Click(goodsData);

            return panel;
        }

        // 상품 패널을 생성하는 메서드
        private Panel CreateGoodsPanel2(GoodsData goodsData)
        {
            // Panel 설정
            Panel panel = new Panel();
            panel.Size = new Size(229, 272);
            panel.Padding = new Padding(0); // 패널 자체의 여백 제거

            // TableLayoutPanel 설정
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Dock = DockStyle.Fill; // TableLayoutPanel을 Panel 크기에 맞게 설정
            tableLayoutPanel.ColumnCount = 1; // 기본적으로 한 열에 배치
            tableLayoutPanel.RowCount = 3; // 이미지, 브랜드 이름, 상품 이름

            // RowStyle 설정: 여백을 줄이기 위해 각 행의 높이를 조정
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 70F)); // 이미지 크기를 70%로 설정
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 15F)); // 브랜드 이름 크기 15%
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 15F)); // 상품 이름 크기 15%

            tableLayoutPanel.Margin = new Padding(0); // TableLayoutPanel의 여백 제거
            tableLayoutPanel.Padding = new Padding(0); // TableLayoutPanel의 패딩 제거

            // 상품 이미지 PictureBox 설정
            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = new Size(200, 200); // 이미지 크기를 200x200으로 설정
            pictureBox.Dock = DockStyle.None; // 이미지 크기를 고정된 크기로 설정
            pictureBox.Margin = new Padding(0); // 이미지의 여백 제거
            pictureBox.Padding = new Padding(0); // 이미지의 패딩 제거
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom; // 이미지 크기 비율에 맞게 설정
            pictureBox.Anchor = AnchorStyles.None; // 중앙에 배치되도록 Anchor 설정

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var imageBytes = client.GetByteArrayAsync(goodsData.GoodsImage).Result;
                    using (var ms = new System.IO.MemoryStream(imageBytes))
                    {
                        pictureBox.Image = Image.FromStream(ms);
                    }
                }
            }
            catch (Exception)
            {
                pictureBox.Image = Image.FromFile("placeholder.png"); // 기본 이미지 설정
            }
            tableLayoutPanel.Controls.Add(pictureBox, 0, 0);

            // 브랜드 이름 Label 설정
            Label brandLabel = new Label();
            brandLabel.Text = goodsData.BrandName;
            brandLabel.Dock = DockStyle.Fill;
            brandLabel.TextAlign = ContentAlignment.MiddleCenter; // 중앙 정렬
            brandLabel.Margin = new Padding(0); // 여백 제거
            tableLayoutPanel.Controls.Add(brandLabel, 0, 1);

            // 상품 이름 Label 설정
            Label goodsNameLabel = new Label();
            goodsNameLabel.Text = goodsData.GoodsName;
            goodsNameLabel.Dock = DockStyle.Fill;
            goodsNameLabel.TextAlign = ContentAlignment.MiddleCenter; // 중앙 정렬
            goodsNameLabel.Margin = new Padding(0); // 여백 제거
            tableLayoutPanel.Controls.Add(goodsNameLabel, 0, 2);

            // TableLayoutPanel을 Panel에 추가
            panel.Controls.Add(tableLayoutPanel);

            // 클릭 이벤트 연결
            pictureBox.Click += (sender, e) => Panel_Click(goodsData);

            return panel;
        }

        private async void Panel_Click(GoodsData goodsData)
        {

            string baseUrl = $"{ApiConfig.url}/1/goods";
            string urlWithParams = $"{baseUrl}/{goodsData.Id}";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(urlWithParams);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseData);


                        if (screens.Length == 2)
                        {
                            inputMonitor = 0;
                        }

                        SearchDetailOutputForm searchDetailOutputForm = new SearchDetailOutputForm(responseData);

                        Screen secondaryScreen = Screen.AllScreens[outputMonitor];
                        searchDetailOutputForm.StartPosition = FormStartPosition.Manual;
                        searchDetailOutputForm.Location = secondaryScreen.Bounds.Location;
                        searchDetailOutputForm.Size = new Size(secondaryScreen.Bounds.Width, secondaryScreen.Bounds.Height);

                        if (outputForm != null && !outputForm.IsDisposed)
                        {
                            outputForm.Hide();
                        }

                        searchDetailOutputForm.Show();

                        JObject jsonResponse = JObject.Parse(resultApi);

                        SearchDetailInputForm inputForm = new SearchDetailInputForm(searchDetailOutputForm, goodsData, responseData);

                        Screen primaryScreen = Screen.AllScreens[inputMonitor];
                        inputForm.StartPosition = FormStartPosition.Manual;
                        inputForm.Location = primaryScreen.Bounds.Location;
                        inputForm.Size = new Size(primaryScreen.Bounds.Width, primaryScreen.Bounds.Height);

                        this.Hide();
                        inputForm.Show();
                    }
                    else
                    {
                        MessageBox.Show($"상품에 대한 요청 실패: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류 발생: {ex.Message}");
            }
        }

        private void home_Click(object sender, EventArgs e)
        {
            this.Hide();

            // 스크린 설정 호출
            var screens = Screen.AllScreens;
            var (primaryScreen, secondaryScreen) = FormHelper.SetupScreens(outputMonitor, ref inputMonitor, screens);

            MainOutputForm mainOutputForm = new MainOutputForm();
            MainInputForm inputForm = new MainInputForm(mainOutputForm);

            FormHelper.SwitchToForm(inputForm, mainOutputForm, primaryScreen, secondaryScreen);

            if (outputForm != null && !outputForm.IsDisposed)
            {
                outputForm.Hide();
            }
        }

        private void research_Click(object sender, EventArgs e)
        {
            this.Hide();

            // 스크린 설정 호출
            var screens = Screen.AllScreens;
            var (primaryScreen, secondaryScreen) = FormHelper.SetupScreens(outputMonitor, ref inputMonitor, screens);

            SearchOutputForm searchOutputForm = new SearchOutputForm();
            SearchInputForm inputForm = new SearchInputForm(searchOutputForm);

            FormHelper.SwitchToForm(inputForm, searchOutputForm, primaryScreen, secondaryScreen);

            if (outputForm != null && !outputForm.IsDisposed)
            {
                outputForm.Hide();
            }
        }

        private void mirror_Click(object sender, EventArgs e)
        {
            outputForm.panel3.Visible = flag;
            flag = !flag;
        }
    }
}
