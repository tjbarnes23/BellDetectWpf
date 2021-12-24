using System.IO;

namespace BellDetectWpf.ViewModels.Waveform
{
    public static partial class C_Waveform
    {
        public static void LoadWaveform()
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

            // Read .wav file
            using FileStream f = new FileStream(WaveformVM.FilePathName, FileMode.Open);
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
            WaveformVM.NumChannels = br.ReadUInt16();

            // Read sampleFrequency (uint)
            WaveformVM.SampleFrequency = br.ReadUInt32();

            // Read dataRate (uint)
            dataRate = br.ReadUInt32();

            // Read blockAlignment (ushort)
            blockAlignment = br.ReadUInt16();

            // Read bitsPerSamplePerChannel (ushort)
            WaveformVM.SampleDepth = br.ReadUInt16();

            // Read 'data'
            data = System.Text.Encoding.UTF8.GetString(br.ReadBytes(4));

            // Read dataSize (uint)
            WaveformVM.SampleLengthBytes = br.ReadUInt32();

            // Calculate sample length in seconds
            WaveformVM.SampleLengthSeconds = (double)(WaveformVM.SampleLengthBytes /
                    (double)((WaveformVM.SampleDepth / 8) * WaveformVM.NumChannels * WaveformVM.SampleFrequency));

            WaveformVM.WavFileFormatValid = "Yes";

            if (riff != "RIFF" ||
                    fileSize != (WaveformVM.SampleLengthBytes + formatParametersSize + 20) ||
                    wave != "WAVE" ||
                    fmt != "fmt " ||
                    formatParametersSize != 16 ||
                    wavType != 1 ||
                    WaveformVM.NumChannels != 1 ||
                    dataRate != WaveformVM.NumChannels * WaveformVM.SampleFrequency * (WaveformVM.SampleDepth / 8) ||
                    blockAlignment != WaveformVM.NumChannels * (WaveformVM.SampleDepth / 8) ||
                    WaveformVM.SampleDepth != 16 ||
                    data != "data")
            {
                WaveformVM.WavFileFormatValid = "No";
            }

            // If file format is valid, read in .wav file data
            if (WaveformVM.WavFileFormatValid == "Yes")
            {
                WaveformVM.NumSamples = (uint)(WaveformVM.SampleLengthBytes /
                    ((WaveformVM.SampleDepth / 8) * WaveformVM.NumChannels));

                WaveformVM.Waveform = new short[WaveformVM.NumSamples];

                for (int i = 0; i < WaveformVM.NumSamples; i++)
                {
                    WaveformVM.Waveform[i] = br.ReadInt16();
                }
            }
        }
    }
}
