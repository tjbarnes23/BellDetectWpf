using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.ViewModels.Shared;

namespace BellDetectWpf.ViewModels.Waveform
{
    public static partial class C_Waveform
    {
        public static async Task WriteWaveform()
        {
            uint fileSize;
            uint formatParametersSize;
            ushort wavType;
            uint dataRate;
            ushort blockAlignment;

            StringBuilder sb;
            byte[] row;
            string txtFilePathName;

            // Set variables
            formatParametersSize = 16; // 16 bytes per the WAV file spec
            wavType = 1; // 1 = PCM
            dataRate = (uint)(WaveformVM.SampleFrequency * (WaveformVM.SampleDepth / 8) *
                    WaveformVM.NumChannels); // bytes per second
            blockAlignment = (ushort)((WaveformVM.SampleDepth / 8) *
                    WaveformVM.NumChannels); // bytes per sample
            fileSize = WaveformVM.LengthBytes + formatParametersSize + 20; // bytes

            SharedVM.StatusMsg = "Saving...";
            SharedVM.StatusForeground = "black";

            // Delete file if it already exists
            if (File.Exists(WaveformVM.FilePathName))
            {
                File.Delete(WaveformVM.FilePathName);
            }

            // Create .wav file
            using FileStream f = new FileStream(WaveformVM.FilePathName, FileMode.Create);
            {
                using BinaryWriter wr = new BinaryWriter(f);
                {
                    wr.Write(Encoding.ASCII.GetBytes("RIFF"));
                    wr.Write(fileSize);
                    wr.Write(Encoding.ASCII.GetBytes("WAVE")); // 
                    wr.Write(Encoding.ASCII.GetBytes("fmt "));
                    wr.Write(formatParametersSize); // 
                    wr.Write(wavType);
                    wr.Write(WaveformVM.NumChannels);
                    wr.Write(WaveformVM.SampleFrequency);
                    wr.Write(dataRate);
                    wr.Write(blockAlignment);
                    wr.Write(WaveformVM.SampleDepth);
                    wr.Write(Encoding.ASCII.GetBytes("data"));
                    wr.Write(WaveformVM.LengthBytes);

                    // Write the summed waveforms to the binary write
                    for (int i = 0; i < WaveformVM.NumSamples; i++)
                    {
                        wr.Write(WaveformVM.WaveformArr[i]);
                    }
                }
            }

            // Create .txt file
            txtFilePathName = WaveformVM.FilePathName + ".txt";

            if (File.Exists(txtFilePathName))
            {
                File.Delete(txtFilePathName);
            }

            // Create a new file     
            using FileStream fs = File.Create(txtFilePathName);
            {
                // Write header row
                sb = new StringBuilder();
                sb.Clear();
                sb.Append("Time");
                sb.Append('\t');
                sb.Append("Amplitude");
                sb.Append('\n');
                
                row = new UTF8Encoding(true).GetBytes(sb.ToString());
                fs.Write(row, 0, row.Length);

                // Write data rows
                for (int i = 0; i < WaveformVM.NumSamples; i++)
                {
                    sb.Clear();
                    sb.Append(Math.Round(WaveformVM.Time[i], 6));
                    sb.Append('\t');
                    sb.Append(WaveformVM.WaveformArr[i]);
                    sb.Append('\n');

                    row = new UTF8Encoding(true).GetBytes(sb.ToString());
                    fs.Write(row, 0, row.Length);
                }
            }

            await C_Shared.Status("Saved", "black", 3000);
        }
    }
}
