using System;
using System.IO;
using System.Text;

namespace BellDetectWpf.ViewModels.WavFile
{
    public static partial class C_WavFile
    {
        public static void SaveWav()
        {
            StringBuilder sb;

            // Set variables
            WavFileVM.FormatParametersSize = 16; // 16 bytes per the WAV file spec
            WavFileVM.WavType = 1; // 1 = PCM
            WavFileVM.NumChannels = 1;
            WavFileVM.BitsPerSamplePerChannel = 16;
            WavFileVM.DataRate = (uint)(WaveformVM.SampleFrequency * (WavFileVM.BitsPerSamplePerChannel / 8) *
                    WavFileVM.NumChannels); // bytes per second
            WavFileVM.BlockAlignment = (ushort)((WavFileVM.BitsPerSamplePerChannel / 8) *
                    WavFileVM.NumChannels); // bytes per sample
            WavFileVM.DataSizeBytes = (uint)((WavFileVM.BitsPerSamplePerChannel * WavFileVM.NumChannels *
                    WaveformVM.NumSamples) / 8); // bytes
            WavFileVM.FileSize = WavFileVM.DataSizeBytes + 36; // bytes

            // Create .wav file
            WavFileVM.FilePathName = @"C:\temp\waveform.wav";

            if (File.Exists(WavFileVM.FilePathName))
            {
                File.Delete(WavFileVM.FilePathName);
            }

            using FileStream f = new FileStream(WavFileVM.FilePathName, FileMode.Create);
            using BinaryWriter wr = new BinaryWriter(f);

            wr.Write(Encoding.ASCII.GetBytes("RIFF"));
            wr.Write(WavFileVM.FileSize);
            wr.Write(Encoding.ASCII.GetBytes("WAVE")); // 
            wr.Write(Encoding.ASCII.GetBytes("fmt "));
            wr.Write(WavFileVM.FormatParametersSize); // 
            wr.Write(WavFileVM.WavType);
            wr.Write(WavFileVM.NumChannels);
            wr.Write(WaveformVM.SampleFrequency);
            wr.Write(WavFileVM.DataRate);
            wr.Write(WavFileVM.BlockAlignment);
            wr.Write(WavFileVM.BitsPerSamplePerChannel);
            wr.Write(Encoding.ASCII.GetBytes("data"));
            wr.Write(WavFileVM.DataSizeBytes);

            // Write the summed waveforms to the binary write
            for (int i = 0; i < WaveformVM.NumSamples; i++)
            {
                wr.Write(WaveformVM.Waveform[i]);
            }

            // Write out waveform to a text file (first 256 samples)

            WavFileVM.FilePathName = @"C:\temp\waveform.txt";

            if (File.Exists(WavFileVM.FilePathName))
            {
                File.Delete(WavFileVM.FilePathName);
            }

            sb = new StringBuilder();

            using FileStream fs = File.Create(WavFileVM.FilePathName);

            for (int i = 0; i < 256; i++)
            {
                sb.Clear();
                sb.Append(WaveformVM.Waveform[i]);
                sb.Append('\n');

                Byte[] row = new UTF8Encoding(true).GetBytes(sb.ToString());
                fs.Write(row, 0, row.Length);
            }
        }
    }
}
