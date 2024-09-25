using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SmartMirror
{

    public partial class StyleInputForm : Form
    {
        int makeupStyle = -1;
        private int outputMonitor = 1;
        private int inputMonitor = 2;
        private Screen[] screens = Screen.AllScreens;
        public String[] SyntheticResponseList;


        //SearchOutputForm outputForm;

        //public StyleInputForm(SearchOutputForm outputForm)
        public StyleInputForm()

        {
            //this.outputForm = outputForm;
            InitializeComponent();
            SyntheticResponseList = new string[100];
            this.VisibleChanged += new EventHandler(Form_VisibleChanged);
            //SyntheticResponseList = new string[100];
            //Console.WriteLine("메이크업 선택 창 또 뜸");
            //Console.WriteLine(SyntheticResponseList[0]);
            //Console.WriteLine(SyntheticResponseList[1]);
            //Console.WriteLine(SyntheticResponseList[2]);
            //Console.WriteLine(SyntheticResponseList[3]);
            //Console.WriteLine(SyntheticResponseList[4]);

        }

        private void Form_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible) // 폼이 다시 보여질 때
            {
                SyntheticResponseList = new string[100]; // 배열 초기화
                Console.WriteLine("배열이 다시 초기화되었습니다.");
            }
        }
        private async void StyleInputForm_Load(object sender, EventArgs e)
        {
            Console.WriteLine("스타일 인풋 폼 로드");
            // api 호출해서 메이크업 스타일 사진 받아오기
            // makeup img 변경
            // img url, 스타일 명, 상품 리스트와 페이지
            HttpClient client = new HttpClient();

            String apiUrl = "http://192.168.100.147:8080/smartmirrorApi/market/1/styles?page=0&size=10";

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

            Console.WriteLine("@@@@@@@@@@@@@@@@@@@");
            if (SyntheticResponseList[styleNum] != null)
            {
            Console.WriteLine("이미지 내용어ㅗ엉");

            }
                    



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


        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();

            MakeupOutputForm openedForm = Application.OpenForms["MakeupOutputFOrm"] as MakeupOutputForm;
            openedForm.Hide();

            MainInputForm openMainInputForm = Application.OpenForms["MainInputForm"] as MainInputForm;
            MainOutputForm openMainOutForm = Application.OpenForms["MainOutputForm"] as MainOutputForm;

            openMainInputForm.Show();
            openMainOutForm.Show();
        }
    }


}
