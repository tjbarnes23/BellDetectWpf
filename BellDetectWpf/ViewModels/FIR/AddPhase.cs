using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Repository;

namespace BellDetectWpf.ViewModels
{
    public partial class FIRVM
    {
        public static void AddPhase()
        {
            int[] zeroPoints;
            int[] zeroGaps;
            bool prevSteadyPhase;
            bool steadyPhase;
            int counter;

            zeroPoints = new int[4];
            zeroGaps = new int[3];
            prevSteadyPhase = false;
            steadyPhase = false;
            counter = 0;

            short maxAmp;

            maxAmp = 0;

            // Loop through audio samples
            for (int i = 1; i < Repo.NumSamples; i++) // start at 1 because we compare to previous idx
            {
                // Keep track of the max amplitude so we can filter out detected phase shifts at relatively low amplitude
                if (Repo.FIRFilteredWaveformArr[0, i] > maxAmp)
                {
                    maxAmp = Repo.FIRFilteredWaveformArr[0, i];
                }

                // Look for zero crossing from negative to positive
                if (Repo.FIRFilteredWaveformArr[0, i - 1] < 0 && Repo.FIRFilteredWaveformArr[0, i] >= 0)
                {
                    prevSteadyPhase = steadyPhase;

                    // Move last 3 recording back 1 index, discarding the earliest 
                    zeroPoints[0] = zeroPoints[1];
                    zeroPoints[1] = zeroPoints[2];
                    zeroPoints[2] = zeroPoints[3];

                    zeroGaps[0] = zeroGaps[1];
                    zeroGaps[1] = zeroGaps[2];

                    // Record current point
                    zeroPoints[3] = i;
                    zeroGaps[2] = zeroPoints[3] - zeroPoints[2];

                    // Test for 3 intervals to be similar
                    double average = zeroGaps.Average();
                    double sumOfSquaresOfDifferences = zeroGaps.Select(val => Math.Pow(val - average, 2)).Sum();
                    double sd = Math.Sqrt(sumOfSquaresOfDifferences / zeroGaps.Length);

                    // Looking for average to be within 1/510 and 1/535 (prime frequency is 522.5 Hz)
                    double lo = 48000.0 / 540.0;
                    double hi = 48000.0 / 505.0;

                    steadyPhase = false;

                    if (average >= lo && average <= hi)
                    {
                        // Look for sd to be less than 
                        if (sd / average < 0.05)
                        {
                            steadyPhase = true;
                        }
                    }

                    if (steadyPhase == false && prevSteadyPhase == true)
                    {
                        // There has been a phase shift

                        // Get max amplitude over the past 100 samples
                        
                        int k = i - 99;

                        if (k < 0)
                        {
                            k = 0;
                        }

                        List<short> last100 = new();
                        
                        for (int j = k; j <= i; j++)
                        {
                            last100.Add(Repo.FIRFilteredWaveformArr[0, j]);
                        }

                        short last100max = last100.Max();

                        // If last100max is less than 25% of maxAmp, it can't be a strike, so don't record a phase shift
                        // Also include an absolute cutoff of 500
                        if (last100max >= maxAmp * 0.25 && last100max >= 500)
                        {
                            // Set counter to 480 (1ms at 48 kHz sample rate)
                            // Write out value of 20000 to array for the next 480 samples (1 ms)
                            counter = 480;
                        }
                    }
                }

                if (counter > 0)
                {
                    Repo.FIRFilteredWaveformArr[1, i] = 20000;
                    counter--;
                }
            }
        }
    }
}
