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

            // Loop through audio samples
            for (int i = 1; i < Repo.NumSamples; i++) // start at 1 because we compare to previous idx
            {
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

                    // Looking for average to be within 1/514 and 1/534 (filter is centered at 524 Hz)
                    double lo = 48000.0 / 534.0;
                    double hi = 48000.0 / 514.0;

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
                        // Set counter to 480 (1ms at 48 kHz sample rate)
                        // Write out value of 20000 to array for the next 480 samples (1 ms)
                        counter = 480;
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
