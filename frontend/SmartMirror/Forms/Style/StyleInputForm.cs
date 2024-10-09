using Newtonsoft.Json.Linq;
using SmartMirror.Models;
using SmartMirror.Config;
using SmartMirror.Helpers;

namespace SmartMirror
{
    public partial class StyleInputForm : Form
    {
        int makeupStyle = -1;

        private int outputMonitor = 1;
        private int inputMonitor = 2;
        private Screen[] screens = Screen.AllScreens;

        public StyleData[] SyntheticResponseList;

        private bool flag;

        public StyleInputForm()
        {
            InitializeComponent();
            SyntheticResponseList = new StyleData[20];
        }

        public void arrayRest()
        {
            SyntheticResponseList = new StyleData[20]; // 배열 초기화
            Console.WriteLine("배열이 초기화");
        }

        private async void StyleInputForm_Load(object sender, EventArgs e)
        {
            Console.WriteLine("스타일 인풋 폼 로드");
            // api 호출해서 메이크업 스타일 사진 받아오기
            // makeup img 변경
            // img url, 스타일 명, 상품 리스트와 페이지
            HttpClient client = new HttpClient();

            String apiUrl = $"{ApiConfig.url}/1/styles?page=0&size=10";

            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                String responseBody = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(responseBody);

                JObject responseJson = JObject.Parse(responseBody);

                var conetnt = responseJson["data"]["styleInfoList"]["content"];

                int buttonWidth = 150;
                int buttonHeight = 150;
                int buttonsPerRow = 4; // 한 줄에 배치할 버튼 수
                int margin = 10; // 버튼 사이의 간격
                int x = margin; // 초기 X 좌표
                int y = margin; // 초기 Y 좌표
                int buttonCount = 0; // 버튼 카운트

                foreach (var style in conetnt)
                {
                    Console.WriteLine(style["styleName"]);
                    String styleName = style["styleName"].ToString();
                    String styleImage = style["styleImage"].ToString();

                    // 버튼 생성
                    Button button = new Button();
                    button.Text = styleName;
                    button.Width = 150;
                    button.Height = 150;
                    button.TextAlign = ContentAlignment.BottomCenter;

                    // 테두리 없애기
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;

                    // 버튼에 클릭 이벤트 추가
                    button.Click += (s, args) => style_Click(int.Parse(style["styleId"].ToString())); // 클릭 시 styleNum을 전달

                    // 이미지를 버튼에 추가
                    try
                    {
                        Image image = Image.FromStream(await client.GetStreamAsync(styleImage));
                        button.Image = new Bitmap(image, new Size(110, 120)); // 이미지를 크기에 맞게 조정
                        button.ImageAlign = ContentAlignment.TopCenter;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"이미지 로드 실패: {ex.Message}");
                    }

                    // 버튼의 위치 설정 (한 줄에 4개씩 배치)
                    button.Location = new Point(x, y);
                    panel9.Controls.Add(button);

                    buttonCount++;
                    if (buttonCount % buttonsPerRow == 0)
                    {
                        // 한 줄에 4개 버튼이 추가되었으면 다음 줄로
                        x = margin;
                        y += buttonHeight + margin;
                    }
                    else
                    {
                        // 같은 줄에 다음 버튼 위치로
                        x += buttonWidth + margin;
                    }
                    panel9.Controls.Add(button);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void style_Click(int styleNum)
        {
            location.Visible = true;

            MakeupOutputForm openMakeupOutputForm = Application.OpenForms["MakeupOutputForm"] as MakeupOutputForm;
            openMakeupOutputForm.Hide();

            SyntheticOutput openSyntheticOutput = Application.OpenForms["SyntheticOutput"] as SyntheticOutput;

            if (openSyntheticOutput != null)
            {
                openSyntheticOutput.Close();
            }
            int outputMonitorIndex = 1;

            Screen output = Screen.AllScreens[outputMonitorIndex];
            SyntheticOutput syntheticOutput = new SyntheticOutput(styleNum); // 나중에 수정 해야함
            syntheticOutput.StartPosition = FormStartPosition.Manual;
            syntheticOutput.Location = output.Bounds.Location;
            syntheticOutput.Show();
        }

        private void home_Click(object sender, EventArgs e)
        {
            location.Visible = false;

            SyntheticOutput openSyntheticOutput = Application.OpenForms["SyntheticOutput"] as SyntheticOutput;

            // 스크린 설정 호출
            var screens = Screen.AllScreens;
            var (primaryScreen, secondaryScreen) = FormHelper.SetupScreens(outputMonitor, ref inputMonitor, screens);

            MainOutputForm mainOutputForm = new MainOutputForm();
            MainInputForm inputForm = new MainInputForm(mainOutputForm);

            FormHelper.SwitchToForm(inputForm, mainOutputForm, primaryScreen, secondaryScreen);

            if (openSyntheticOutput != null && !openSyntheticOutput.IsDisposed)
            {
                openSyntheticOutput.Hide();
            }

            this.Close();
        }

        private void camera_Click(object sender, EventArgs e)
        {
            location.Visible = false;

            if (screens.Length == 2)
            {
                inputMonitor = 0; // 2개의 모니터 중 첫 번째로 설정
            }

            // primary와 secondary 스크린 설정
            Screen primaryScreen = screens[inputMonitor];
            Screen secondaryScreen = screens[outputMonitor];

            MakeupOutputForm openMakeupOutputForm = Application.OpenForms["MakeupOutputForm"] as MakeupOutputForm;

            if (openMakeupOutputForm == null)
            {
                MakeupOutputForm makeupOutputForm = new MakeupOutputForm();
                makeupOutputForm.StartPosition = FormStartPosition.Manual;
                makeupOutputForm.Location = secondaryScreen.Bounds.Location;
                makeupOutputForm.Size = new Size(secondaryScreen.Bounds.Width, secondaryScreen.Bounds.Height);

                Screen mirror = Screen.AllScreens[outputMonitor];

                Console.WriteLine("연결");
                makeupOutputForm.Show();

                MakeupInputForm inputForm = new MakeupInputForm(makeupOutputForm);
                inputForm.StartPosition = FormStartPosition.Manual;
                inputForm.Location = primaryScreen.Bounds.Location;
                inputForm.Size = new Size(primaryScreen.Bounds.Width, primaryScreen.Bounds.Height);
                inputForm.Show();

                this.Hide();
                //outputForm.Hide();
            }
            else
            {
                openMakeupOutputForm.StartPosition = FormStartPosition.Manual;
                openMakeupOutputForm.Location = secondaryScreen.Bounds.Location;
                openMakeupOutputForm.Size = new Size(secondaryScreen.Bounds.Width, secondaryScreen.Bounds.Height);
                openMakeupOutputForm.Show();
                openMakeupOutputForm.CaptureImage();

                // makeupinput  
                MakeupInputForm openMakeupInputForm = Application.OpenForms["MakeupInputForm"] as MakeupInputForm;
                openMakeupInputForm.StartPosition = FormStartPosition.Manual;
                openMakeupInputForm.Location = primaryScreen.Bounds.Location;
                openMakeupInputForm.Size = new Size(primaryScreen.Bounds.Width, primaryScreen.Bounds.Height);
                openMakeupInputForm.Show();

                // MaininputForm 숨기기
                this.Hide();
            }
        }

        private void leftBtn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("leftBTn 클릭");
            SyntheticOutput openSyntheticOutput = Application.OpenForms["SyntheticOutput"] as SyntheticOutput;
            openSyntheticOutput.changePage(0);
        }

        private void rightBtn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("rightBtn 클릭");
            SyntheticOutput openSyntheticOutput = Application.OpenForms["SyntheticOutput"] as SyntheticOutput;
            openSyntheticOutput.changePage(1);
        }

        private void location_Click(object sender, EventArgs e)
        {
            SyntheticOutput openSyntheticOutput = Application.OpenForms["SyntheticOutput"] as SyntheticOutput;
            if (openSyntheticOutput != null)
            {
                openSyntheticOutput.visbleLocation();
            }
        }

        private void mirror_Click(object sender, EventArgs e)
        {
            SyntheticOutput openSyntheticOutput = Application.OpenForms["SyntheticOutput"] as SyntheticOutput;
            openSyntheticOutput.panel3.Dock = DockStyle.Fill;
            openSyntheticOutput.panel3.Visible = flag;

            flag = !flag;
        }
    }
}
