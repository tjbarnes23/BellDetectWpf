using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Repository;

namespace BellDetectWpf.ViewModels
{
    public partial class FFTVM
    {
        public async Task WriteFFT()
        {
            StringBuilder sb;
            byte[] row;
            double time;
            double freq;

            FFTStatus = "Saving FFTs...";
            await Task.Delay(25);

            // Delete file if it already exists
            if (File.Exists(FFTFilePathName))
            {
                File.Delete(FFTFilePathName);
            }

            // Create a new file     
            using FileStream fs = File.Create(FFTFilePathName);

            // Write description row
            sb = new StringBuilder();
            sb.Clear();
            sb.Append("Bins down the page, Amplitude over time across the page\n");
            
            row = new UTF8Encoding(true).GetBytes(sb.ToString());
            fs.Write(row, 0, row.Length);

            // Write header row
            sb.Clear();
            sb.Append("Hz \\ sec\t");
            
            for (int j = 0; j < nA; j++)
            {
                time = Math.Round(((double)N / Repo.SampleFrequency) * j, 3);
                sb.Append(time);
                sb.Append('\t');
            }
            
            sb.Append('\n');
            row = new UTF8Encoding(true).GetBytes(sb.ToString());
            fs.Write(row, 0, row.Length);

            // Write data rows
            for (int i = 0; i < N / 2; i++) // Only interested in bottom half of frequency buckets
            {
                sb.Clear();

                freq = Math.Round(((double)Repo.SampleFrequency / N) * i, 1);
                sb.Append(freq);
                sb.Append('\t');

                for (int j = 0; j < nA; j++)
                {
                    sb.Append(results[i, j]);
                    sb.Append('\t');
                }

                sb.Append('\n');
                row = new UTF8Encoding(true).GetBytes(sb.ToString());
                fs.Write(row, 0, row.Length);
            }

            FFTStatus = "FFTs saved";
            await Task.Delay(25);
        }
    }
}
