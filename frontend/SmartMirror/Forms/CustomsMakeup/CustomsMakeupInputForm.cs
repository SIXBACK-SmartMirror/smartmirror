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
using Newtonsoft.Json.Linq;

namespace SmartMirror
{
    public partial class CustomsMakeupInputForm : Form
    {
        public CustomsMakeupInputForm()
        {
            InitializeComponent();
        }

        private void CustomsMakeupInputForm_Load(object sender, EventArgs e)
        {
            //Console.WriteLine("스타일 인풋 폼 로드");
            //// api 호출해서 메이크업 스타일 사진 받아오기
            //// makeup img 변경
            //// img url, 스타일 명, 상품 리스트와 페이지
            //HttpClient client = new HttpClient();

            //String apiUrl = "http://192.168.100.147:8080/smartMirrorApi/market/1/styles?page=0&size=10";

            //try
            //{
            //    HttpResponseMessage response = await client.GetAsync(apiUrl);
            //    response.EnsureSuccessStatusCode();

            //    String responseBody = await response.Content.ReadAsStringAsync();
            //    //Console.WriteLine(responseBody);

            //    JObject responseJson = JObject.Parse(responseBody);

            //    var conetnt = responseJson["data"]["styleInfoList"]["content"];

            //    int buttonWidth = 150;
            //    int buttonHeight = 150;
            //    int buttonsPerRow = 4; // 한 줄에 배치할 버튼 수
            //    int margin = 10; // 버튼 사이의 간격
            //    int x = margin; // 초기 X 좌표
            //    int y = margin; // 초기 Y 좌표
            //    int buttonCount = 0; // 버튼 카운트

            //    foreach (var style in conetnt)
            //    {
            //        Console.WriteLine(style["styleName"]);
            //        String styleName = style["styleName"].ToString();
            //        String styleImage = style["styleImage"].ToString();



            //        // 버튼 생성
            //        Button button = new Button();
            //        button.Text = styleName;
            //        button.Width = 150;
            //        button.Height = 150;
            //        button.TextAlign = ContentAlignment.BottomCenter;

            //        // 테두리 없애기
            //        button.FlatStyle = FlatStyle.Flat;
            //        button.FlatAppearance.BorderSize = 0;

            //        // 버튼에 클릭 이벤트 추가
            //        button.Click += (s, args) => style_Click(int.Parse(style["styleId"].ToString())); // 클릭 시 styleNum을 전달


            //        // 이미지를 버튼에 추가
            //        try
            //        {
            //            Image image = Image.FromStream(await client.GetStreamAsync(styleImage));
            //            button.Image = new Bitmap(image, new Size(110, 120)); // 이미지를 크기에 맞게 조정
            //            button.ImageAlign = ContentAlignment.TopCenter;
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine($"이미지 로드 실패: {ex.Message}");
            //        }

            //        // 버튼의 위치 설정 (한 줄에 4개씩 배치)
            //        button.Location = new Point(x, y);
            //        panel9.Controls.Add(button);

            //        buttonCount++;
            //        if (buttonCount % buttonsPerRow == 0)
            //        {
            //            // 한 줄에 4개 버튼이 추가되었으면 다음 줄로
            //            x = margin;
            //            y += buttonHeight + margin;
            //        }
            //        else
            //        {
            //            // 같은 줄에 다음 버튼 위치로
            //            x += buttonWidth + margin;
            //        }
            //        panel9.Controls.Add(button);
            //    }


            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            // 커스텀 화장 합성 부분 클로즈 해줘야함
            //CustomsMakeupOutputForm openCustomsMakeupOutputForm = Application.OpenForms["CustomsMakeupOutputForm"] as CustomsMakeupOutputForm;
            //if (openCustomsMakeupOutputForm != null)
            //{
            //    openCustomsMakeupOutputForm.Close(); 
            //}

            this.Hide();

            MakeupOutputForm openedForm = Application.OpenForms["MakeupOutputFOrm"] as MakeupOutputForm;
            openedForm.Hide();

            MainInputForm openMainInputForm = Application.OpenForms["MainInputForm"] as MainInputForm;
            MainOutputForm openMainOutForm = Application.OpenForms["MainOutputForm"] as MainOutputForm;

            openMainInputForm.Show();
            openMainOutForm.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MakeupOutputForm openMakeupOutputForm = Application.OpenForms["MakeupOutputForm"] as MakeupOutputForm;
            openMakeupOutputForm.Show();

            // CustomsMakeupOutForm 클로즈
            //SyntheticOutput openSyntheticOutput = Application.OpenForms["SyntheticOutput"] as SyntheticOutput;
            //if (openSyntheticOutput != null)
            //{
            //    openSyntheticOutput.Close();
            //}
            MakeupInputForm openMakeupInputForm = Application.OpenForms["MakeupInputForm"] as MakeupInputForm;
            this.Hide();
            openMakeupInputForm.Show();
        }
    }
}
