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
                    SampleInfo si = new()
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
                    SampleInfo si = new()
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
                    SampleInfo si = new()
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

            /*******************************************************************************
            * At this point all the crossings are identified
            * 
            * Now add the following to the sample info objects:
            * If sample is a crossing, find closest sample to the mid number of samples
            * prior that is a crossing in the same direction
            * From the resulting number of samples, calculate implied frequency
            * If implied frequency is within the range defined by lowLow and highLow,
            * or within the range defined by lowHigh and highHigh,
            * then mark that a strike is detected
            *******************************************************************************/
            
            int samplesLowLow = Convert.ToInt32(48000 / lowLow);
            int samplesMid = Convert.ToInt32(48000 / mid);
            int samplesHighHigh = Convert.ToInt32(48000 / highHigh);

            // range represents the furthest we need to look for finding a matching crossing
            int range = Math.Max(Math.Abs(samplesLowLow - samplesMid), Math.Abs(samplesMid - samplesHighHigh));

            int numMinAmplitudeSamples = samplesMid * Repo.MinAmplitudeCycles;
            int numMinAmplitudeIncreaseSamples = (int)(48000 * (Repo.MinAmplitudeIncreaseTS / 1000.0));

            // From starting point, we go back samplesMid, and then from there look at +/- range
            // start at samplesMid + range to avoid falling off the front of the array
            // Finish at Repo.NumSamples - numMinAmplitudeSamples - numMinAmplitudeIncreaseSamples to avoid falling off the end of the array

            for (int i = samplesMid + range; i < (Repo.NumSamples - numMinAmplitudeSamples - numMinAmplitudeIncreaseSamples); i++)
            {
                if (Repo.Samples[hand][i].Crossing == true)
                {
                    CrossingTypeEnum ct = Repo.Samples[hand][i].CrossingType;
                    int j = i - samplesMid;
                    bool found = false;

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

                        if ((Repo.Samples[hand][i].ImpliedFrequency >= lowLow && Repo.Samples[hand][i].ImpliedFrequency <= lowHigh) ||
                                (Repo.Samples[hand][i].ImpliedFrequency >= highLow && Repo.Samples[hand][i].ImpliedFrequency <= highHigh))
                        {
                            Repo.Samples[hand][i].ImpFreqInShiftRange = true;
                        }
                    }
                }
            }

            /*******************************************************************************
            * At this point all the prior crossings are indentified and their
            * implied frequencies are recorded
            * The sampleInfo objects are also marked with whether the implied frequency is
            * within the range we are looking for
            *
            * Now loop through the sampleInfo objects again
            * If a sample has ImpFreqInShiftRange == true, then
            * see if MinAmplitudeValue is found in the next MinAmplitudeCycles
            * 
            * If the above is found, then
            * see if MinAmplitudeIncreasePC is found in the next MinAmplitudeIncreaseTS
            *******************************************************************************/

            int counter = 0;

            for (int i = 0; i < (Repo.NumSamples - numMinAmplitudeSamples - numMinAmplitudeIncreaseSamples); i++)
            { 
                if (Repo.Samples[hand][i].ImpFreqInShiftRange == true)
                {
                    int maxAmplitudeFound = 0;
                    int maxAmplitudeSampleNum = 0;

                    // Test whether amplitude reached at least Repo.MinAmplitudeValue during the next n cycles where n = Repo.MinAmplitudeCycles
                    for (int j = i; j < i + numMinAmplitudeSamples; j++)
                    {
                        if (Math.Abs(Repo.Samples[hand][j].Amplitude) > maxAmplitudeFound)
                        {
                            maxAmplitudeFound = Math.Abs(Repo.Samples[hand][j].Amplitude);
                            maxAmplitudeSampleNum = j;
                        }
                    }

                    if (maxAmplitudeFound >= Repo.MinAmplitudeValue)
                    {
                        Repo.Samples[hand][i].MinAmplitudeMet = true;
                        Repo.Samples[hand][i].MaxAmplitudeFound = maxAmplitudeFound;
                        Repo.Samples[hand][i].MaxAmplitudeSampleNum = maxAmplitudeSampleNum;
                    }

                    // If the minimum amplitude was met in the previous test,
                    // check whether the max amplitude found increases by at least the percent specified
                    // by MinAmplitudeIncreasePC in the next MinAmplitudeIncreaseTS

                    if (Repo.Samples[hand][i].MinAmplitudeMet == true)
                    {
                        int maxAmplitudeIncreaseFound = 0;
                        int maxAmplitudeIncreaseSampleNum = 0;

                        for (int j = i + numMinAmplitudeSamples; j < i + numMinAmplitudeSamples + numMinAmplitudeIncreaseSamples; j++)
                        {
                            if (Math.Abs(Repo.Samples[hand][j].Amplitude) > maxAmplitudeIncreaseFound)
                            {
                                maxAmplitudeIncreaseFound = Math.Abs(Repo.Samples[hand][j].Amplitude);
                                maxAmplitudeIncreaseSampleNum = j;
                            }
                        }

                        double maxAmplitudeIncreasePC = maxAmplitudeIncreaseFound / (double)maxAmplitudeFound;

                        if (maxAmplitudeIncreasePC >= (1 + (Repo.MinAmplitudeIncreasePC / 100.0)))
                        {
                            Repo.Samples[hand][i].MinAmplitudeIncreaseMet = true;
                            Repo.Samples[hand][i].MaxAmplitudeIncreaseFound = maxAmplitudeIncreasePC;
                            Repo.Samples[hand][i].MaxAmplitudeIncreaseSampleNum = maxAmplitudeIncreaseSampleNum;
                        }

                        // If the minimum amplitude increase was met in the previous test,
                        // record that a strike was detected

                        if (Repo.Samples[hand][i].MinAmplitudeIncreaseMet == true)
                        {
                            Repo.Samples[hand][i].StrikeDetected = true;
                            counter = 480;
                        }
                    }
                }

                // Write out strike detection signal to waveform
                if (counter > 0)
                {
                    Repo.DetectionWaveformArr[outputChannel, i] = 20000;
                    counter--;
                }
            }
        }
    }
}
