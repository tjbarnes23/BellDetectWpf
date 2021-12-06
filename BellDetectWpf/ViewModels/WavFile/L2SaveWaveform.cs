using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.ViewModels;

namespace BellDetectWpf.ViewModels.WavFile
{
    public static class L2SaveWaveform
    {
        public static void SaveWaveform()
        {
            // Declare variables
            uint formatParametersSize; // bytes
            ushort wavType; // number
            ushort numChannels; // number

            ushort bitsPerSamplePerChannel; // bits
            uint dataRate; // bytes per second
            ushort blockAlignment; // bytes

            uint dataSize; // bytes
            uint fileSize; // bytes

            string fileName;
            StringBuilder sb;

            // Set variables
            formatParametersSize = 16; // bytes
            wavType = 1; // number
            numChannels = 1; // number

            bitsPerSamplePerChannel = 16; // bits
            dataRate = (WaveformVM.SampleFrequency * bitsPerSamplePerChannel * numChannels) / 8; // bytes per second
            blockAlignment = (ushort)((bitsPerSamplePerChannel * numChannels) / 8); // bytes

            dataSize = (uint)((bitsPerSamplePerChannel * numChannels * WaveformVM.NumSamples) / 8); // bytes
            fileSize = dataSize + 36; // bytes

            // Create .wav file
            fileName = @"C:\temp\waveform.wav";

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            using FileStream f = new FileStream(fileName, FileMode.Create);
            using BinaryWriter wr = new BinaryWriter(f);

            wr.Write(Encoding.ASCII.GetBytes("RIFF"));
            wr.Write(fileSize);
            wr.Write(Encoding.ASCII.GetBytes("WAVE")); // 
            wr.Write(Encoding.ASCII.GetBytes("fmt "));
            wr.Write(formatParametersSize); // 
            wr.Write(wavType);
            wr.Write(numChannels);
            wr.Write(WaveformVM.SampleFrequency);
            wr.Write(dataRate);
            wr.Write(blockAlignment);
            wr.Write(bitsPerSamplePerChannel);
            wr.Write(Encoding.ASCII.GetBytes("data"));
            wr.Write(dataSize);

            // Write the summed waveforms to the binary write
            for (int i = 0; i < WaveformVM.NumSamples; i++)
            {
                wr.Write(WaveformVM.Waveform[i]);
            }
            
            // Write out waveform to a text file (first 256 samples)
            
            fileName = @"C:\temp\waveform.txt";

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            sb = new StringBuilder();

            using FileStream fs = File.Create(fileName);

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
