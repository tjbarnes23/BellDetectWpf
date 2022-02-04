using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.ViewModels.Shared;

namespace BellDetectWpf.ViewModels.Elliptic
{
    public static partial class C_Elliptic
    {
        public static async Task WriteElliptic()
        {
            StringBuilder sb;
            byte[] row;
            double time;
            double freq;

            await C_Shared.Status("Saving FFTs...", "black", 10, false);

            // Delete file if it already exists
            if (File.Exists(EllipticVM.FilePathName))
            {
                File.Delete(EllipticVM.FilePathName);
            }

            // Create a new file     
            using FileStream fs = File.Create(FFTVM.FilePathName);

            // Write description row
            sb = new StringBuilder();
            sb.Clear();
            sb.Append("Bins down the page, Amplitude over time across the page\n");
            
            row = new UTF8Encoding(true).GetBytes(sb.ToString());
            fs.Write(row, 0, row.Length);

            // Write header row
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

            // Write data rows
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

            await C_Shared.Status("Filtered waveform saved", "black", 3000, true);
        }
    }
}
