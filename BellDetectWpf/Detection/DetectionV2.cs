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
        public static void DetectionV2(short[,] filteredWaveformArr)
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
                if (filteredWaveformArr[0, i - 1] < 0 && filteredWaveformArr[0, i] >= 0)
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
                else if (filteredWaveformArr[0, i - 1] >= 0 && filteredWaveformArr[0, i] < 0)
                {
                    si = new()
                    {
                        SampleNum = i,
                        Time = i / 48000.0,
                        Amplitude = filteredWaveformArr[0, i],
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
                        Amplitude = filteredWaveformArr[0, i],
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

            for (int i = 150; i < (numSamples - 1); i++) // start at 150 because we go back 92 samples and then look up to +/- 50 from that sample
            {
                if (Repo.Samples[i].Crossing == true)
                {
                    ct = Repo.Samples[i].CrossingType;
                    j = i - 92;
                    found = false;

                    for (int k = 0; k < 50; k++)
                    {
                        if (Repo.Samples[j + k].Crossing == true && Repo.Samples[j + k].CrossingType == ct)
                        {
                            Repo.Samples[i].NearestCrossing92Prior = 92 - k;
                            found = true;
                            break;
                        }

                        if (Repo.Samples[j - k].Crossing == true && Repo.Samples[j - k].CrossingType == ct)
                        {
                            Repo.Samples[i].NearestCrossing92Prior = 92 + k;
                            found = true;
                            break;
                        }
                    }

                    if (found == true)
                    {
                        Repo.Samples[i].ImpliedFrequency = 48000.0 / Repo.Samples[i].NearestCrossing92Prior;
                    }

                    if (Repo.Samples[i].ImpliedFrequency < 472.5 || Repo.Samples[i].ImpliedFrequency > 572.5)
                    {
                        found = false;

                        // Ensure amplitude reached at least 1000 during this cycle
                        for (int k = i - Repo.Samples[i].NearestCrossing92Prior; k <= i; k++)
                        {
                            if (Repo.Samples[k].Amplitude >= 1000)
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
                    filteredWaveformArr[1, i] = 20000;
                    counter--;
                }
            }
        }
    }
}
