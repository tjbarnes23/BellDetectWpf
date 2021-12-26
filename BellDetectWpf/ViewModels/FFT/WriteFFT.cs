using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.ViewModels.Shared;

namespace BellDetectWpf.ViewModels.FFT
{
    public static partial class C_FFT
    {
        public static async Task WriteFFT()
        {
            StringBuilder sb = new StringBuilder();
            double time;
            double freq;

            SharedVM.StatusMsg = "Saving...";
            SharedVM.StatusForeground = "black";

            // Delete file if it already exists
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
                time = Math.Round(((double)FFTVM.N / WaveformVM.SampleFrequency) * j, 3);
                sb.Append(time);
                sb.Append('\t');
            }
            
            sb.Append('\n');
            row = new UTF8Encoding(true).GetBytes(sb.ToString());
            fs.Write(row, 0, row.Length);

            for (int i = 0; i < FFTVM.N / 2; i++) // Only interested in bottom half of frequency buckets
            {
                sb.Clear();

                freq = Math.Round(((double)WaveformVM.SampleFrequency / FFTVM.N) * i, 1);
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

            await C_Shared.Status("Saved", "black", 3000);
        }
    }
}
