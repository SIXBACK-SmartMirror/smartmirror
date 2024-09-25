using NAudio.Wave;

namespace SmartMirror.Helpers
{
    public class AudioRecorder
    {
        private WaveInEvent waveIn;
        private WaveFileWriter writer;
        private string outputFilePath;

        public AudioRecorder(string filePath)
        {
            outputFilePath = filePath;
        }

        // 녹음 시작 메서드
        public void StartRecording()
        {
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
            waveIn?.StopRecording();
        }

        // 녹음 중에 데이터가 들어올 때마다 호출되는 메서드
        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            writer?.Write(e.Buffer, 0, e.BytesRecorded);
            writer?.Flush();
        }

        // 녹음이 중지될 때 호출되는 메서드
        private void OnRecordingStopped(object sender, StoppedEventArgs e)
        {
            writer?.Dispose();
            waveIn?.Dispose();
            MessageBox.Show($"녹음이 완료되었습니다. 파일 경로: {outputFilePath}");
        }
    }
}
