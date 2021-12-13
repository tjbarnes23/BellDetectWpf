using System.IO;

namespace BellDetectWpf.ViewModels.LoadWav
{
    public static partial class C_LoadWav
    {
        public static void LoadWav()
        {
            string riff;
            uint fileSize;
            string wave;
            string fmt;
            uint formatParametersSize;
            ushort wavType;
            uint dataRate;
            ushort blockAlignment;
            string data;

            // Set filename
            WavFileVM.FilePathName = @"C:\Users\Tim\source\repos\tjbarnes23\BellDetectWpf\BellDetectWpf\BellSamples\8th handstroke mono 96 kHz signed 16-bit.wav";

            // Read .wav file
            using FileStream f = new FileStream(WavFileVM.FilePathName, FileMode.Open);
            using BinaryReader br = new BinaryReader(f);

            // Read 'RIFF'
            riff = System.Text.Encoding.UTF8.GetString(br.ReadBytes(4));

            // Read fileSize (uint)
            fileSize = br.ReadUInt32();

            // Read 'WAVE' and 'fmt '
            wave = System.Text.Encoding.UTF8.GetString(br.ReadBytes(4));
            fmt = System.Text.Encoding.UTF8.GetString(br.ReadBytes(4));

            // Read formatParametersSize (uint)
            formatParametersSize = br.ReadUInt32();

            // Read wavType (ushort)
            wavType = br.ReadUInt16();

            // Read numChannels (ushort)
            WavFileVM.NumChannels = br.ReadUInt16();

            // Read sampleFrequency (uint)
            WavFileVM.SampleFrequency = br.ReadUInt32();

            // Read dataRate (uint)
            dataRate = br.ReadUInt32();

            // Read blockAlignment (ushort)
            blockAlignment = br.ReadUInt16();

            // Read bitsPerSamplePerChannel (ushort)
            WavFileVM.SampleDepth = br.ReadUInt16();

            // Read 'data'
            data = System.Text.Encoding.UTF8.GetString(br.ReadBytes(4));

            // Read dataSize (uint)
            WavFileVM.SampleLengthBytes = br.ReadUInt32();

            // Calculate sample length in seconds
            WavFileVM.SampleLengthSeconds = (double)(WavFileVM.SampleLengthBytes /
                    (double)((WavFileVM.SampleDepth / 8) * WavFileVM.NumChannels * WavFileVM.SampleFrequency));

            WavFileVM.WavFileFormatValid = "Yes";

            if (riff != "RIFF" ||
                    fileSize != (WavFileVM.SampleLengthBytes + formatParametersSize + 20) ||
                    wave != "WAVE" ||
                    fmt != "fmt " ||
                    formatParametersSize != 16 ||
                    wavType != 1 ||
                    WavFileVM.NumChannels != 1 ||
                    dataRate != WavFileVM.NumChannels * WavFileVM.SampleFrequency * (WavFileVM.SampleDepth / 8) ||
                    blockAlignment != WavFileVM.NumChannels * (WavFileVM.SampleDepth / 8) ||
                    WavFileVM.SampleDepth != 16 ||
                    data != "data")
            {
                WavFileVM.WavFileFormatValid = "No";
            }

            // If file format is valid, read in .wav file data
            if (WavFileVM.WavFileFormatValid == "Yes")
            {
                CreateWaveformVM.NumSamples = (uint)(WavFileVM.SampleLengthBytes /
                    ((WavFileVM.SampleDepth / 8) * WavFileVM.NumChannels));

                CreateWaveformVM.Waveform = new short[CreateWaveformVM.NumSamples];

                for (int i = 0; i < CreateWaveformVM.NumSamples; i++)
                {
                    CreateWaveformVM.Waveform[i] = br.ReadInt16();
                }
            }
        }
    }
}
