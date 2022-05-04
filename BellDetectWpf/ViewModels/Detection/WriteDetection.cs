using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Repository;

namespace BellDetectWpf.ViewModels
{
    public partial class DetectionVM
    {
        public async Task WriteDetection()
        {
            uint fileSize;
            uint dataSize;
            uint formatParametersSize;
            ushort wavType;
            uint dataRate;
            ushort blockAlignment;

            DetectionStatus = "Saving detection waveform...";
            await Task.Delay(25);

            // Set formatParametersSize (uint)
            formatParametersSize = 16; // 16 bytes per the WAV file spec

            // Set wavType (ushort)
            wavType = 1; // 1 = PCM

            // Set dataRate (uint)
            dataRate = (uint)(Repo.SampleFrequency * (Repo.SampleDepth / 8) * Repo.DetectionNumChannels); // bytes per second

            // Set blockAlignment (ushort)
            blockAlignment = (ushort)((Repo.SampleDepth / 8) * Repo.DetectionNumChannels); // bytes per sample

            // Set dataSize (uint)
            dataSize = (Repo.DataSize / Repo.WavNumChannels) * Repo.DetectionNumChannels; // Divide original data size by original num channels
                                                                                    // because we only processed the first channel

            // Set fileSize (uint)
            fileSize = dataSize + formatParametersSize + 20; // bytes

            // Delete file if it already exists
            if (File.Exists(DetectionFilePathName))
            {
                File.Delete(DetectionFilePathName);
            }

            // Create .wav file
            using FileStream f = new FileStream(DetectionFilePathName, FileMode.Create);
            {
                using BinaryWriter wr = new BinaryWriter(f);
                {
                    wr.Write(Encoding.ASCII.GetBytes("RIFF"));
                    wr.Write(fileSize);
                    wr.Write(Encoding.ASCII.GetBytes("WAVE")); // 
                    wr.Write(Encoding.ASCII.GetBytes("fmt "));
                    wr.Write(formatParametersSize); // 
                    wr.Write(wavType);
                    wr.Write(Repo.DetectionNumChannels);
                    wr.Write(Repo.SampleFrequency);
                    wr.Write(dataRate);
                    wr.Write(blockAlignment);
                    wr.Write(Repo.SampleDepth);
                    wr.Write(Encoding.ASCII.GetBytes("data"));
                    wr.Write(dataSize);

                    // Write the original and filtered waveforms
                    for (int i = 0; i < Repo.NumSamples; i++)
                    {
                        for (int j = 0; j < Repo.DetectionNumChannels; j++)
                        {
                            wr.Write(Repo.DetectionWaveformArr[j, i]);
                        }
                    }
                }
            }


            /**************************************************
            * Write detection data to text file
            **************************************************/
            StringBuilder sb;
            string filePathName;

            for (int i = 0; i < Repo.DetectionNumChannels / 3; i++)
            {
                sb = new();
                sb.Append(DetectionFilePathName);

                if (i == 0)
                {
                    sb.Append(".Left");
                }
                else
                {
                    sb.Append(".Right");
                }

                sb.Append(".txt");
                filePathName = sb.ToString();

                // Delete file if it already exists
                if (File.Exists(filePathName))
                {
                    File.Delete(filePathName);
                }

                double lowLow, lowHigh, mid, highLow, highHigh;

                if (i == 0)
                {
                    lowLow = Repo.LeftLowLow;
                    lowHigh = Repo.LeftLowHigh;
                    mid = Repo.LeftMid;
                    highLow = Repo.LeftHighLow;
                    highHigh = Repo.LeftHighHigh;
                }
                else
                {
                    lowLow = Repo.RightLowLow;
                    lowHigh = Repo.RightLowHigh;
                    mid = Repo.RightMid;
                    highLow = Repo.RightHighLow;
                    highHigh = Repo.RightHighHigh;
                }
                
                int samplesMid = Convert.ToInt32(48000 / mid);

                using Stream s = File.Create(filePathName);
                using StreamWriter sw = new(s, new UTF8Encoding(false));

                // Write header row
                sb.Clear();
                sb.Append($"Sample #\t");
                sb.Append($"Time (ms)\t");
                sb.Append($"Amplitude\t");
                sb.Append($"Crossing?\t");
                sb.Append($"Crossing type\t");
                sb.Append($"Closest matching crossing to {samplesMid} prior\t");
                sb.Append($"Implied frequency\t");
                sb.Append($"Strike? ((>= {lowLow} Hz and >= {lowHigh} Hz) or (>= {highLow} Hz and <= {highHigh} Hz)) and (amplitude >= {Repo.AmplitudeCutoff})\t");

                sw.WriteLine(sb.ToString());

                // Write data rows
                for (int j = 0; i < Repo.Samples.Count; i++)
                {
                    sb.Clear();
                    sb.Append(Repo.Samples[i].SampleNum);
                    sb.Append('\t');
                    sb.Append(Repo.Samples[i].Time);
                    sb.Append('\t');
                    sb.Append(Repo.Samples[i].Amplitude);
                    sb.Append('\t');

                    if (Repo.Samples[i].Crossing == true)
                    {
                        sb.Append(Repo.Samples[i].Crossing);
                        sb.Append('\t');
                        sb.Append(Repo.Samples[i].CrossingType);
                        sb.Append('\t');

                        if (Repo.Samples[i].NearestCrossingMidPrior != 0)
                        {
                            sb.Append(Repo.Samples[i].NearestCrossingMidPrior);
                            sb.Append('\t');
                            sb.Append(Repo.Samples[i].ImpliedFrequency);
                            sb.Append('\t');

                            if (Repo.Samples[i].StrikeDetected == true)
                            {
                                sb.Append(Repo.Samples[i].StrikeDetected);
                                sb.Append('\t');
                            }
                        }
                    }

                    sw.WriteLine(sb.ToString());
                }
            }

            DetectionStatus = "Detection waveform saved";
            await Task.Delay(25);
        }
    }
}
