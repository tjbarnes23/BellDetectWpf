using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Repository;

namespace BellDetectWpf.ViewModels
{
    public partial class FIRVM
    {
        public async Task WriteFIR()
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

            FIRStatus = "Saving waveform...";
            await Task.Delay(25);

            // Set formatParametersSize (uint)
            formatParametersSize = 16; // 16 bytes per the WAV file spec

            // Set wavType (ushort)
            wavType = 1; // 1 = PCM

            // Set numChannels (ushort)
            if (Repo.FIRNumChannels == 2)
            {
                numChannels = (ushort)(Repo.FIRNumChannels + 1); // Number of channels in filter output array, plus 1 for first channel of the input wav file
            }
            else // = 4
            {
                numChannels = (ushort)(Repo.FIRNumChannels + 2); // Number of channels in filter output array, plus 1 for each of the stereo input wav file channels
            }

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
            if (File.Exists(FIRFilePathName))
            {
                File.Delete(FIRFilePathName);
            }

            // Create .wav file
            using FileStream f = new FileStream(FIRFilePathName, FileMode.Create);
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

                    // Write the original and filtered waveforms
                    for (int i = 0; i < Repo.NumSamples; i++)
                    {
                        wr.Write((short)Repo.WavDataInt[0, i]); // Taking first channel of original wav file

                        for (int j = 0; j < 2; j++)
                        {
                            wr.Write(Repo.FIRFilteredWaveformArr[j, i]);
                        }

                        if (numChannels == 6)
                        {
                            wr.Write((short)Repo.WavDataInt[1, i]); // Taking second channel of original wav file

                            for (int j = 2; j < 4; j++)
                            {
                                wr.Write(Repo.FIRFilteredWaveformArr[j, i]);
                            }
                        }
                    }
                }
            }

            // Create .txt file
            txtFilePathName = Repo.FIRFilePathName + ".txt";

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

                    for (int j = 0; j < 2; j++)
                    {
                        sb.Append(Repo.FIRFilteredWaveformArr[j, i]);
                        sb.Append('\t');
                    }

                    if (numChannels == 6)
                    {
                        sb.Append(Repo.WavDataInt[1, i]); // Second channel of original wav file
                        sb.Append('\t');

                        for (int j = 2; j < 4; j++)
                        {
                            sb.Append(Repo.FIRFilteredWaveformArr[j, i]);
                            sb.Append('\t');
                        }
                    }

                    sb.Append('\n');

                    row = new UTF8Encoding(true).GetBytes(sb.ToString());
                    fs.Write(row, 0, row.Length);
                }
            }

            FIRStatus = "Waveform saved";
            await Task.Delay(25);
        }
    }
}
