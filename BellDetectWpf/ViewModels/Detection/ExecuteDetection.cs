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

            // Create a sampleInfo record for the first sample (can't do a comparison on this item)
            SampleInfo si = new()
            {
                SampleNum = 0,
                Time = 0 / 48000.0,
                Amplitude = Repo.DetectionWaveformArr[inputChannel, 0],
                Crossing = false,
                CrossingType = CrossingTypeEnum.NA
            };

            Repo.Samples[hand].Add(si);

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

            /*******************************************************************************
            * At this point all the crossings are identified
            * 
            * Now add the following to the sample info objects:
            * If sample is a crossing, find closest sample to the mid number of samples
            * prior that is a crossing in the same direction
            * From the resulting number of samples, calculate implied frequency
            * If implied frequency is within the range defined by lowLow and highLow,
            * or within the range defined by lowHigh and highHigh,
            * then mark that a specified frequency shift has been detected
            *******************************************************************************/
            
            int samplesLowLow = Convert.ToInt32(48000 / lowLow);
            int samplesMid = Convert.ToInt32(48000 / mid);
            int samplesHighHigh = Convert.ToInt32(48000 / highHigh);

            // range represents the furthest we need to look for finding a matching crossing
            int range = Math.Max(Math.Abs(samplesLowLow - samplesMid), Math.Abs(samplesMid - samplesHighHigh));

            // Make sure range is less than (samplesMid - 1)
            if (range >= (samplesMid - 1))
            {
                range = samplesMid - 2;
            }

            // From starting point, we go back samplesMid, and then from there look at +/- range
            // Start at samplesMid + range to avoid falling off the front of the array

            for (int i = samplesMid + range; i < Repo.NumSamples; i++)
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
            * 
            * If freq shift occurs at a PosToNeg crossing,
            * record peak positive amplitude in the half cycle prior to the crossing,
            * and record peak negative amplitude in the half cycle following the crossing
            * 
            * Vice versa for a NegToPos crossing
            * 
            * Calculated the average of the absolute of these two peaks. This is the denominator
            * for the amplitude increase test
            * 
            * 2. Test if MinAmplitudeValue is found in the next MinAmplitudeCycles following the freq shift detection
            * 
            * 3. If MinAmplitude reached, then
            * get max absolute amplitude in the MinAmplitudeIncreaseTS timespan following the freq shift detection
            * and divide this by the value found in 1. above.
            * If the result is greater than MinAmplitudeIncreasePC, then mark a strike as detected
            *******************************************************************************/

            // Finish at Repo.NumSamples - numMinAmplitudeSamples - numMinAmplitudeIncreaseSamples to avoid falling off the end of the array

            int numHalfCycleSamples = samplesMid / 2;
            int numMinAmplitudeSamples = samplesMid * Repo.MinAmplitudeCycles;
            int numMinAmplitudeIncreaseSamples = (int)(48000 * (Repo.MinAmplitudeIncreaseTS / 1000.0));

            int maxOffset = Math.Max(numMinAmplitudeSamples, numMinAmplitudeIncreaseSamples);
            
            int counter = 0;

            int maxAmplitudeFound = 0;
            int maxAmplitudeSampleNum = 0;

            int maxPosAmplitudeFound = 0;
            int maxPosAmplitudeSampleNum = 0;
            int maxNegAmplitudeFound = 0;
            int maxNegAmplitudeSampleNum = 0;

            for (int i = 0; i < (Repo.NumSamples - maxOffset); i++)
            {
                if (Repo.Samples[hand][i].ImpFreqInShiftRange == true)
                {
                    // Test whether amplitude reached at least Repo.MinAmplitudeValue during the next n cycles where n = Repo.MinAmplitudeCycles
                    for (int j = i; j < i + numMinAmplitudeSamples; j++)
                    {
                        if (Math.Abs(Repo.Samples[hand][j].Amplitude) > maxAmplitudeFound)
                        {
                            maxAmplitudeFound = Math.Abs(Repo.Samples[hand][j].Amplitude);
                            maxAmplitudeSampleNum = j;
                        }
                    }

                    Repo.Samples[hand][i].MaxAmplitudeFound = maxAmplitudeFound;
                    Repo.Samples[hand][i].MaxAmplitudeSampleNum = maxAmplitudeSampleNum;

                    if (maxAmplitudeFound >= Repo.MinAmplitudeValue)
                    {
                        // Record location of maxAmplitude found
                        Repo.Samples[hand][i].MinAmplitudeMet = true;

                        // Calc denominator
                        if (Repo.Samples[hand][i].CrossingType == CrossingTypeEnum.PosToNeg)
                        {
                            // Get positive peak amplitude in prior half cycle 
                            for (int j = i; j > i - numHalfCycleSamples; j--)
                            {
                                if (Repo.Samples[hand][j].Amplitude > maxPosAmplitudeFound)
                                {
                                    maxPosAmplitudeFound = Repo.Samples[hand][j].Amplitude;
                                    maxPosAmplitudeSampleNum = j;
                                }
                            }

                            // Get negative peak amplitude in next half cycle
                            for (int j = i; j < i + numHalfCycleSamples; j++)
                            {
                                if (Repo.Samples[hand][j].Amplitude < maxNegAmplitudeFound)
                                {
                                    maxNegAmplitudeFound = Repo.Samples[hand][j].Amplitude;
                                    maxNegAmplitudeSampleNum = j;
                                }
                            }
                        }
                        else
                        {
                            // Get negative peak amplitude in prior half cycle 
                            for (int j = i; j > i - numHalfCycleSamples; j--)
                            {
                                if (Repo.Samples[hand][j].Amplitude < maxNegAmplitudeFound)
                                {
                                    maxNegAmplitudeFound = Repo.Samples[hand][j].Amplitude;
                                    maxNegAmplitudeSampleNum = j;
                                }
                            }

                            // Get positive peak amplitude in next half cycle
                            for (int j = i; j < i + numHalfCycleSamples; j++)
                            {
                                if (Repo.Samples[hand][j].Amplitude > maxPosAmplitudeFound)
                                {
                                    maxPosAmplitudeFound = Repo.Samples[hand][j].Amplitude;
                                    maxPosAmplitudeSampleNum = j;
                                }
                            }
                        }

                        int denom = (Math.Abs(maxNegAmplitudeFound) + maxPosAmplitudeFound) / 2;
                        
                        // Write out pos, neg and average peaks
                        Repo.Samples[hand][i].HalfCyclePeakPositiveValue = maxPosAmplitudeFound;
                        Repo.Samples[hand][i].HalfCyclePeakPositiveSampleNum = maxPosAmplitudeSampleNum;
                        Repo.Samples[hand][i].HalfCyclePeakNegativeValue = maxNegAmplitudeFound;
                        Repo.Samples[hand][i].HalfCyclePeakNegativeSampleNum = maxNegAmplitudeSampleNum;
                        Repo.Samples[hand][i].Denominator = denom;

                        // Test whether the max amplitude found in the next MinAmplitudeIncreaseTS after the
                        // freq shift increases by at least the percent specified
                        // by MinAmplitudeIncreasePC 

                        int maxNumeratorFound = 0;
                        int maxNumeratorSampleNum = 0;

                        for (int j = i; j < i + numMinAmplitudeIncreaseSamples; j++)
                        {
                            if (Math.Abs(Repo.Samples[hand][j].Amplitude) > maxNumeratorFound)
                            {
                                maxNumeratorFound = Math.Abs(Repo.Samples[hand][j].Amplitude);
                                maxNumeratorSampleNum = j;
                            }
                        }

                        int maxAmplitudeIncreasePC = (int)(((maxNumeratorFound / (double)denom) - 1) * 100);

                        Repo.Samples[hand][i].Numerator = maxNumeratorFound;
                        Repo.Samples[hand][i].MaxAmplitudeIncreasePC = maxAmplitudeIncreasePC;
                        Repo.Samples[hand][i].MaxAmplitudeIncreaseSampleNum = maxNumeratorSampleNum;

                        if (maxAmplitudeIncreasePC >= Repo.MinAmplitudeIncreasePC)
                        {
                            Repo.Samples[hand][i].MinAmplitudeIncreaseMet = true;

                            // Record that a strike was detected
                        
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
