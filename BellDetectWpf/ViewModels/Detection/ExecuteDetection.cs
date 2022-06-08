using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Enums;
using BellDetectWpf.Models;
using BellDetectWpf.Repository;

namespace BellDetectWpf.ViewModels
{
    public partial class DetectionVM
    {
        public static void ExecuteDetection(int inputChannel, int outputChannel,
                double lowLow, double lowHigh, double mid, double highLow, double highHigh)
        {
            int hand;
            SampleInfo si;

            CrossingTypeEnum ct;
            int j;
            bool found;
            int counter;

            // 
            if (inputChannel == 1)
            {
                hand = 0;
            }
            else
            {
                hand = 1;
            }

            // Loop through audio samples identifying crossings
            for (int i = 1; i < Repo.NumSamples; i++) // start at 1 because we compare to previous idx
            {
                // Look for crossing from negative to positive
                if (Repo.DetectionWaveformArr[inputChannel, i - 1] < 0 && Repo.DetectionWaveformArr[inputChannel, i] >= 0)
                {
                    si = new()
                    {
                        SampleNum = i,
                        Time = i / 48000.0,
                        Amplitude = Repo.DetectionWaveformArr[inputChannel, i],
                        Crossing = true,
                        CrossingType = CrossingTypeEnum.NegToPos
                    };

                    Repo.Samples[hand].Add(si);
                }
                else if (Repo.DetectionWaveformArr[inputChannel, i - 1] >= 0 && Repo.DetectionWaveformArr[inputChannel, i] < 0)
                {
                    si = new()
                    {
                        SampleNum = i,
                        Time = i / 48000.0,
                        Amplitude = Repo.DetectionWaveformArr[inputChannel, i],
                        Crossing = true,
                        CrossingType = CrossingTypeEnum.PosToNeg
                    };

                    Repo.Samples[hand].Add(si);
                }
                else
                {
                    si = new()
                    {
                        SampleNum = i,
                        Time = i / 48000.0,
                        Amplitude = Repo.DetectionWaveformArr[inputChannel, i],
                        Crossing = false,
                        CrossingType = CrossingTypeEnum.NA
                    };

                    Repo.Samples[hand].Add(si);
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

            // From starting point, we go back samplesMid, and then from there look at +/- range
            for (int i = samplesMid + range + 1; i < (Repo.NumSamples - ((Repo.AmplitudeIncreaseTS / 1000.0) * 48000) - 1); i++)
                    // start at samplesMid + range + 1 to avoid falling off the front of the array
                    // Finish at Repo.NumSamples - ((Repo.AmplitudeIncreaseTS / 1000.0) * 48000) - 1 to avoid falling off the end of the array
            {
                if (Repo.Samples[hand][i].Crossing == true)
                {
                    ct = Repo.Samples[hand][i].CrossingType;
                    j = i - samplesMid;
                    found = false;

                    for (int k = 0; k <= range; k++)
                    {
                        if (Repo.Samples[hand][j + k].Crossing == true && Repo.Samples[hand][j + k].CrossingType == ct)
                        {
                            Repo.Samples[hand][i].NearestCrossingMidPrior = samplesMid - k;
                            found = true;
                            break;
                        }

                        if (Repo.Samples[hand][j - k].Crossing == true && Repo.Samples[hand][j - k].CrossingType == ct)
                        {
                            Repo.Samples[hand][i].NearestCrossingMidPrior = samplesMid + k;
                            found = true;
                            break;
                        }
                    }

                    if (found == true)
                    {
                        Repo.Samples[hand][i].ImpliedFrequency = 48000.0 / Repo.Samples[hand][i].NearestCrossingMidPrior;
                    }

                    if ((Repo.Samples[hand][i].ImpliedFrequency >= lowLow && Repo.Samples[hand][i].ImpliedFrequency <= lowHigh) ||
                            (Repo.Samples[hand][i].ImpliedFrequency >= highLow && Repo.Samples[hand][i].ImpliedFrequency <= highHigh))
                    {
                        Repo.Samples[hand][i].ImpFreqInShiftRange = true;

                        // Ensure amplitude reached at least amplitudeCutoff during the past n cycles where n = Repo.AmplitudeLookback
                        int lk = samplesMid * Repo.AmplitudeLookback;

                        for (int k = Math.Max(i - lk, 0); k <= i; k++)
                        {
                            if (Repo.Samples[hand][k].Amplitude >= Repo.AmplitudeCutoff)
                            {
                                Repo.Samples[hand][i].AmpCutoffMet = true;
                                break;
                            }
                        }

                        if (Repo.Samples[hand][i].AmpCutoffMet == true)
                        {
                            // Run tests to ensure there is a sufficient amplitude increase over the specified
                            // following timespan

                            // Get the peak absolute amplitude in the half cycle prior to time zero.
                            int priorHalfCyclePeak = 0;

                            for (int k = i - samplesMid / 2; k <= i; k++)
                            {
                                if (Math.Abs(Repo.Samples[hand][k].Amplitude) > priorHalfCyclePeak)
                                {
                                    priorHalfCyclePeak = Repo.Samples[hand][k].Amplitude;
                                }
                            }

                            // Get positive and negative peaks over next Repo.AmplitudeIncreaseTS timespan
                            int posPeak = 0;
                            int negPeak = 0;

                            for (int k = i; k <= i + ((Repo.AmplitudeIncreaseTS / 1000.0) * 48000); k++)
                            {
                                if (Repo.Samples[hand][k].Amplitude > posPeak)
                                {
                                    posPeak = Repo.Samples[hand][k].Amplitude;
                                }

                                if (Repo.Samples[hand][k].Amplitude < negPeak)
                                {
                                    negPeak = Repo.Samples[hand][k].Amplitude;
                                }
                            }

                            if (posPeak > priorHalfCyclePeak * (1 + (Repo.AmplitudeIncreasePC / 100.0)) && negPeak * -1 > priorHalfCyclePeak * (1 + (Repo.AmplitudeIncreasePC / 100.0)))
                            {
                                Repo.Samples[hand][i].AmpIncreaseMet = true;
                                Repo.Samples[hand][i].StrikeDetected = true;

                                // Set the counter that controls the writing out of the square wave
                                counter = 480;
                            }
                        }
                    }
                }


                if (counter > 0)
                {
                    Repo.DetectionWaveformArr[outputChannel, i] = 20000;
                    counter--;
                }
            }
        }
    }
}
