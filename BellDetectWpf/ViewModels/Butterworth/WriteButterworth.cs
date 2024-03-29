﻿using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Repository;

namespace BellDetectWpf.ViewModels
{
    public partial class ButterworthVM
    {
        public async Task WriteButterworth()
        {
            uint fileSize;
            uint dataSize;
            uint formatParametersSize;
            ushort wavType;
            ushort numChannels;
            uint dataRate;
            ushort blockAlignment;

            StringBuilder sb;
            byte[] row;
            string txtFilePathName;

            ButterworthStatus = "Saving waveform...";
            await Task.Delay(25);

            // Set formatParametersSize (uint)
            formatParametersSize = 16; // 16 bytes per the WAV file spec

            // Set wavType (ushort)
            wavType = 1; // 1 = PCM

            // Set numChannels (ushort)
            numChannels = (ushort)(Repo.ButterworthNumChannels + 1); // Number of channels in filter output array, plus 1 for first channel of the input wav file

            // Set dataRate (uint)
            dataRate = (uint)(Repo.SampleFrequency * (Repo.SampleDepth / 8) * numChannels); // bytes per second

            // Set blockAlignment (ushort)
            blockAlignment = (ushort)((Repo.SampleDepth / 8) * numChannels); // bytes per sample

            // Set dataSize (uint)
            dataSize = (Repo.DataSize / Repo.WavNumChannels) * numChannels; // Divide original data size by original num channels
                                                                                         // because we only processed the first channel

            // Set fileSize (uint)
            fileSize = dataSize + formatParametersSize + 20; // bytes

            // Delete file if it already exists
            if (File.Exists(ButterworthFilePathName))
            {
                File.Delete(ButterworthFilePathName);
            }

            // Create .wav file
            using FileStream f = new FileStream(ButterworthFilePathName, FileMode.Create);
            {
                using BinaryWriter wr = new BinaryWriter(f);
                {
                    wr.Write(Encoding.ASCII.GetBytes("RIFF"));
                    wr.Write(fileSize);
                    wr.Write(Encoding.ASCII.GetBytes("WAVE")); // 
                    wr.Write(Encoding.ASCII.GetBytes("fmt "));
                    wr.Write(formatParametersSize); // 
                    wr.Write(wavType);
                    wr.Write(numChannels);
                    wr.Write(Repo.SampleFrequency);
                    wr.Write(dataRate);
                    wr.Write(blockAlignment);
                    wr.Write(Repo.SampleDepth);
                    wr.Write(Encoding.ASCII.GetBytes("data"));
                    wr.Write(dataSize);

                    // Write the filtered waveforms to the binary writer
                    for (int i = 0; i < Repo.NumSamples; i++)
                    {
                        wr.Write((short)Repo.WavDataInt[0, i]); // Taking first channel of original wav file

                        for (int j = 0; j < Repo.ButterworthNumChannels; j++)
                        {
                            wr.Write(Repo.ButterworthFilteredWaveformArr[j, i]);
                        }
                    }
                }
            }

            // Create .txt file
            txtFilePathName = Repo.ButterworthFilePathName + ".txt";

            if (File.Exists(txtFilePathName))
            {
                File.Delete(txtFilePathName);
            }

            // Create a new file     
            using FileStream fs = File.Create(txtFilePathName);
            {
                // Write header row
                sb = new StringBuilder();
                
                sb.Append("Time");
                sb.Append('\t');

                for (int j = 0; j < numChannels; j++)
                {
                    sb.Append("Channel ");
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

                    sb.Append(Repo.WavDataInt[0, i]); // First channel of original wav file
                    sb.Append('\t');

                    for (int j = 0; j < Repo.ButterworthNumChannels; j++)
                    {
                        sb.Append(Repo.ButterworthFilteredWaveformArr[j, i]);
                        sb.Append('\t');
                    }

                    sb.Append('\n');

                    row = new UTF8Encoding(true).GetBytes(sb.ToString());
                    fs.Write(row, 0, row.Length);
                }
            }

            ButterworthStatus = "Waveform saved";
            await Task.Delay(25);
        }
    }
}
