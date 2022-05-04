
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Repository;

namespace BellDetectWpf.Detection
{
    public static partial class Detect
    {
        public static void WriteDetection(string filePathName, double lowLow, double lowHigh, double mid, double highLow, double highHigh, int amplitudeCutoff)
        {
            StringBuilder sb;

            /**************************************************
            * Write detection data to text file
            **************************************************/

            sb = new();
            sb.Append(filePathName);
            sb.Append(".txt");
            filePathName = sb.ToString();

            // Delete file if it already exists
            if (File.Exists(filePathName))
            {
                File.Delete(filePathName);
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
            sb.Append($"Strike? ((>= {lowLow} Hz and >= {lowHigh} Hz) or (>= {highLow} Hz and <= {highHigh} Hz)) and (amplitude >= {amplitudeCutoff})\t");

            sw.WriteLine(sb.ToString());

            // Write data rows
            for (int i = 0; i < Repo.Samples.Count; i++)
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
    }
}
