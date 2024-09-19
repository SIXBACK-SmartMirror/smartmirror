using System.Drawing.Drawing2D;
using System.Diagnostics;
using NAudio.Wave;

namespace SmartMirror
{
    public partial class SearchInputForm : Form
    {
        private WaveInEvent waveIn; // 마이크 입력
        private WaveFileWriter writer; // 녹음한 오디오를 파일로 저장
        private string outputFilePath = "recordedAudio.wav"; // 녹음 파일 경로
        private bool isRecording = false; // 녹음 상태 관리 변수
        private int outputMonitor = 1;
        private int inputMonitor = 2;

        private SearchOutputForm outputForm;
        private Process oskProcess;

        public SearchInputForm(SearchOutputForm outputForm)
        {
            InitializeComponent();
            this.outputForm = outputForm;
            outputForm.textBox1.KeyDown += textBox1_KeyDown;
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            // 둥근 모서리 반지름 설정
            int cornerRadius = 15;

            // 패널의 크기
            int panelWidth = voice.Width;
            int panelHeight = voice.Height;

            // GraphicsPath를 사용해 둥근 모서리 경로를 생성
            GraphicsPath path = new GraphicsPath();
            path.AddArc(new Rectangle(0, 0, cornerRadius, cornerRadius), 180, 90);  // 좌상단
            path.AddArc(new Rectangle(panelWidth - cornerRadius, 0, cornerRadius, cornerRadius), 270, 90); // 우상단
            path.AddArc(new Rectangle(panelWidth - cornerRadius, panelHeight - cornerRadius, cornerRadius, cornerRadius), 0, 90); // 우하단
            path.AddArc(new Rectangle(0, panelHeight - cornerRadius, cornerRadius, cornerRadius), 90, 90); // 좌하단
            path.CloseFigure();

            // 패널의 모양을 둥근 모서리로 설정
            mirror.Region = new Region(path);
            voice.Region = new Region(path);
            keybaord.Region = new Region(path);
        }

        private void ShowOnScreenKeyboard()
        {
            // 이미 실행 중인 가상 키보드가 있다면 종료
            CloseOnScreenKeyboard();

            oskProcess = Process.Start("osk.exe");
            outputForm.textBox1.Focus();
        }

        private void CloseOnScreenKeyboard()
        {
            foreach (var process in Process.GetProcessesByName("osk"))
            {
                process.Kill();
            }
        }

        // Enter 키를 감지하는 KeyDown 이벤트 핸들러
        public void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CloseOnScreenKeyboard(); // Enter 키를 누르면 가상 키보드를 종료
                e.Handled = true; // 이벤트 처리 완료
                e.SuppressKeyPress = true; // Enter 키 입력을 텍스트 박스에 전달하지 않음

                change();
            }
        }

        private void change()
        {
            this.Hide();

            Screen[] screens = Screen.AllScreens;

            if (screens.Length == 2)
            {
                inputMonitor = 1;
            }

            SearchInfoOutputForm searchInfoOutputForm = new SearchInfoOutputForm();

            Screen secondaryScreen = Screen.AllScreens[outputMonitor];
            searchInfoOutputForm.StartPosition = FormStartPosition.Manual;
            searchInfoOutputForm.Location = secondaryScreen.Bounds.Location;
            searchInfoOutputForm.Size = new Size(secondaryScreen.Bounds.Width, secondaryScreen.Bounds.Height);

            if (outputForm != null && !outputForm.IsDisposed)
            {
                outputForm.Hide(); // MainOutputForm 숨기기
            }

            // SearchOutputForm 표시
            searchInfoOutputForm.Show();

            SearchInfoInputForm searchInputForm = new SearchInfoInputForm();

            Screen primaryScreen = Screen.AllScreens[inputMonitor];
            searchInputForm.StartPosition = FormStartPosition.Manual;
            searchInputForm.Location = primaryScreen.Bounds.Location;
            searchInputForm.Size = new Size(primaryScreen.Bounds.Width, primaryScreen.Bounds.Height);
            searchInputForm.Show();
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            ShowOnScreenKeyboard();
        }

        private void voice_Click(object sender, EventArgs e)
        {

            if (!isRecording)
            {
                label2.Text = "녹음 중지";
                label5.Text = "'OO' 찾아줘~";

                StartRecording();
            }
            else
            {
                label5.Text = "움성으로 물건 찾기";
                label2.Text = "음성 검색";
                StopRecording();
            }

            isRecording = !isRecording;
        }

        private void mirror_Click(object sender, EventArgs e)
        {
            this.Hide();

            Screen[] screens = Screen.AllScreens;

            if (screens.Length == 2)
            {
                inputMonitor = 1;
            }

            MainOutputForm mainOutputForm = new MainOutputForm();

            Screen secondaryScreen = Screen.AllScreens[outputMonitor];
            mainOutputForm.StartPosition = FormStartPosition.Manual;
            mainOutputForm.Location = secondaryScreen.Bounds.Location;
            mainOutputForm.Size = new Size(secondaryScreen.Bounds.Width, secondaryScreen.Bounds.Height);

            if (outputForm != null && !outputForm.IsDisposed)
            {
                outputForm.Hide();
            }

            mainOutputForm.Show();

            MainInputForm inputForm = new MainInputForm(mainOutputForm);

            Screen primaryScreen = Screen.AllScreens[inputMonitor];
            inputForm.StartPosition = FormStartPosition.Manual;
            inputForm.Location = primaryScreen.Bounds.Location;
            inputForm.Size = new Size(primaryScreen.Bounds.Width, primaryScreen.Bounds.Height);
            inputForm.Show();
        }

        // 녹음 시작 메서드
        private void StartRecording()
        {
            waveIn = new WaveInEvent(); // 마이크 입력 초기화
            waveIn.WaveFormat = new WaveFormat(44100, 1); // 44.1kHz, 모노

            // 데이터가 들어올 때마다 호출되는 이벤트 핸들러 연결
            waveIn.DataAvailable += OnDataAvailable;
            waveIn.RecordingStopped += OnRecordingStopped;

            // .wav 파일 작성 시작
            writer = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);
            Console.Write(outputFilePath);

            // 녹음 시작
            waveIn.StartRecording();
            MessageBox.Show("녹음을 시작합니다.");
        }

        // 녹음 중에 데이터가 들어올 때마다 호출되는 메서드
        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            if (writer != null)
            {
                // 녹음된 데이터를 파일에 기록
                writer.Write(e.Buffer, 0, e.BytesRecorded);
                writer.Flush();
            }
        }

        // 녹음 중지 메서드
        private void StopRecording()
        {
            waveIn?.StopRecording(); // 녹음 중지
        }

        // 녹음이 중지될 때 호출되는 메서드
        private void OnRecordingStopped(object sender, StoppedEventArgs e)
        {
            writer?.Dispose(); // 파일 작성 완료 및 해제
            writer = null;
            waveIn.Dispose(); // 마이크 입력 해제
            waveIn = null;

            MessageBox.Show($"녹음이 완료되었습니다. 파일 경로: {outputFilePath}");
        }
    }
}
