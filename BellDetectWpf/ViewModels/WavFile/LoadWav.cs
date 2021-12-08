using System.IO;

namespace BellDetectWpf.ViewModels.WavFile
{
    public static partial class C_WavFile
    {
        public static void LoadWav()
        {
            // Set filename
            WavFileVM.FilePathName = @"C:\Users\Tim\source\repos\tjbarnes23\BellDetectWpf\BellDetectWpf\BellSamples\8th handstroke mono 96 kHz signed 16-bit.wav";

            // Read .wav file
            using FileStream f = new FileStream(WavFileVM.FilePathName, FileMode.Open);
            using BinaryReader br = new BinaryReader(f);

            // Read 'RIFF'
            for (int i = 0; i < 4; i++)
            {
                _ = br.ReadByte();
            }

            // Read fileSize (uint)
            WavFileVM.FileSize = br.ReadUInt32();

            // Read 'WAVE' and 'fmt '
            for (int i = 0; i < 8; i++)
            {
                _ = br.ReadByte();
            }

            // Read formatParametersSize (uint)
            WavFileVM.FormatParametersSize = br.ReadUInt32();

            // Read wavType (ushort)
            WavFileVM.WavType = br.ReadUInt16();

            // Read numChannels (ushort)
            WavFileVM.NumChannels = br.ReadUInt16();

            // Read sampleFrequency (uint)
            WaveformVM.SampleFrequency = br.ReadUInt32();

            // Read dataRate (uint)
            WavFileVM.DataRate = br.ReadUInt32();

            // Read blockAlignment (ushort)
            WavFileVM.BlockAlignment = br.ReadUInt16();

            // Read bitsPerSamplePerChannel (ushort)
            WavFileVM.BitsPerSamplePerChannel = br.ReadUInt16();

            // Read 'data'
            for (int i = 0; i < 4; i++)
            {
                _ = br.ReadByte();
            }

            // Read dataSize (uint)
            WavFileVM.DataSizeBytes = br.ReadUInt32();
            WaveformVM.NumSamples = (uint)(WavFileVM.DataSizeBytes /
                    ((WavFileVM.BitsPerSamplePerChannel / 8) * WavFileVM.NumChannels));

            WaveformVM.Waveform = new short[WaveformVM.NumSamples];

            for (int i = 0; i < WaveformVM.NumSamples; i++)
            {
                WaveformVM.Waveform[i] = br.ReadInt16();
            }
        }
    }
}
