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

        public SearchInfoInputForm(string apiResponse)
        {
            InitializeComponent();
            this.resultApi = apiResponse;

            // API 결과를 처리 및 UI 표시
            DisplayApiResponse(apiResponse);
        }

        // API 응답 데이터를 처리하여 UI에 표시하는 메서드
        private void DisplayApiResponse(string apiResponse)
        {
            // API 응답을 JSON으로 파싱
            JObject jsonResponse = JObject.Parse(apiResponse);

            // API 응답에서 필요한 데이터 추출 (예: 검색된 상품 목록)
            var goodsList = jsonResponse["data"]["goodsList"]["content"];

            // 검색 키워드 추출 및 total 라벨에 반영
            string searchKeyword = jsonResponse["data"]["searchKeyword"].ToString();
            total.Text = $"'{searchKeyword}'에 대한 검색결과";

            // 총 개수와 페이지 크기 표시 (label7)
            int totalElements = (int)jsonResponse["data"]["goodsList"]["page"]["totalElements"];
            label7.Text = $"총 {totalElements}개";

            // 동적으로 패널을 추가하여 상품 정보 표시
            int panelIndex = 0;
            int itemsPerRow = 3;  // 한 줄에 배치할 아이템 수
            int panelSpacing = 20; // 패널 간격 설정

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
        }

        // 상품 패널을 생성하는 메서드
        private Panel CreateGoodsPanel(string goodsName, string goodsPrice, string goodsDiscountPrice, string brandName, string goodsImage)
        {
            Panel panel = new Panel();
            panel.Size = new Size(309, 342);
            panel.BorderStyle = BorderStyle.FixedSingle;

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
            priceLabel.Text = goodsPrice + "원";
            priceLabel.Location = new Point(73, 312);
            priceLabel.Size = new Size(100, 25);
            panel.Controls.Add(priceLabel);

            // 할인 가격 Label
            Label discountPriceLabel = new Label();
            discountPriceLabel.Text = goodsDiscountPrice + "원";
            discountPriceLabel.Location = new Point(167, 312);
            discountPriceLabel.Size = new Size(100, 25);
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
    }
}
