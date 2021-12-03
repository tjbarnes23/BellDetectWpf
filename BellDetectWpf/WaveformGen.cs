using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf
{
    public static class WaveformGen
    {
        public static void GenerateWaveform()
        {
            int channels = 1;
            int bitsPerSample = 8;

            //WaveFile is custom class to create a wav file.
            WaveFile file = new WaveFile(channels, bitsPerSample, 11025);

            int seconds = 60;
            int samples = 11025 * seconds; //Create x seconds of audio

            // Sound Data Size = Number Of Channels * Bits Per Sample * Samples

            byte[] data = new byte[channels * bitsPerSample / 8 * samples];

            //Creates a Constant Sound
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)(256 * Math.Sin(i));
            }
            file.SetData(data, samples);
        }
    }
}
