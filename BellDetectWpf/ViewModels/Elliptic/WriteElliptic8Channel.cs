using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Repository;

namespace BellDetectWpf.ViewModels
{
    public partial class EllipticVM
    {
        public async Task WriteElliptic8Channel()
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

            EllipticStatus = "Saving waveform...";
            await Task.Delay(25);

            // Set formatParametersSize (uint)
            formatParametersSize = 16; // 16 bytes per the WAV file spec

            // Set wavType (ushort)
            wavType = 1; // 1 = PCM

            // Set dataRate (uint)
            dataRate = (uint)(Repo.SampleFrequency * (Repo.SampleDepth / 8) *
                    Repo.EllipticNumChannels); // bytes per second

            // Set blockAlignment (ushort)
            blockAlignment = (ushort)((Repo.SampleDepth / 8) *
                    Repo.EllipticNumChannels); // bytes per sample

            // Set dataSize (uint)
            dataSize = (Repo.DataSize / Repo.NumChannels) * Repo.EllipticNumChannels; // Divide original data size by original num channels
                                                                                        // because we only processed the first channel

            // Set fileSize (uint)
            fileSize = dataSize + formatParametersSize + 20; // bytes

            // Delete file if it already exists
            if (File.Exists(EllipticFilePathName))
            {
                File.Delete(EllipticFilePathName);
            }

            // Create .wav file
            using FileStream f = new FileStream(EllipticFilePathName, FileMode.Create);
            {
                using BinaryWriter wr = new BinaryWriter(f);
                {
                    wr.Write(Encoding.ASCII.GetBytes("RIFF"));
                    wr.Write(fileSize);
                    wr.Write(Encoding.ASCII.GetBytes("WAVE")); // 
                    wr.Write(Encoding.ASCII.GetBytes("fmt "));
                    wr.Write(formatParametersSize); // 
                    wr.Write(wavType);
                    wr.Write(Repo.EllipticNumChannels);
                    wr.Write(Repo.SampleFrequency);
                    wr.Write(dataRate);
                    wr.Write(blockAlignment);
                    wr.Write(Repo.SampleDepth);
                    wr.Write(Encoding.ASCII.GetBytes("data"));
                    wr.Write(dataSize);

                    // Write the filtered waveforms to the binary writer
                    for (int i = 0; i < Repo.NumSamples; i++)
                    {
                        for (int j = 0; j < Repo.EllipticNumChannels; j++)
                        {
                            wr.Write(Repo.FilteredWaveformArr[j, i]);
                        }
                    }
                }
            }

            // Create .txt file
            txtFilePathName = Repo.EllipticFilePathName + ".txt";

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

                for (int j = 0; j < Repo.EllipticNumChannels; j++)
                {
                    sb.Append("Bell ");
                    sb.Append(j + 1);
                    sb.Append('\t');
                }

                sb.Append('\n');

                row = new UTF8Encoding(true).GetBytes(sb.ToString());
                fs.Write(row, 0, row.Length);

                // Write data rows
                for (int i = 0; i < Repo.NumSamples; i++)
                {
                    sb.Clear();
                    sb.Append(Math.Round(Repo.Time[i], 6));
                    sb.Append('\t');

                    for (int j = 0; j < Repo.EllipticNumChannels; j++)
                    {
                        sb.Append(Repo.FilteredWaveformArr[j, i]);
                        sb.Append('\t');
                    }

                    sb.Append('\n');

                    row = new UTF8Encoding(true).GetBytes(sb.ToString());
                    fs.Write(row, 0, row.Length);
                }
            }

            EllipticStatus = "Waveform saved";
            await Task.Delay(25);
        }
    }
}
