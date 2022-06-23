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
                sb.Append($"Time (s)\t");
                sb.Append($"Amplitude\t");
                sb.Append($"Crossing?\t");

                sb.Append($"Crossing type\t");
                sb.Append($"Closest matching crossing to {samplesMid} prior\t");

                sb.Append($"Implied frequency (Hz)\t");
                sb.Append($"Implied freq in shift range?\t");

                sb.Append($"Max amplitude found\t");
                sb.Append($"Max amplitude sample #\t");
                sb.Append($"Min amplitude met?\t");

                sb.Append($"Half-cycle peak positive value\t");
                sb.Append($"Half-cycle peak positive sample #\t");
                sb.Append($"Half-cycle peak negative value\t");
                sb.Append($"Half-cycle peak negative sample #\t");
                sb.Append($"Denominator\t");
                sb.Append($"Numerator\t");
                sb.Append($"Max amplitude increase %\t");
                sb.Append($"Max amplitude increase sample #\t");
                sb.Append($"Min amplitude increase met?\t");

                sb.Append($"Strike detected?\t");

                sw.WriteLine(sb.ToString());

                // Write data rows
                for (int j = 0; j < Repo.Samples[i].Count; j++)
                {
                    sb.Clear();
                    sb.Append(Repo.Samples[i][j].SampleNum);
                    sb.Append('\t');
                    sb.Append(Repo.Samples[i][j].Time);
                    sb.Append('\t');
                    sb.Append(Repo.Samples[i][j].Amplitude);
                    sb.Append('\t');
                    sb.Append(Repo.Samples[i][j].Crossing);
                    sb.Append('\t');

                    if (Repo.Samples[i][j].Crossing == true)
                    {
                        sb.Append(Repo.Samples[i][j].CrossingType);
                        sb.Append('\t');
                        sb.Append(Repo.Samples[i][j].NearestCrossingMidPrior);
                        sb.Append('\t');

                        if (Repo.Samples[i][j].NearestCrossingMidPrior != 0)
                        {
                            sb.Append(Repo.Samples[i][j].ImpliedFrequency);
                            sb.Append('\t');
                            sb.Append(Repo.Samples[i][j].ImpFreqInShiftRange);
                            sb.Append('\t');

                            if (Repo.Samples[i][j].ImpFreqInShiftRange == true)
                            {
                                sb.Append(Repo.Samples[i][j].MaxAmplitudeFound);
                                sb.Append('\t');
                                sb.Append(Repo.Samples[i][j].MaxAmplitudeSampleNum);
                                sb.Append('\t');
                                sb.Append(Repo.Samples[i][j].MinAmplitudeMet);
                                sb.Append('\t');

                                if (Repo.Samples[i][j].MinAmplitudeMet == true)
                                {
                                    sb.Append(Repo.Samples[i][j].HalfCyclePeakPositiveValue);
                                    sb.Append('\t');
                                    sb.Append(Repo.Samples[i][j].HalfCyclePeakPositiveSampleNum);
                                    sb.Append('\t');
                                    sb.Append(Repo.Samples[i][j].HalfCyclePeakNegativeValue);
                                    sb.Append('\t');
                                    sb.Append(Repo.Samples[i][j].HalfCyclePeakNegativeSampleNum);
                                    sb.Append('\t');
                                    sb.Append(Repo.Samples[i][j].Denominator);
                                    sb.Append('\t');
                                    sb.Append(Repo.Samples[i][j].Numerator);
                                    sb.Append('\t');
                                    sb.Append(Repo.Samples[i][j].MaxAmplitudeIncreasePC);
                                    sb.Append('\t');
                                    sb.Append(Repo.Samples[i][j].MaxAmplitudeIncreaseSampleNum);
                                    sb.Append('\t');
                                    sb.Append(Repo.Samples[i][j].MinAmplitudeIncreaseMet);
                                    sb.Append('\t');

                                    if (Repo.Samples[i][j].MinAmplitudeIncreaseMet == true)
                                    {
                                        sb.Append(Repo.Samples[i][j].StrikeDetected);
                                        sb.Append('\t');
                                    }
                                }
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
