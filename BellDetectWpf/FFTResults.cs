using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf
{
    public static class FFTResults
    {
        public static void CreateResultsFile()
        {
            string fileName = @"C:\temp\results.txt";
            StringBuilder sb = new StringBuilder();
            double bin;
            double amplitude;
            double phase;

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            // Create a new file     
            using FileStream fs = File.Create(fileName);

            Byte[] row = new UTF8Encoding(true).GetBytes("Bin\tAmplitude\tPhase\n");
            fs.Write(row, 0, row.Length);

            for (int i = 0; i < 256; i++)
            {
                bin = (44100 / 256) * (i + 1);
                amplitude = Math.Sqrt(Math.Pow(ViewModel.XRe[i], 2) + Math.Pow(ViewModel.XIm[i], 2));
                phase = Math.Atan2(ViewModel.XIm[i], ViewModel.XRe[i]);

                amplitude = Math.Round(amplitude, 3);
                phase = Math.Round(phase, 3);

                sb.Clear();
                sb.Append(bin);
                sb.Append('\t');
                sb.Append(amplitude);
                sb.Append('\t');
                sb.Append(phase);
                sb.Append('\n');

                row = new UTF8Encoding(true).GetBytes(sb.ToString());
                fs.Write(row, 0, row.Length);
            }
        }
    }
}
