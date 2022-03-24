using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf.ViewModels
{
    public partial class DFTVM
    {
        public async Task WriteDFT()
        {
            StringBuilder sb;
            byte[] row;
            double freq;

            DFTStatus = "Saving DFT...";
            await Task.Delay(25);

            // Delete file if it already exists
            if (File.Exists(DFTFilePathName))
            {
                File.Delete(DFTFilePathName);
            }

            // Create a new file     
            using FileStream fs = File.Create(DFTFilePathName);

            // Write header row
            sb = new StringBuilder();
            sb.Append("Frequency (Hz)\t");
            sb.Append("Amplitude (0-32767)\n");

            row = new UTF8Encoding(true).GetBytes(sb.ToString());
            fs.Write(row, 0, row.Length);

            // Write data rows
            for (int i = 0; i < 10000; i++) // Frequency buckets 0 - 4,999.5 Hz at 0.5 Hz frequency resolution
            {
                sb.Clear();

                freq = Math.Round(0.5 * i, 1);
                sb.Append(freq);
                sb.Append('\t');

                sb.Append(results[i]);
                sb.Append('\t');
                sb.Append('\n');

                row = new UTF8Encoding(true).GetBytes(sb.ToString());
                fs.Write(row, 0, row.Length);
            }

            DFTStatus = "DFT saved";
            await Task.Delay(25);
        }
    }
}
