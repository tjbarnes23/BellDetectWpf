using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Repository;

namespace BellDetectWpf.ViewModels
{
    public partial class EllipticVM
    {
        public async Task WriteElliptic()
        {
            uint fileSize;
            uint dataSize;
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
            dataRate = (uint)(Repo.SampleFrequency * (Repo.SampleDepth / 8) *
                    EllipticVM.NumChannels); // bytes per second
            blockAlignment = (ushort)((WaveformVM.SampleDepth / 8) *
                    EllipticVM.NumChannels); // bytes per sample
            dataSize = (WaveformVM.LengthBytes * EllipticVM.NumChannels);
            fileSize = dataSize + formatParametersSize + 20; // bytes

            await C_Shared.Status("Saving waveform...", "black", 10, false);

            // Delete file if it already exists
            if (File.Exists(EllipticVM.FilePathName))
            {
                File.Delete(EllipticVM.FilePathName);
            }

            // Create .wav file
            using FileStream f = new FileStream(EllipticVM.FilePathName, FileMode.Create);
            {
                using BinaryWriter wr = new BinaryWriter(f);
                {
                    wr.Write(Encoding.ASCII.GetBytes("RIFF"));
                    wr.Write(fileSize);
                    wr.Write(Encoding.ASCII.GetBytes("WAVE")); // 
                    wr.Write(Encoding.ASCII.GetBytes("fmt "));
                    wr.Write(formatParametersSize); // 
                    wr.Write(wavType);
                    wr.Write(EllipticVM.NumChannels);
                    wr.Write(WaveformVM.SampleFrequency);
                    wr.Write(dataRate);
                    wr.Write(blockAlignment);
                    wr.Write(WaveformVM.SampleDepth);
                    wr.Write(Encoding.ASCII.GetBytes("data"));
                    wr.Write(dataSize);

                    // Write the filtered waveforms to the binary writer
                    for (int i = 0; i < WaveformVM.NumSamples; i++)
                    {
                        for (int j = 0; j < EllipticVM.NumChannels; j++)
                        {
                            wr.Write(EllipticVM.FilteredWaveformArr[i, j]);
                        }
                    }
                }
            }

            // Create .txt file
            txtFilePathName = EllipticVM.FilePathName + ".txt";

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

                for (int j = 0; j < EllipticVM.NumChannels; j++)
                {
                    sb.Append("Bell ");
                    sb.Append(j + 1);
                    sb.Append('\t');
                }

                sb.Append('\n');

                row = new UTF8Encoding(true).GetBytes(sb.ToString());
                fs.Write(row, 0, row.Length);

                // Write data rows
                for (int i = 0; i < WaveformVM.NumSamples; i++)
                {
                    sb.Clear();
                    sb.Append(Math.Round(WaveformVM.Time[i], 6));
                    sb.Append('\t');

                    for (int j = 0; j < EllipticVM.NumChannels; j++)
                    {
                        sb.Append(EllipticVM.FilteredWaveformArr[i, j]);
                        sb.Append('\t');
                    }

                    sb.Append('\n');

                    row = new UTF8Encoding(true).GetBytes(sb.ToString());
                    fs.Write(row, 0, row.Length);
                }
            }

            await C_Shared.Status("Waveform saved", "black", 3000, true);
        }
    }
}
