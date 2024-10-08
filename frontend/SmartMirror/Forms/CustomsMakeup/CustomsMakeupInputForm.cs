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
using SmartMirror.Config;
using SmartMirror.Helpers;
using SmartMirror.Models;
using System.Reflection.Metadata;


namespace SmartMirror
{
    public partial class CustomsMakeupInputForm : Form
    {
        SmartMirror.Helpers.FindForm formFinder = new SmartMirror.Helpers.FindForm();
        public bool isFomr = false;
        public CustomsGoodsData[] EyebrowList;
        public CustomsGoodsData[] SkinList;
        public CustomsGoodsData[] LipList;
        public CustomsGoodsData[] chooseGoodsList = new CustomsGoodsData[3];
        public CustomsGoodsData lipGoods = null;
        public CustomsGoodsData eyeGoods = null;
        public CustomsGoodsData skinGoods = null;


        public int lipGoodsIndex = -1;
        public int eyeGoodsIndex = -1;
        public int skinGoodsIndex = -1;

        public CustomsMakeupInputForm()
        {
            InitializeComponent();
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
        private async void CustomsMakeupInputForm_Load(object sender, EventArgs e)
        {
            clickList.Controls.Clear();
            Console.WriteLine("커스텀 인풋 폼 로드");

            if (isFomr) //폼이 열려써었다 
            {
                this.goodsListVisible();
            }
            else // 폼이 여린적 없다 => api요청, 정보 캐싱
            {
                isFomr = true;
                // api 호출해서 메이크업 스타일 사진 받아오기
                // makeup img 변경
                // img url, 스타일 명, 상품 리스트와 페이지
                HttpClient client = new HttpClient();

                String apiUrl = $"{ApiConfig.url}/1/custom";

                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    String responseBody = await response.Content.ReadAsStringAsync();

                    JObject responseJson = JObject.Parse(responseBody);

                    var eyebrowList = responseJson["data"]["eyebrowList"];
                    var skinList = responseJson["data"]["skinList"];
                    var lipList = responseJson["data"]["lipList"];

                    EyebrowList = new CustomsGoodsData[eyebrowList.Count()];
                    SkinList = new CustomsGoodsData[skinList.Count()];
                    LipList = new CustomsGoodsData[lipList.Count()];

                    for (int i = 0; i < eyebrowList.Count(); i++)
                    {
                        CustomsGoodsData customsGoodsData = new CustomsGoodsData();
                        customsGoodsData.optionId = (int)eyebrowList[i]["optionId"];
                        customsGoodsData.category = "eye";
                        customsGoodsData.optionColor = eyebrowList[i]["optionColor"].ToString();
                        customsGoodsData.goodsName = eyebrowList[i]["goodsName"].ToString();
                        customsGoodsData.optionName = eyebrowList[i]["optionName"].ToString();
                        customsGoodsData.optionImage = eyebrowList[i]["optionImage"].ToString();
                        customsGoodsData.isInMarket = (bool)eyebrowList[i]["isInMarket"];
                        customsGoodsData.location = eyebrowList[i]["location"]["name"].ToString();
                        customsGoodsData.stock = (int)eyebrowList[i]["stock"];
                        EyebrowList[i] = customsGoodsData;

                    }

                    for (int i = 0; i < skinList.Count(); i++)
                    {
                        CustomsGoodsData customsGoodsData = new CustomsGoodsData();
                        customsGoodsData.optionId = (int)skinList[i]["optionId"];
                        customsGoodsData.category = "skin";
                        customsGoodsData.optionColor = skinList[i]["optionColor"].ToString();
                        customsGoodsData.goodsName = skinList[i]["goodsName"].ToString();
                        customsGoodsData.optionName = skinList[i]["optionName"].ToString();
                        customsGoodsData.optionImage = skinList[i]["optionImage"].ToString();
                        customsGoodsData.isInMarket = (bool)skinList[i]["isInMarket"];
                        customsGoodsData.location = skinList[i]["location"]["name"].ToString();
                        customsGoodsData.stock = (int)skinList[i]["stock"];
                        SkinList[i] = customsGoodsData;
                    }

                    for (int i = 0; i < lipList.Count(); i++)
                    {
                        CustomsGoodsData customsGoodsData = new CustomsGoodsData();
                        customsGoodsData.optionId = (int)lipList[i]["optionId"];
                        customsGoodsData.category = "lip";
                        customsGoodsData.optionColor = lipList[i]["optionColor"].ToString();
                        customsGoodsData.goodsName = lipList[i]["goodsName"].ToString();
                        customsGoodsData.optionName = lipList[i]["optionName"].ToString();
                        customsGoodsData.optionImage = lipList[i]["optionImage"].ToString();
                        customsGoodsData.isInMarket = (bool)lipList[i]["isInMarket"];
                        customsGoodsData.location = lipList[i]["location"]["name"].ToString();
                        customsGoodsData.stock = (int)lipList[i]["stock"];
                        LipList[i] = customsGoodsData;
                    }

                    this.goodsListVisible();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        // 화면에 굿즈 리스트 넣는 메서드
        public async void goodsListVisible()
        {
            CustomsGoodsData[][] customsGoodsData = { EyebrowList, SkinList, LipList };
            HttpClient client = new HttpClient();

            foreach (var customsGoods in customsGoodsData)
            {
                int buttonWidth = 130;
                int buttonHeight = 130;
                int buttonsPerRow = 6; // 한 줄에 배치할 버튼 수
                int margin = 10; // 버튼 사이의 간격
                int x = margin; // 초기 X 좌표
                int y = margin; // 초기 Y 좌표
                int buttonCount = 0; // 버튼 카운트

                foreach (var customsGood in customsGoods)
                {
                    string optionImage = customsGood.optionImage;
                    string goodsName = customsGood.goodsName;
                    string optionName = customsGood.optionName;


                    string optionColorString = customsGood.optionColor;
                    string[] colors = optionColorString.Split(',');
                    int[] optionColor = new int[3];

                    for (int i = 0; i < colors.Length; i++)
                    {
                        if (int.TryParse(colors[i], out int colorValue))
                        {
                            optionColor[i] = colorValue;
                        }
                        else
                        {
                            Console.WriteLine("Invalid color value.");
                        }
                    }

                    // 버튼 생성
                    Button button = new Button();
                    button.Text = optionName;
                    button.Width = 130;
                    button.Height = 130;
                    button.TextAlign = ContentAlignment.BottomCenter;

                    // 테두리 없애기
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;

                    // 버튼에 클릭 이벤트 추가
                    button.Click += (s, args) => goodsClick(customsGood);

                    // 옵션 이미지 PictureBox 생성
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Size = new Size(100, 100);

                    // PictureBox의 크기 및 부모 컨트롤 크기 가져오기
                    int parentWidth = button.Width;
                    int pictureBoxWidth = pictureBox.Width;

                    // 중앙 정렬 X 좌표
                    int centerX = (parentWidth - pictureBoxWidth) / 2;

                    // 위로 픽셀 이동
                    int offsetY = 0;
                    int centerY = pictureBox.Location.Y - offsetY;

                    // PictureBox의 위치 설정
                    pictureBox.Location = new Point(centerX, centerY);


                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                    pictureBox.BackColor = Color.FromArgb(optionColor[0], optionColor[1], optionColor[2]);
                    pictureBox.Enabled = false;

                    button.Controls.Add(pictureBox);

                    // 버튼의 위치 설정 (한 줄에 4개씩 배치)
                    button.Location = new Point(x, y);
                    clickList.Controls.Add(button);

                    // 버튼의 위치 설정 (한 줄에 4개씩 배치)
                    button.Location = new Point(x, y);
                    if (customsGoods == EyebrowList)
                    {
                        eye.Controls.Add(button);
                    }
                    else if (customsGoods == SkinList)
                    {
                        skin.Controls.Add(button);
                    }
                    else
                    {
                        lip.Controls.Add(button);
                    }

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
                }
            }
        }

        private async void goodsClick(CustomsGoodsData goods)
        {
            switch (goods.category)
            {
                case "lip":
                    lipGoods = goods;
                    chooseGoodsList[0] = goods;
                    break;
                case "eye":
                    eyeGoods = goods;
                    chooseGoodsList[1] = goods;
                    break;
                case "skin":
                    skinGoods = goods;
                    chooseGoodsList[2] = goods;
                    break;
            }
            clickList.Controls.Clear();


            HttpClient client = new HttpClient();

            int buttonWidth = 130;
            int buttonHeight = 130;
            int buttonsPerRow = 3; // 한 줄에 배치할 버튼 수
            int margin = 10; // 버튼 사이의 간격
            int x = margin; // 초기 X 좌표
            int y = margin; // 초기 Y 좌표
            int buttonCount = 0; // 버튼 카운트


            foreach (var customsGood in chooseGoodsList)
            {
                if (customsGood != null)
                {
                    string optionImage = customsGood.optionImage;
                    string goodsName = customsGood.goodsName;
                    string optionName = customsGood.optionName;

                    string optionColorString = customsGood.optionColor;
                    string[] colors = optionColorString.Split(',');
                    int[] optionColor = new int[3];

                    for (int i = 0; i < colors.Length; i++)
                    {
                        if (int.TryParse(colors[i], out int colorValue))
                        {
                            optionColor[i] = colorValue;
                        }
                        else
                        {
                            Console.WriteLine("Invalid color value.");
                        }
                    }

                    // 버튼 생성
                    Button button = new Button();
                    button.Text = optionName;
                    button.Width = 130;
                    button.Height = 130;
                    button.TextAlign = ContentAlignment.BottomCenter;

                    // 테두리 없애기
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;

                    // 버튼에 클릭 이벤트 추가
                    button.Click += (s, args) => chooseGoods_click(customsGood, buttonCount);

                    // 옵션 이미지 PictureBox 생성
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Size = new Size(100, 100);

                    // PictureBox의 크기 및 부모 컨트롤 크기 가져오기
                    int parentWidth = button.Width;
                    int pictureBoxWidth = pictureBox.Width;

                    // 중앙 정렬 X 좌표
                    int centerX = (parentWidth - pictureBoxWidth) / 2;

                    // 위로 픽셀 이동
                    int offsetY = 0;
                    int centerY = pictureBox.Location.Y - offsetY;

                    // PictureBox의 위치 설정
                    pictureBox.Location = new Point(centerX, centerY);

                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                    pictureBox.BackColor = Color.FromArgb(optionColor[0], optionColor[1], optionColor[2]);
                    pictureBox.Enabled = false;

                    button.Controls.Add(pictureBox);

                    // 버튼의 위치 설정 (한 줄에 4개씩 배치)
                    button.Location = new Point(x, y);
                    Console.WriteLine(buttonCount);
                    clickList.Controls.Add(button);


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
                }
            }
        }

        public void chooseGoods_click(CustomsGoodsData customsGood, int buttonCount)
        {
            // 현재 clickList에 있는 버튼과 일치하는지 확인하고 제거
            Control controlToRemove = null;

            // chooseGoodsList의 인덱스와 clickList.Controls가 동일하다는 보장이 없을 수 있으므로, 직접 해당 컨트롤을 찾음
            foreach (Control control in clickList.Controls)
            {
                if (control.Text == customsGood.optionName) // 예시: 버튼의 Text 속성이 선택된 goods의 이름과 같을 경우
                {
                    controlToRemove = control;
                    break;
                }
            }

            if (controlToRemove != null)
            {
                clickList.Controls.Remove(controlToRemove);
                Console.WriteLine($"{controlToRemove.Text} 버튼이 삭제되었습니다.");
                switch (customsGood.category)
                {
                    case "lip":
                        {
                            chooseGoodsList[0] = null;
                            break;
                        }
                    case "eye":
                        {
                            chooseGoodsList[1] = null;
                            break;
                        }
                    case "skin":
                        {
                            chooseGoodsList[2] = null;
                            break;
                        }
                }
            }
            else
            {
                Console.WriteLine("삭제할 버튼을 찾지 못했습니다.");
            }

            // 전체 버튼의 위치를 재배치 (삭제된 버튼의 위치를 고려)
            RepositionButtons();
        }

        // 버튼 삭제 후 전체 버튼의 위치를 재배치하는 메서드
        private void RepositionButtons()
        {
            int buttonWidth = 130;
            int buttonHeight = 130;
            int buttonsPerRow = 3; // 한 줄에 배치할 버튼 수
            int margin = 10; // 버튼 사이의 간격
            int x = margin; // 초기 X 좌표
            int y = margin; // 초기 Y 좌표
            int buttonCount = 0; // 버튼 카운트

            foreach (Control control in clickList.Controls)
            {
                // 버튼의 위치 재설정
                control.Location = new Point(x, y);
                buttonCount++;

                if (buttonCount % buttonsPerRow == 0)
                {
                    // 한 줄에 buttonsPerRow 개의 버튼이 추가되었으면 다음 줄로 이동
                    x = margin;
                    y += buttonHeight + margin;
                }
                else
                {
                    // 같은 줄에 다음 버튼 위치로
                    x += buttonWidth + margin;
                }
            }
        }


        private void label6_Click(object sender, EventArgs e)
        {
            QR.Visible = false;
            QRpicture.Visible = false;
            chooseGoodsList[0] = null;
            chooseGoodsList[1] = null;
            chooseGoodsList[2] = null;
            foreach (Control control in clickList.Controls.Cast<Control>().ToList())
            {
                // Button 타입의 컨트롤만 삭제
                if (control is Button)
                {
                    clickList.Controls.Remove(control);
                }
            }

            // 커스텀 화장 합성 부분 클로즈 해줘야함
            Form openCustomsMakeupOutputForm = formFinder.findForm("CustomsMakeupOutputForm");
            if (openCustomsMakeupOutputForm != null)
            {
                openCustomsMakeupOutputForm.Close();
            }
            this.Hide();

            Form openedForm = formFinder.findForm("MakeupOutputFOrm");
            openedForm.Hide();

            Form openMainInputForm = formFinder.findForm("MainInputForm");
            Form openMainOutForm = formFinder.findForm("MainOutputForm");

            openMainInputForm.Show();
            openMainOutForm.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            QR.Visible = false;
            QRpicture.Visible = false;
            chooseGoodsList[0] = null;
            chooseGoodsList[1] = null;
            chooseGoodsList[2] = null;
            foreach (Control control in clickList.Controls.Cast<Control>().ToList())
            {
                // Button 타입의 컨트롤만 삭제
                if (control is Button)
                {
                    clickList.Controls.Remove(control);
                }
            }

            Form openMakeupOutputForm = formFinder.findForm("MakeupOutputForm");
            openMakeupOutputForm.Show();

            // CustomsMakeupOutForm 클로즈
            Form openCustomsMakeupOutputForm = formFinder.findForm("CustomsMakeupOutputForm");
            if (openCustomsMakeupOutputForm != null)
            {
                openCustomsMakeupOutputForm.Close();
            }
            MakeupInputForm openMakeupInputForm = Application.OpenForms["MakeupInputForm"] as MakeupInputForm;
            this.Hide();
            openMakeupInputForm.Show();
        }

        private void lipListLabel_Click(object sender, EventArgs e)
        {
            lip.Visible = true;
            eye.Visible = false;
            skin.Visible = false;
        }

        private void eyebrowListLabel_Click(object sender, EventArgs e)
        {
            lip.Visible = false;
            eye.Visible = true;
            skin.Visible = false;
        }

        private void skinListLabel_Click(object sender, EventArgs e)
        {
            lip.Visible = false;
            eye.Visible = false;
            skin.Visible = true;
        }

        private async void makeupStart_Click(object sender, EventArgs e)
        {
            if (chooseGoodsList[0] == null && chooseGoodsList[2] == null && chooseGoodsList[2] == null)
            {
                clickplease.Visible = true;
                await Task.Delay(3000);
                clickplease.Visible = false;

            }
            else
            {

                CustomsMakeupOutputForm openCustomsMakeupOutputForm = Application.OpenForms["CustomsMakeupOutputForm"] as CustomsMakeupOutputForm;
                if (openCustomsMakeupOutputForm == null)
                {
                    int outputMonitorIndex = 1;
                    Screen output = Screen.AllScreens[outputMonitorIndex];
                    CustomsMakeupOutputForm customsMakeupOutputForm = new CustomsMakeupOutputForm();
                    customsMakeupOutputForm.StartPosition = FormStartPosition.Manual;
                    customsMakeupOutputForm.Location = output.Bounds.Location;
                    customsMakeupOutputForm.apiRequest(chooseGoodsList);
                    customsMakeupOutputForm.Show();
                }
                else
                {
                    openCustomsMakeupOutputForm.apiRequest(chooseGoodsList);
                    openCustomsMakeupOutputForm.Show();
                }
                QR.Visible = true;
                QRpicture.Visible = true;


                Form openMakeupOutputForm = formFinder.findForm("MakeupOutputForm");
                openMakeupOutputForm.Hide();
            }
        }

        private void QR_Click(object sender, EventArgs e)
        {
            CustomsMakeupOutputForm openCustomsMakeupOutputForm = Application.OpenForms["CustomsMakeupOutputForm"] as CustomsMakeupOutputForm;
            if (openCustomsMakeupOutputForm != null && openCustomsMakeupOutputForm.Visible)
            {
                openCustomsMakeupOutputForm.qr_click(chooseGoodsList);
            }
        }
    }
}
