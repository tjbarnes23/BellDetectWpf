using System;
using System.IO;
using System.Text;

namespace BellDetectWpf.ViewModels.FFT
{
    public static partial class C_FFT
    {
        public static void SaveFFT()
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

            // *** Update this to be parameter driven depending on sample frequency and number of bins to be used ***

            for (int i = 0; i < 256; i++) // Only interested in bottom 1/4 of frequency buckets
            {
                bin = (96000 / 1024) * (i + 1);
                amplitude = Math.Sqrt(Math.Pow(FFTVM.XRe[i], 2) + Math.Pow(FFTVM.XIm[i], 2));
                phase = Math.Atan2(FFTVM.XIm[i], FFTVM.XRe[i]);

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
