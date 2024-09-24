using System;
using System.Drawing;
using System.Net.Http;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

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

        public SearchInfoInputForm(string apiResponse, SearchInfoOutputForm outputForm)
        {
            InitializeComponent();
            this.outputForm = outputForm;
            this.resultApi = apiResponse;

            // 페이지 버튼 패널 초기화
            InitializePageButtonsPanel();

            // API 결과를 처리 및 UI 표시
            DisplayApiResponse(apiResponse);
        }

        // 페이지 번호 버튼 패널 초기화
        private void InitializePageButtonsPanel()
        {
            pageButtonsPanel = new FlowLayoutPanel();
            pageButtonsPanel.Location = new Point(600, 910);  // 페이지 버튼 위치 설정
            pageButtonsPanel.Size = new Size(800, 50);        // 페이지 버튼 크기 설정
            pageButtonsPanel.FlowDirection = FlowDirection.LeftToRight; // 버튼을 가로로 배치
            Controls.Add(pageButtonsPanel);
        }

        // API 응답 데이터를 처리하여 UI에 표시하는 메서드
        private void DisplayApiResponse(string apiResponse)
        {
            // 기존 상품 목록 및 페이지 버튼 초기화
            panel6.Controls.Clear();
            pageButtonsPanel.Controls.Clear();

            // API 응답을 JSON으로 파싱
            JObject jsonResponse = JObject.Parse(apiResponse);

            // API 응답에서 필요한 데이터 추출 (예: 검색된 상품 목록)
            var goodsList = jsonResponse["data"]["goodsList"]["content"];

            // 검색 키워드 추출 및 total 라벨에 반영
            currentKeyword = jsonResponse["data"]["searchKeyword"].ToString();
            total.Text = $"'{currentKeyword}'에 대한 검색결과";

            // 총 개수와 페이지 크기 표시 (label7)
            int totalElements = (int)jsonResponse["data"]["goodsList"]["page"]["totalElements"];

            totalPages = (int)jsonResponse["data"]["goodsList"]["page"]["totalPages"];

            label7.Text = $"총 {totalElements}개";

            // 동적으로 패널을 추가하여 상품 정보 표시
            int panelIndex = 0;
            int itemsPerRow = 3;  // 한 줄에 배치할 아이템 수
            int panelSpacing = 15; // 패널 간격 설정

            foreach (var goods in goodsList)
            {
                string goodsName = goods["goodsName"].ToString();
                string goodsPrice = goods["goodsPrice"].ToString();
                string goodsDiscountPrice = goods["goodsDiscountPrice"].ToString();
                string brandName = goods["brandNameKr"].ToString();
                string goodsImage = goods["goodsImage"].ToString();

                // 상품 패널 생성
                Panel productPanel = CreateGoodsPanel(goodsName, goodsPrice, goodsDiscountPrice, brandName, goodsImage);

                // 가로로 배치 (한 줄에 3개씩)
                int xPosition = (panelIndex % itemsPerRow) * (productPanel.Width + panelSpacing);
                int yPosition = (panelIndex / itemsPerRow) * (productPanel.Height + panelSpacing);

                // 패널 위치 지정
                productPanel.Location = new Point(xPosition, yPosition);

                // 패널 추가
                this.panel6.Controls.Add(productPanel);

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
                pageButton.Size = new Size(50, 40);    // 버튼 크기 설정
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
            }
        }

        // 상품 패널을 생성하는 메서드
        private Panel CreateGoodsPanel(string goodsName, string goodsPrice, string goodsDiscountPrice, string brandName, string goodsImage)
        {
            Panel panel = new Panel();
            panel.Size = new Size(309, 342);
            //panel.BorderStyle = BorderStyle.FixedSingle;

            // 상품 이름 Label
            Label goodsNameLabel = new Label();
            goodsNameLabel.Text = goodsName;
            goodsNameLabel.Location = new Point(0, 287);
            goodsNameLabel.Size = new Size(309, 25);
            goodsNameLabel.TextAlign = ContentAlignment.MiddleCenter; // 상품 이름을 중앙에 배치
            panel.Controls.Add(goodsNameLabel);

            // 브랜드 이름 Label
            Label brandLabel = new Label();
            brandLabel.Text = brandName;
            brandLabel.Location = new Point(0, 262);
            brandLabel.Size = new Size(309, 25);
            brandLabel.TextAlign = ContentAlignment.MiddleCenter; // 브랜드 이름 중앙에 배치
            panel.Controls.Add(brandLabel);

            // 상품 가격 Label
            Label priceLabel = new Label();
            priceLabel.Text = $"{int.Parse(goodsPrice):N0}원~";
            priceLabel.Location = new Point(80, 312);
            priceLabel.Size = new Size(80, 25);
            priceLabel.ForeColor = Color.Gray;
            priceLabel.Font = new Font(priceLabel.Font, FontStyle.Strikeout);
            panel.Controls.Add(priceLabel);

            // 할인 가격 Label
            Label discountPriceLabel = new Label();
            discountPriceLabel.Text = $"{int.Parse(goodsDiscountPrice):N0}원~";
            discountPriceLabel.Location = new Point(160, 312);
            discountPriceLabel.Size = new Size(80, 25);
            discountPriceLabel.ForeColor = Color.Red;
            panel.Controls.Add(discountPriceLabel);

            // 상품 이미지 PictureBox
            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = new Size(246, 246);
            pictureBox.Location = new Point(30, 13);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            // 이미지 로드 (임시로 placeholder 이미지 설정)
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var imageBytes = client.GetByteArrayAsync(goodsImage).Result;
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

            panel.Controls.Add(pictureBox);

            return panel;
        }

        private void home_Click(object sender, EventArgs e)
        {
            this.Hide();

            if (screens.Length == 2)
            {
                inputMonitor = 0;
            }

            MainOutputForm mainOutputForm = new MainOutputForm();

            Screen secondaryScreen = Screen.AllScreens[outputMonitor];
            mainOutputForm.StartPosition = FormStartPosition.Manual;
            mainOutputForm.Location = secondaryScreen.Bounds.Location;
            mainOutputForm.Size = new Size(secondaryScreen.Bounds.Width, secondaryScreen.Bounds.Height);

            if (outputForm != null && !outputForm.IsDisposed)
            {
                outputForm.Hide();
            }

            mainOutputForm.Show();

            MainInputForm inputForm = new MainInputForm(mainOutputForm);

            Screen primaryScreen = Screen.AllScreens[inputMonitor];
            inputForm.StartPosition = FormStartPosition.Manual;
            inputForm.Location = primaryScreen.Bounds.Location;
            inputForm.Size = new Size(primaryScreen.Bounds.Width, primaryScreen.Bounds.Height);
            inputForm.Show();
        }

        private void research_Click(object sender, EventArgs e)
        {
            this.Hide();

            if (screens.Length == 2)
            {
                inputMonitor = 0;
            }

            SearchOutputForm searchOutputForm = new SearchOutputForm();

            Screen secondaryScreen = Screen.AllScreens[outputMonitor];
            searchOutputForm.StartPosition = FormStartPosition.Manual;
            searchOutputForm.Location = secondaryScreen.Bounds.Location;
            searchOutputForm.Size = new Size(secondaryScreen.Bounds.Width, secondaryScreen.Bounds.Height);

            if (outputForm != null && !outputForm.IsDisposed)
            {
                outputForm.Hide();
            }

            searchOutputForm.Show();

            SearchInputForm inputForm = new SearchInputForm(searchOutputForm);

            Screen primaryScreen = Screen.AllScreens[inputMonitor];
            inputForm.StartPosition = FormStartPosition.Manual;
            inputForm.Location = primaryScreen.Bounds.Location;
            inputForm.Size = new Size(primaryScreen.Bounds.Width, primaryScreen.Bounds.Height);
            inputForm.Show();
        }
    }
}
