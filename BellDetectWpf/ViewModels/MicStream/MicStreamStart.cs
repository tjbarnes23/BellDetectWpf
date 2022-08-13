using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;
using NAudio.Wave;


namespace BellDetectWpf.ViewModels
{
    public partial class MicStreamVM
    {
        public void MicStreamStart()
        {
            // Set parameters for audio stream
            SampleFrequency = 48000;
            SampleDepth = 16;
            NumChannels = 1;

            // Set up NAudio variables
            waveFormat = new(SampleFrequency, SampleDepth, NumChannels);

            waveIn = new()
            {
                DeviceNumber = 0, // Uses input device specified in Windows Sound settings
                WaveFormat = waveFormat,
                BufferMilliseconds = 100,
                NumberOfBuffers = 2
            };

            waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(MicDataAvailable);

            // Initalize buffer
            AudioBuffer = new();

            waveIn.StartRecording();
        }

        public void MicDataAvailable(object sender, WaveInEventArgs e)
        {
            int numBytes;
            int bytePos;
            short amp;

            // Convert 2 bytes (16-bit signed int) into int16 and enqueue
            numBytes = e.BytesRecorded;
            bytePos = 0;

            for (int i = 0; i < numBytes / 2; i++)
            {
                amp = BitConverter.ToInt16(e.Buffer, bytePos);
                bytePos += 2;

                AudioBuffer.Enqueue(amp);
            }

            /*
            while (AudioBuffer.Count >= FFTVM.N)
            {
                RunAudioFFT();
            }
            */
        }
    }
}
