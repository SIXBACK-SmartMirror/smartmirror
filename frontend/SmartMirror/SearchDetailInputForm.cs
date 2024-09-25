using SmartMirror.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartMirror
{
    public partial class SearchDetailInputForm : Form
    {
        private SearchDetailOutputForm outputForm;
        private GoodsData goodsData;

        public SearchDetailInputForm(SearchDetailOutputForm outputForm, GoodsData goodsData, string options)
        {
            this.outputForm = outputForm;
            this.goodsData = goodsData;

            InitializeComponent();
        }

        private void SearchDetailInputForm_Load(object sender, EventArgs e)
        {
            brand.Text = goodsData.BrandName;
            name.Text = goodsData.GoodsName;
            price.Text = $"{int.Parse(goodsData.GoodsPrice):N0}원~";
            discountPrice.Text = $"{int.Parse(goodsData.GoodsDiscountPrice):N0}원~";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var imageBytes = client.GetByteArrayAsync(goodsData.GoodsImage).Result;
                    using (var ms = new System.IO.MemoryStream(imageBytes))
                    {
                        img.Image = Image.FromStream(ms);
                    }
                }
            }
            catch (Exception)
            {
                img.Image = Image.FromFile("placeholder.png"); // 기본 이미지 설정
            }
        }
    }
}
