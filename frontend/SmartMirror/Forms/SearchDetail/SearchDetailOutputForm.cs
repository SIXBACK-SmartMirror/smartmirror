﻿using Newtonsoft.Json.Linq;

namespace SmartMirror
{
    public partial class SearchDetailOutputForm : Form
    {

        public SearchDetailOutputForm(string responseData)
        {
            InitializeComponent();
            HighlightPanelsBasedOnLocation(responseData);
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

        // locationList 데이터를 받아 패널을 업데이트하는 메서드
        private async void HighlightPanelsBasedOnLocation(string jsonResponse)
        {
            try
            {
                // JSON 데이터를 파싱하여 locationList 가져오기
                JObject json = JObject.Parse(jsonResponse);
                var locationList = json["data"]["locationList"];

                // locationList를 순회하면서 패널의 색상과 텍스트를 업데이트
                foreach (var locationData in locationList)
                {
                    var locationNode = locationData["location"];
                    if (locationNode == null || locationNode.Type == JTokenType.Null)
                    {
                        // location이 null일 때는 해당 항목을 무시하거나 기본 처리
                        continue;
                    }

                    // location 객체에서 name 값 가져오기
                    string locationName = locationData["location"]["name"].ToString();
                    int stock = (int)locationData["stock"];

                    // 패널 이름으로 기존 패널 찾기
                    Panel panel = FindPanelByName(locationName);

                    if (panel != null)
                    {
                        Console.WriteLine(panel.Name);

                        while (true)
                        {
                            panel.BackColor = stock > 0 ? Color.FromArgb(130, 220, 40) : Color.Yellow;
                            await Task.Delay(500);
                            panel.BackColor = Color.FromArgb(231, 231, 231);
                            await Task.Delay(200);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 패널 이름으로 기존 패널을 찾는 메서드
        private Panel FindPanelByName(string panelName)
        {
            // Form 내의 모든 패널 중에서 이름이 일치하는 패널을 찾음
            Panel panel = this.Controls.Find(panelName, true).FirstOrDefault() as Panel;
            return panel;
        }
    }
}
