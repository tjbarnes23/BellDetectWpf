using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace BellDetectWpf.ViewModels.MicStream
{
    public static class C_MicStream
    {
        public static void StartMicStream()
        {
            MicStreamVM.waveIn = new();
            MicStreamVM.waveIn.DeviceNumber = 0;
            MicStreamVM.waveIn.WaveFormat = new WaveFormat(44100, 16, 1);

            // MicStreamVM.buffer = new();

            MicStreamVM.writer = new WaveFileWriter(@"C:\ProgramData\BellDetect\testMic.wav",
                    MicStreamVM.waveIn.WaveFormat);

            MicStreamVM.waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(MicDataAvailable);
            MicStreamVM.waveIn.StartRecording();
        }

        public static void StopMicStream()
        {
            MicStreamVM.waveIn.StopRecording();
            MicStreamVM.writer.Dispose();
        }

        public static void MicDataAvailable(object sender, WaveInEventArgs e)
        {
            MicStreamVM.writer.Write(e.Buffer, 0, e.BytesRecorded);
        }
    }
}
