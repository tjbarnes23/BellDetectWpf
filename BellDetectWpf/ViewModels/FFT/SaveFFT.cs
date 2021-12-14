using System;
using System.IO;
using System.Text;

namespace BellDetectWpf.ViewModels.FFT
{
    public static partial class C_FFT
    {
        public static void SaveFFT()
        {
            FFTVM.FilePathName = @"C:\temp\results.txt";

            StringBuilder sb = new StringBuilder();
            double time;
            double freq;

            if (File.Exists(FFTVM.FilePathName))
            {
                File.Delete(FFTVM.FilePathName);
            }

            // Create a new file     
            using FileStream fs = File.Create(FFTVM.FilePathName);

            Byte[] row = new UTF8Encoding(true).GetBytes("Bins down the page, Amplitude over time across the page\n");
            fs.Write(row, 0, row.Length);

            sb.Clear();
            sb.Append("Hz \\ sec\t");

            for (int j = 0; j < FFTVM.NA; j++)
            {
                time = Math.Round(((double)FFTVM.N / WavFileVM.SampleFrequency) * j, 3);
                sb.Append(time);
                sb.Append('\t');
            }
            
            sb.Append('\n');
            row = new UTF8Encoding(true).GetBytes(sb.ToString());
            fs.Write(row, 0, row.Length);

            for (int i = 0; i < FFTVM.N / 2; i++) // Only interested in bottom half of frequency buckets
            {
                sb.Clear();

                freq = Math.Round(((double)WavFileVM.SampleFrequency / FFTVM.N) * (i + 1), 3);
                sb.Append(freq);
                sb.Append('\t');

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
