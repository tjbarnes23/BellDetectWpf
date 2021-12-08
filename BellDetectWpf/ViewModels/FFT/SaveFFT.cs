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

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            // Create a new file     
            using FileStream fs = File.Create(fileName);

            Byte[] row = new UTF8Encoding(true).GetBytes("Bins down the page, Amplitude over time across the page\n");
            fs.Write(row, 0, row.Length);

            // *** Update this to be parameter driven depending on sample frequency and number of bins to be used ***

            for (int i = 0; i < FFTVM.N / 4; i++) // Only interested in bottom 1/4 of frequency buckets
            {
                sb.Clear();

                for (int j = 0; j < FFTVM.NA; j++)
                {
                    sb.Append(FFTVM.Results[i, j]);
                    sb.Append('\t');
                }

                sb.Append('\n');
                row = new UTF8Encoding(true).GetBytes(sb.ToString());
                fs.Write(row, 0, row.Length);
            }
        }
    }
}
