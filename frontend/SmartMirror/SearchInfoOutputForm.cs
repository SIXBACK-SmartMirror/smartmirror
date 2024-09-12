using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartMirror
{
    public partial class SearchInfoOutputForm : Form
    {
        //private class Product
        //{
        //    public int goodsId { get; set; }
        //    public String goodsImage { get; set; }

        //    public String goodsName { get; set; }
        //    public int goodsPrice { get; set; }
        //    public int goodsDiscountPrice { get; set; }
        //    public String brandNameKr { get; set; }
        //}

        public SearchInfoOutputForm()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // 둥근 모서리 반지름 설정
            int cornerRadius = 30;

            // 패널의 크기
            int panelWidth = panel1.Width;
            int panelHeight = panel1.Height;

            // GraphicsPath를 사용해 둥근 모서리 경로를 생성
            GraphicsPath path = new GraphicsPath();
            path.AddArc(new Rectangle(0, 0, cornerRadius, cornerRadius), 180, 90);  // 좌상단
            path.AddArc(new Rectangle(panelWidth - cornerRadius, 0, cornerRadius, cornerRadius), 270, 90); // 우상단
            path.AddArc(new Rectangle(panelWidth - cornerRadius, panelHeight - cornerRadius, cornerRadius, cornerRadius), 0, 90); // 우하단
            path.AddArc(new Rectangle(0, panelHeight - cornerRadius, cornerRadius, cornerRadius), 90, 90); // 좌하단
            path.CloseFigure();

            // 패널의 모양을 둥근 모서리로 설정
            panel1.Region = new Region(path);

            // 초록색 테두리 그리기
            Pen greenPen = new Pen(Color.Green, 5); // 초록색, 두께 5의 테두리
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias; // 테두리의 부드러운 렌더링
         
            e.Graphics.DrawPath(greenPen, path);
        }
        //private void btnSearch_Click(object sender, EventArgs e)
        //{
        //    // 임시 제품 데이터 생성
        //    List<Product> products = GetMockProducts();

        //    if (products != null && products.Count > 0)
        //    {
        //        DisplayProducts(products); // 제품을 동적으로 표시
        //    }
        //    else
        //    {
        //        MessageBox.Show("검색 결과가 없습니다.");
        //    }
        //}

        //// 임시 데이터 생성 메서드
        //private List<Product> GetMockProducts()
        //{
        //    return new List<Product>
        //{
        //    new Product
        //    {
        //        goodsId = 1,
        //        goodsImage = "https://via.placeholder.com/100", // 임시 이미지 URL
        //        goodsName = "Product 1",
        //        goodsPrice = 10000,
        //        goodsDiscountPrice = 9000,
        //        brandNameKr = "Brand A"
        //    },
        //    new Product
        //    {
        //        goodsId = 2,
        //        goodsImage = "https://via.placeholder.com/100", // 임시 이미지 URL
        //        goodsName = "Product 2",
        //        goodsPrice = 20000,
        //        goodsDiscountPrice = 18000,
        //        brandNameKr = "Brand B"
        //    },
        //    new Product
        //    {
        //        goodsId = 3,
        //        goodsImage = "https://via.placeholder.com/100", // 임시 이미지 URL
        //        goodsName = "Product 3",
        //        goodsPrice = 30000,
        //        goodsDiscountPrice = 27000,
        //        brandNameKr = "Brand C"
        //    }
        //};
        //}

        //// 받아온 제품 목록을 화면에 동적으로 표시하는 메서드
        //private void DisplayProducts(List<Product> products)
        //{
        //    // 기존의 컨트롤이 있다면 모두 제거
        //    panelProducts.Controls.Clear();

        //    int yPosition = 10; // 컨트롤의 초기 y 위치

        //    foreach (var product in products)
        //    {
        //        // 동적으로 PictureBox 생성
        //        PictureBox pictureBox = new PictureBox
        //        {
        //            Size = new Size(100, 100),
        //            Location = new Point(10, yPosition),
        //            SizeMode = PictureBoxSizeMode.StretchImage
        //        };
        //        // 임시 이미지 URL을 사용하여 로컬 또는 웹 이미지 로드
        //        LoadImageFromUrl(product.goodsImage, pictureBox);

        //        // 동적으로 Label (이름) 생성
        //        Label nameLabel = new Label
        //        {
        //            Text = product.goodsName,
        //            Location = new Point(120, yPosition),
        //            AutoSize = true
        //        };

        //        // 동적으로 Label (가격) 생성
        //        Label priceLabel = new Label
        //        {
        //            Text = $"가격: ₩{product.goodsPrice}",
        //            Location = new Point(120, yPosition + 20),
        //            AutoSize = true
        //        };

        //        // 동적으로 Label (할인 가격) 생성
        //        Label discountPriceLabel = new Label
        //        {
        //            Text = $"할인가: ₩{product.goodsDiscountPrice}",
        //            Location = new Point(120, yPosition + 40),
        //            AutoSize = true
        //        };

        //        // 동적으로 Label (브랜드) 생성
        //        Label brandLabel = new Label
        //        {
        //            Text = $"브랜드: {product.brandNameKr}",
        //            Location = new Point(120, yPosition + 60),
        //            AutoSize = true
        //        };

        //        // 컨트롤을 패널에 추가
        //        panelProducts.Controls.Add(pictureBox);
        //        panelProducts.Controls.Add(nameLabel);
        //        panelProducts.Controls.Add(priceLabel);
        //        panelProducts.Controls.Add(discountPriceLabel);
        //        panelProducts.Controls.Add(brandLabel);

        //        // 다음 컨트롤을 배치할 위치를 계산
        //        yPosition += 120; // 각 제품 블록의 간격 설정
        //    }
        //}

        //// URL로부터 이미지를 로드하여 PictureBox에 표시하는 메서드 (임시 URL 사용)
        //private void LoadImageFromUrl(string imageUrl, PictureBox pictureBox)
        //{
        //    try
        //    {
        //        // 임시로 이미지 로드를 동기 방식으로 진행 (비동기로 처리할 필요 없음)
        //        pictureBox.Load(imageUrl);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("이미지를 불러오는 중 오류가 발생했습니다: " + ex.Message);
        //    }
        //}
    }
}
