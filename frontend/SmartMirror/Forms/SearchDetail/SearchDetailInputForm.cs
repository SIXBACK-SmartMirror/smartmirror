using Newtonsoft.Json.Linq;
using SmartMirror.Helpers;
using SmartMirror.Models;

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

        private bool flag = false;
        private bool flag2 = false;

        public SearchDetailInputForm(SearchDetailOutputForm outputForm, GoodsData goodsData, string options)
        {
            InitializeComponent();

            this.outputForm = outputForm;
            this.goodsData = goodsData;

            // JSON 문자열을 JObject로 파싱
            this.jsonOptions = JObject.Parse(options);

            // 패널 초기화
            InitializePanelContainer();

            PopulatePanelWithOptions(this.jsonOptions);

            outputForm.panel3.Dock = DockStyle.Fill;
        }

        private void SearchDetailInputForm_Load(object sender, EventArgs e)
        {
            brand.Text = goodsData.BrandName;
            outputForm.brand.Text = goodsData.BrandName;

            name.Text = goodsData.GoodsName;
            outputForm.name.Text = goodsData.GoodsName;

            outputForm.price.Text = $"{int.Parse(goodsData.GoodsPrice):N0}원~";

            outputForm.discountPrice.Text = $"{int.Parse(goodsData.GoodsDiscountPrice):N0}원~";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var imageBytes = client.GetByteArrayAsync(goodsData.GoodsImage).Result;
                    using (var ms = new System.IO.MemoryStream(imageBytes))
                    {
                        outputForm.img.Image = Image.FromStream(ms);
                    }
                }
            }
            catch (Exception)
            {
                outputForm.img.Image = Image.FromFile("placeholder.png");
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

        private void PopulatePanelWithOptions(JObject jsonResponse)
        {
            // 기존 Panel 초기화
            panel1.Controls.Clear();
            panel1.AutoScroll = true; // 스크롤 가능하도록 설정

            // JSON 데이터에서 optionDtoList 항목을 가져옴
            var optionList = jsonResponse["data"]["optionDtoList"];

            int yPosition = 0; // 각 패널의 위치를 조절하기 위한 y 좌표

            // optionDtoList의 항목을 Panel로 추가
            foreach (var option in optionList)
            {
                string optionName = option["optionName"].ToString();
                int optionId = (int)option["optionId"];

                // 항목을 나타낼 Panel 생성
                Panel optionPanel = new Panel();
                optionPanel.Size = new Size(panel1.Width - 20, 50); // 패널 크기 설정
                optionPanel.Location = new Point(0, yPosition); // 위치 설정
                optionPanel.BackColor = Color.Gray;
                optionPanel.Margin = new Padding(5); // 여백 설정
                optionPanel.Tag = optionId; // Panel의 Tag에 optionId를 저장하여 나중에 참조 가능

                // Panel 내부의 Label 생성
                Label optionLabel = new Label();
                optionLabel.Text = optionName;
                optionLabel.Dock = DockStyle.Fill;
                optionLabel.TextAlign = ContentAlignment.MiddleCenter;
                optionLabel.ForeColor = Color.Black;

                // Panel에 Label 추가
                optionPanel.Controls.Add(optionLabel);

                // Panel 클릭 이벤트 핸들러 추가
                optionPanel.Click += OptionPanel_Click;
                optionLabel.Click += OptionPanel_Click; // Label 클릭 시에도 같은 핸들러 사용


                // Panel을 패널1에 추가
                panel1.Controls.Add(optionPanel);

                // 다음 Panel 위치 설정을 위한 yPosition 조정
                yPosition += optionPanel.Height + 10; // 패널의 높이와 간격만큼 y 좌표 증가
            }

            // 기본적으로 첫 번째 항목 선택
            if (panel1.Controls.Count > 0)
            {
                SelectOptionPanel((Panel)panel1.Controls[0]);
            }
        }

        // 각 Panel 클릭 이벤트 핸들러
        private void OptionPanel_Click(object sender, EventArgs e)
        {
            // 클릭된 객체가 Panel이 아닌 경우 (예: Label), 부모 Panel을 가져옴
            Panel clickedPanel = sender as Panel ?? ((Control)sender).Parent as Panel;

            // clickedPanel이 null인지 확인
            if (clickedPanel == null)
            {
                MessageBox.Show("클릭된 패널이 null입니다. 확인해보세요.");
                return;
            }

            // 선택된 항목을 시각적으로 구분하기 위해 색상 변경
            SelectOptionPanel(clickedPanel);

            // Panel 내부의 Label을 가져옴
            Label optionLabel = clickedPanel.Controls.OfType<Label>().FirstOrDefault();

            // Label의 Text 값을 string으로 가져옴
            if (optionLabel != null)
            {
                string labelText = optionLabel.Text;
                CreateAndShowPanelForSelectedOption(labelText);
            }
        }


        // 선택된 Panel을 시각적으로 표시하는 메서드
        private void SelectOptionPanel(Panel selectedPanel)
        {
            if (selectedPanel == null)
            {
                MessageBox.Show("선택된 패널이 없습니다. 패널이 null입니다.");
                return;
            }

            // 모든 패널의 배경색을 초기화
            foreach (Control control in panel1.Controls)
            {
                if (control is Panel)
                {
                    control.BackColor = Color.WhiteSmoke;
                }
            }

            // 선택된 패널의 배경색을 변경하여 시각적으로 구분
            selectedPanel.BackColor = Color.LightBlue;
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

                outputForm.optionName.Text = selectedOption["optionName"].ToString();
                outputForm.optionPrice.Text = $"{int.Parse(selectedOption["optionDiscountPrice"].ToString()):N0}원";
                outputForm.optionStock.Text = selectedOption["inMarket"].ToObject<bool>()
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
                            outputForm.optionImg.Image = Image.FromStream(ms);
                        }
                    }
                }
                catch (Exception)
                {
                    outputForm.optionImg.Image = Image.FromFile("placeholder.png");
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

        private void button1_Click(object sender, EventArgs e)
        {
            if(!flag)
            {
                button1.Text = "위치 켜기";
            }
            else
            {
                button1.Text = "위치 끄기";
            }
            outputForm.panel2.Visible = flag;
            flag = !flag;
        }

        private void mirror_Click(object sender, EventArgs e)
        {
            outputForm.panel3.Visible = flag2;
            flag2 = !flag2;
        }
    }
}
