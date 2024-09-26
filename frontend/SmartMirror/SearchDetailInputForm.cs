using Newtonsoft.Json.Linq;
using SmartMirror.Helpers;
using SmartMirror.Models;
using System.Net.Http;
using System.Windows.Forms;

namespace SmartMirror
{
    public partial class SearchDetailInputForm : Form
    {
        private SearchDetailOutputForm outputForm;
        private GoodsData goodsData;
        private JObject jsonOptions; // JSON 데이터를 저장할 변수
        private Panel panelContainer;

        private int outputMonitor = 1;
        private int inputMonitor = 2;
        private Screen[] screens = Screen.AllScreens;

        public SearchDetailInputForm(SearchDetailOutputForm outputForm, GoodsData goodsData, string options)
        {
            InitializeComponent();

            this.outputForm = outputForm;
            this.goodsData = goodsData;

            // JSON 문자열을 JObject로 파싱
            this.jsonOptions = JObject.Parse(options);

            // 패널 초기화
            InitializePanelContainer();

            PopulateComboBox(this.jsonOptions);
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

        private void InitializePanelContainer()
        {
            panelContainer = new Panel
            {
                Location = new Point(50, 300),
                Size = new Size(900, 150),
                BackColor = Color.White
            };
            this.Controls.Add(panelContainer);
        }

        private void PopulateComboBox(JObject jsonResponse)
        {
            // JSON 데이터에서 optionDtoList 항목을 가져옴
            var optionList = jsonResponse["data"]["optionDtoList"];

            // optionName 항목을 ComboBox에 추가
            foreach (var option in optionList)
            {
                string optionName = option["optionName"].ToString();
                comboBox1.Items.Add(optionName);
            }

            // 콤보박스에서 첫 번째 항목을 기본 선택
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }

            // 콤보박스 선택 이벤트 핸들러 연결
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }

        // 콤보박스 선택 이벤트 핸들러
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOption = comboBox1.SelectedItem.ToString();
            CreateAndShowPanelForSelectedOption(selectedOption);
        }

        // 패널을 생성하고 표시하는 메서드
        private void CreateAndShowPanelForSelectedOption(string selectedOptionName)
        {
            // 기존 패널을 지우고 새로 표시
            if (panelContainer != null)
            {
                panelContainer.Controls.Clear();
            }

            // 데이터에서 선택된 옵션을 찾음
            var selectedOption = jsonOptions["data"]["optionDtoList"]
                .FirstOrDefault(option => option["optionName"].ToString() == selectedOptionName);

            if (selectedOption != null)
            {

                optionName.Text = selectedOption["optionName"].ToString();
                optionPrice.Text = $"{int.Parse(selectedOption["optionDiscountPrice"].ToString()):N0}원";
                optionStock.Text = selectedOption["inMarket"].ToObject<bool>()
                                    ? (selectedOption["stock"].ToObject<int>() == 0 ? "품절" : $"{selectedOption["stock"]}개")
                                    : "미판매";
                string optionImageUrl = selectedOption["optionImage"].ToString();

                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var imageBytes = client.GetByteArrayAsync(optionImageUrl).Result;
                        using (var ms = new System.IO.MemoryStream(imageBytes))
                        {
                            optionImg.Image = Image.FromStream(ms);
                        }
                    }
                }
                catch (Exception)
                {
                    optionImg.Image = Image.FromFile("placeholder.png");
                }
            }
        }

        private void reasearch_Click(object sender, EventArgs e)
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
    }
}
