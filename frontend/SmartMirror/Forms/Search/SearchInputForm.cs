using System.Drawing.Drawing2D;
using System.Diagnostics;
using NAudio.Wave;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Newtonsoft.Json.Linq;
using SmartMirror.Helpers;

namespace SmartMirror
{
    public partial class SearchInputForm : Form
    {
        private AudioRecorder audioRecorder;
        private SearchOutputForm outputForm;
        private bool isRecording = false;

        private int outputMonitor = 1;
        private int inputMonitor = 2;
        private Screen[] screens = Screen.AllScreens;

        public SearchInputForm(SearchOutputForm outputForm)
        {
            InitializeComponent();
            this.outputForm = outputForm;
            outputForm.textBox1.KeyDown += textBox1_KeyDown;

            audioRecorder = new AudioRecorder("recordedAudio.wav");
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            int panelWidth = voice.Width;
            int panelHeight = voice.Height;

            GraphicsPath path = BoarderStyle.RoundSquare(panelWidth, panelHeight);

            // 패널의 모양을 둥근 모서리로 설정
            voice.Region = new Region(path);
            keybaord.Region = new Region(path);
        }

        private void ShowOnScreenKeyboard()
        {
            KeyboardHelper.ShowOnScreenKeyboard(2, Screen.AllScreens);
            outputForm.textBox1.Focus();
        }

        public async void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                KeyboardHelper.CloseOnScreenKeyboard(); // 가상 키보드를 닫음
                e.Handled = true; // 이벤트 처리 완료
                e.SuppressKeyPress = true; // Enter 키 입력을 텍스트 박스에 전달하지 않음

                string apiResponse = await SearchApi.CallSearchApi(outputForm.textBox1.Text, 0);

                // API 호출 및 화면 전환
                change(apiResponse);
            }
        }

        private void change(string apiResponse)
        {
            if (apiResponse != null)
            {
                var screens = Screen.AllScreens;
                var (primaryScreen, secondaryScreen) = FormHelper.SetupScreens(outputMonitor, ref inputMonitor, screens);

                SearchInfoOutputForm searchInfoOutputForm = new SearchInfoOutputForm();
                SearchInfoInputForm searchInputForm = new SearchInfoInputForm(apiResponse, searchInfoOutputForm);

                FormHelper.SwitchToForm(searchInputForm, searchInfoOutputForm, primaryScreen, secondaryScreen);

                this.Hide();

                if (outputForm != null && !outputForm.IsDisposed)
                {
                    outputForm.Hide(); // MainOutputForm 숨기기
                }
            }
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            ShowOnScreenKeyboard();
        }

        private async void voice_Click(object sender, EventArgs e)
        {

            if (!isRecording)
            {
                label2.Text = "녹음 중지";
                audioRecorder.StartRecording();
            }
            else
            {
                label2.Text = "음성 검색";
                audioRecorder.StopRecording();

                await Task.Delay(100);
                string result = await audioRecorder.SendPostRequestWithRecordedAudio();
                change(result);
            }

            isRecording = !isRecording;
        }

        private void home_Click(object sender, EventArgs e)
        {
            this.Hide();

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
