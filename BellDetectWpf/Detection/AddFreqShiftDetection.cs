using System;
using System.Collections.Generic;
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
        public static void AddFreqShiftDetection(short[,] filteredWaveformArr)
        {
            int numSamples;
            int nextTestPoint;
            int lastCrossingSample;
            int counter;

            List<ZeroCrossing> crossings;
            ZeroCrossing zc;
            List<int> block960;
            List<int> block240;

            double average960;
            double sumOfSquaresOfDifferences;
            double sD960;
            double average240;

            StringBuilder sb;

            numSamples = filteredWaveformArr.GetLength(1);
            nextTestPoint = 1200;
            lastCrossingSample = 0;
            counter = 0;

            crossings = new();

            sb = new();
            sb.Append($"Sample #\t");
            sb.Append($"960 # crossings\t");
            sb.Append($"960 avg\t");
            sb.Append($"960 sd\t");
            sb.Append($"240 # crossings\t");
            sb.Append($"240 avg\t");
            sb.Append($"Strike?\t");
            
            Repo.Logger.Info(sb.ToString());

            // Loop through audio samples
            for (int i = 1; i < numSamples; i++) // start at 1 because we compare to previous idx
            {
                // Look for crossing from negative to positive
                if (filteredWaveformArr[0, i - 1] < 0 && filteredWaveformArr[0, i] >= 0)
                {
                    zc = new()
                    {
                        SampleNum = i,
                        CrossingType = CrossingTypeEnum.NegToPos,
                        SampleInterval = i - lastCrossingSample
                    };

                    crossings.Add(zc);
                    lastCrossingSample = i;
                }
                else if (filteredWaveformArr[0, i - 1] >= 0 && filteredWaveformArr[0, i] < 0)
                {
                    zc = new()
                    {
                        SampleNum = i,
                        CrossingType = CrossingTypeEnum.PosToNeg,
                        SampleInterval = i - lastCrossingSample
                    };

                    crossings.Add(zc);
                    lastCrossingSample = i;
                }

                // Have we reached nextSamplePoint?
                if (i == nextTestPoint)
                {
                    sb.Clear();
                    sb.Append($"{i}\t");

                    // Remove all items in crossings that have a sample num less than (nextTestPoint - 1199)
                    crossings.RemoveAll(z => z.SampleNum < nextTestPoint - 1199);

                    // Test for steady crossing intervals between (nextTestPoint - 1199) and (nextTestPoint - 240)
                    // Steady crossing intervals means SD is less than 5 samples
                    block960 = crossings
                            .Where(z => z.SampleNum >= nextTestPoint - 1199 && z.SampleNum <= nextTestPoint - 240)
                            .Select(z => z.SampleInterval)
                            .ToList();

                    average960 = block960.Average();
                    
                    sumOfSquaresOfDifferences = block960
                            .Select(val => Math.Pow(val - average960, 2))
                            .Sum();
                    
                    sD960 = Math.Sqrt(sumOfSquaresOfDifferences / block960.Count);

                    sb.Append($"{block960.Count}\t");
                    sb.Append($"{Math.Round(average960,2)}\t");
                    sb.Append($"{Math.Round(sD960,2)}\t");

                    // Calculate the average crossing interval from
                    // (nextTestPoint - 239) and (nextTestPoint)
                    block240 = crossings
                        .Where(z => z.SampleNum >= nextTestPoint - 239 && z.SampleNum <= nextTestPoint)
                        .Select(z => z.SampleInterval)
                        .ToList();

                    average240 = block240.Average();

                    sb.Append($"{block240.Count}\t");
                    sb.Append($"{Math.Round(average240,2)}\t");

                    // If average for 240 is 5 more or 5 less than average for 960, then a strike is detected
                    if (average240 > average960 + 5 || average240 < average960 - 5)
                    {
                        // Strike is detected
                        // Set the counter that controls the writing out of the square wave
                        counter = 480;

                        // Advance the nextTestPoint by 1200
                        nextTestPoint += 1200;

                        sb.Append($"Detected\t");
                    }
                    else
                    {
                        // Strike is not detected,
                        // Advance the nextTestPoint by 240
                        nextTestPoint += 240;
                    }

                    Repo.Logger.Info(sb.ToString());
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
