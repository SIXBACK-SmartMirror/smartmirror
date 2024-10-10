using NAudio.Wave;
using SmartMirror.Config;

namespace SmartMirror.Helpers
{
    public class AudioRecorder
    {
        private WaveInEvent waveIn;
        private WaveFileWriter writer;
        private string outputFilePath;
        private bool isRecording = false;

        public AudioRecorder(string filePath)
        {
            outputFilePath = filePath;
        }

        // 녹음 시작 메서드
        public void StartRecording()
        {
            if (isRecording) return; // 이미 녹음 중이면 중복 녹음 방지
            isRecording = true;

            waveIn = new WaveInEvent();
            waveIn.WaveFormat = new WaveFormat(44100, 1);

            waveIn.DataAvailable += OnDataAvailable;
            waveIn.RecordingStopped += OnRecordingStopped;

            writer = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);
            waveIn.StartRecording();
            MessageBox.Show("녹음을 시작합니다.");
        }

        // 녹음 중지 메서드
        public void StopRecording()
        {
            waveIn?.StopRecording(); // StopRecording은 RecordingStopped 이벤트를 트리거함
        }

        // 녹음 중에 데이터가 들어올 때마다 호출되는 메서드
        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            writer?.Write(e.Buffer, 0, e.BytesRecorded);
            writer?.Flush();
        }

        // 녹음이 중지될 때 호출되는 메서드
        private async void OnRecordingStopped(object sender, StoppedEventArgs e)
        {
            try
            {
                // 파일이 완전히 해제되었는지 확인
                if (writer != null)
                {
                    writer.Close(); // 파일 닫기
                    writer.Dispose(); // 자원 해제
                }

                waveIn.Dispose(); // 마이크 자원 해제
                waveIn = null;
                writer = null;
                isRecording = false;

                // 녹음이 완료된 후 파일을 전송
                await SendPostRequestWithRecordedAudio();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"녹음 중지 중 오류 발생: {ex.Message}");
            }
        }

        // POST 요청을 통해 녹음된 오디오 파일 전송
        public async Task<string> SendPostRequestWithRecordedAudio()
        {
            string url = $"{ApiConfig.url}/1/goods";
            string filePath = outputFilePath; // audioRecorder로 생성한 파일 경로

            if (!File.Exists(filePath))
            {
                MessageBox.Show("녹음 파일이 존재하지 않습니다.");
                return null;
            }

            using (var form = new MultipartFormDataContent())
            {
                try
                {
                    var audioFile = new ByteArrayContent(File.ReadAllBytes(outputFilePath));
                    audioFile.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("audio/wav");
                    form.Add(audioFile, "audioFile", Path.GetFileName(filePath)); // audioFile 필드로 파일 전송
                    form.Add(new StringContent("0"), "page");
                    form.Add(new StringContent("10"), "size");

                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.PostAsync(url, form);
                        response.EnsureSuccessStatusCode();

                        return await response.Content.ReadAsStringAsync();
                    }
                }
                catch (HttpRequestException httpEx)
                {
                    MessageBox.Show($"HTTP 요청 실패: {httpEx.Message}");
                    return null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"예기치 않은 오류 발생: {ex.Message}");
                    return null;
                }
            }
        }
    }
}
