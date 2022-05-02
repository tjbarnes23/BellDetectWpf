using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Enums;
using BellDetectWpf.Models;
using BellDetectWpf.Repository;

namespace BellDetectWpf.Detection
{
    public static partial class Detect
    {
        public static void DetectionV2(short[,] filteredWaveformArr, int inputChannel, int outputChannel, int amplitudeCutoff,
                double lowLow, double lowHigh, double mid, double highLow, double highHigh, bool isLeft)
        {
            int numSamples;
            SampleInfo si;

            CrossingTypeEnum ct;
            int j;
            bool found;
            int counter;

            numSamples = filteredWaveformArr.GetLength(1);
            Repo.Samples = new();

            // Loop through audio samples identifying crossings
            for (int i = 1; i < numSamples; i++) // start at 1 because we compare to previous idx
            {
                // Look for crossing from negative to positive
                if (filteredWaveformArr[inputChannel, i - 1] < 0 && filteredWaveformArr[inputChannel, i] >= 0)
                {
                    si = new()
                    {
                        SampleNum = i,
                        Time = i / 48000.0,
                        Amplitude = filteredWaveformArr[0, i],
                        Crossing = true,
                        CrossingType = CrossingTypeEnum.NegToPos
                    };

                    Repo.Samples.Add(si);
                }
                else if (filteredWaveformArr[inputChannel, i - 1] >= 0 && filteredWaveformArr[inputChannel, i] < 0)
                {
                    si = new()
                    {
                        SampleNum = i,
                        Time = i / 48000.0,
                        Amplitude = filteredWaveformArr[inputChannel, i],
                        Crossing = true,
                        CrossingType = CrossingTypeEnum.PosToNeg
                    };

                    Repo.Samples.Add(si);
                }
                else
                {
                    si = new()
                    {
                        SampleNum = i,
                        Time = i / 48000.0,
                        Amplitude = filteredWaveformArr[inputChannel, i],
                        Crossing = false,
                        CrossingType = CrossingTypeEnum.NA
                    };

                    Repo.Samples.Add(si);
                }
            }

            // Loop through samples
            // if sample is a crossing, find closest sample to 92 samples prior that is a matching crossing
            // Use the sample gap to calculate implied frequency
            // If implied frequency < 510 or > 535, mark that a strike is detected
            counter = 0;

            int samplesLowLow = Convert.ToInt32(48000 / lowLow);
            int samplesMid = Convert.ToInt32(48000 / mid);
            int samplesHighHigh = Convert.ToInt32(48000 / highHigh);

            int range = Math.Max(Math.Abs(samplesLowLow - samplesMid), Math.Abs(samplesMid - samplesHighHigh));

            for (int i = samplesMid + range + 1; i < (numSamples - 1); i++) // start at samplesMid + range + 1 to avoid falling off the front of the array
            {
                if (Repo.Samples[i].Crossing == true)
                {
                    ct = Repo.Samples[i].CrossingType;
                    j = i - samplesMid;
                    found = false;

                    for (int k = 0; k <= range; k++)
                    {
                        if (Repo.Samples[j + k].Crossing == true && Repo.Samples[j + k].CrossingType == ct)
                        {
                            Repo.Samples[i].NearestCrossingMidPrior = samplesMid - k;
                            found = true;
                            break;
                        }

                        if (Repo.Samples[j - k].Crossing == true && Repo.Samples[j - k].CrossingType == ct)
                        {
                            Repo.Samples[i].NearestCrossingMidPrior = samplesMid + k;
                            found = true;
                            break;
                        }
                    }

                    if (found == true)
                    {
                        Repo.Samples[i].ImpliedFrequency = 48000.0 / Repo.Samples[i].NearestCrossingMidPrior;
                    }

                    if ((Repo.Samples[i].ImpliedFrequency >= lowLow && Repo.Samples[i].ImpliedFrequency <= lowHigh) ||
                            (Repo.Samples[i].ImpliedFrequency >= highLow && Repo.Samples[i].ImpliedFrequency <= highHigh))
                    {
                        found = false;

                        // Ensure amplitude reached at least amplitudeCutoff during this cycle
                        for (int k = i - Repo.Samples[i].NearestCrossingMidPrior; k <= i; k++)
                        {
                            if (Repo.Samples[k].Amplitude >= amplitudeCutoff)
                            {
                                found = true;
                                break;
                            }
                        }

                        if (found == true)
                        {
                            Repo.Samples[i].StrikeDetected = true;

                            // Set the counter that controls the writing out of the square wave
                            counter = 480;
                        }
                    }
                }

                if (counter > 0)
                {
                    filteredWaveformArr[outputChannel, i] = 20000;
                    counter--;
                }
            }

            // Write detection data to text
            if (isLeft == true)
            {
                WriteDetection(@"C:\ProgramData\BellDetect\DetectionLeft.txt", Repo.LeftLowLow, Repo.LeftLowHigh, Repo.LeftMid, Repo.LeftHighLow, Repo.LeftHighHigh, Repo.AmplitudeCutoff);
            }
            else
            {
                WriteDetection(@"C:\ProgramData\BellDetect\DetectionRight.txt", Repo.RightLowLow, Repo.RightLowHigh, Repo.RightMid, Repo.RightHighLow, Repo.RightHighHigh, Repo.AmplitudeCutoff);
            }
        }
    }
}
