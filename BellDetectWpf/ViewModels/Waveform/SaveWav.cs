using System;
using System.IO;
using System.Text;

namespace BellDetectWpf.ViewModels.Waveform
{
    public static partial class C_Waveform
    {
        public static void SaveWav()
        {
            uint fileSize;
            uint formatParametersSize;
            ushort wavType;
            uint dataRate;
            ushort blockAlignment;

            // Set variables
            formatParametersSize = 16; // 16 bytes per the WAV file spec
            wavType = 1; // 1 = PCM
            WavFileVM.NumChannels = 1;
            WavFileVM.SampleDepth = 16;
            dataRate = (uint)(WavFileVM.SampleFrequency * (WavFileVM.SampleDepth / 8) *
                    WavFileVM.NumChannels); // bytes per second
            blockAlignment = (ushort)((WavFileVM.SampleDepth / 8) *
                    WavFileVM.NumChannels); // bytes per sample

            WavFileVM.SampleLengthBytes = (uint)(WavFileVM.SampleLengthSeconds * WavFileVM.SampleFrequency *
                    (WavFileVM.SampleDepth / 8) * WavFileVM.NumChannels); // bytes
            
            fileSize = WavFileVM.SampleLengthBytes + formatParametersSize + 20; // bytes

            // Delete file if it already exists
            if (File.Exists(WavFileVM.FilePathName))
            {
                File.Delete(WavFileVM.FilePathName);
            }

            // Create .wav file
            using FileStream f = new FileStream(WaveformVM.FilePathName, FileMode.Create);
            using BinaryWriter wr = new BinaryWriter(f);

            wr.Write(Encoding.ASCII.GetBytes("RIFF"));
            wr.Write(fileSize);
            wr.Write(Encoding.ASCII.GetBytes("WAVE")); // 
            wr.Write(Encoding.ASCII.GetBytes("fmt "));
            wr.Write(formatParametersSize); // 
            wr.Write(wavType);
            wr.Write(WavFileVM.NumChannels);
            wr.Write(WavFileVM.SampleFrequency);
            wr.Write(dataRate);
            wr.Write(blockAlignment);
            wr.Write(WavFileVM.SampleDepth);
            wr.Write(Encoding.ASCII.GetBytes("data"));
            wr.Write(WavFileVM.SampleLengthBytes);

            // Write the summed waveforms to the binary write
            for (int i = 0; i < WaveformVM.NumSamples; i++)
            {
                wr.Write(WaveformVM.Waveform[i]);
            }
        }
    }
}
