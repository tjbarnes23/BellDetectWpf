﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.ViewModels.Shared;

namespace BellDetectWpf.ViewModels.DFT
{
    public static partial class C_DFT
    {
        public static async Task WriteDFT()
        {
            StringBuilder sb;
            byte[] row;
            double freq;

            await C_Shared.Status("Saving DFT results...", "black", 10, false);

            // Delete file if it already exists
            if (File.Exists(DFTVM.FilePathName))
            {
                File.Delete(DFTVM.FilePathName);
            }

            // Create a new file     
            using FileStream fs = File.Create(DFTVM.FilePathName);

            double endTime = DFTVM.StartTime + 2.0;

            // Write description row
            sb = new StringBuilder();
            sb.Clear();
            sb.Append("Sample range used: ");
            sb.Append(DFTVM.StartTime);
            sb.Append('-');
            sb.Append(endTime);
            sb.Append("s\n");

            row = new UTF8Encoding(true).GetBytes(sb.ToString());
            fs.Write(row, 0, row.Length);

            // Write header row
            sb.Clear();
            sb.Append("Frequency (Hz)\t");
            sb.Append("Amplitude (0-32767)\n");

            row = new UTF8Encoding(true).GetBytes(sb.ToString());
            fs.Write(row, 0, row.Length);

            // Write data rows
            for (int i = 0; i < 10000; i++) // Only interested in bottom half of frequency buckets
            {
                sb.Clear();

                freq = Math.Round(0.5 * i, 1);
                sb.Append(freq);
                sb.Append('\t');

                sb.Append(DFTVM.Results[i]);
                sb.Append('\t');
                sb.Append('\n');

                row = new UTF8Encoding(true).GetBytes(sb.ToString());
                fs.Write(row, 0, row.Length);
            }

            await C_Shared.Status("DFT results saved", "black", 3000, true);
        }
    }
}
