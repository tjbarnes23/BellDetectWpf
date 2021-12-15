using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf.ViewModels.DFT
{
    public static partial class C_DFT
    {
        public static void SaveDFT()
        {
            DFTVM.FilePathName = @"C:\temp\resultsDFT.txt";

            StringBuilder sb = new StringBuilder();
            double freq;

            if (File.Exists(DFTVM.FilePathName))
            {
                File.Delete(DFTVM.FilePathName);
            }

            // Create a new file     
            using FileStream fs = File.Create(DFTVM.FilePathName);

            Byte[] row = new UTF8Encoding(true).GetBytes("Bins down the page, Amplitude over time across the page\n");
            fs.Write(row, 0, row.Length);

            sb.Clear();

            sb.Append("Hz \\ sec\t");
            double startTime = Math.Round((double)DFTVM.Offset / WavFileVM.SampleFrequency, 3);
            double endTime = Math.Round((double)(DFTVM.Offset + DFTVM.NumSamples) / WavFileVM.SampleFrequency, 3);
            sb.Append(startTime);
            sb.Append('-');
            sb.Append(endTime);
            sb.Append("s\t");
            sb.Append('\n');

            row = new UTF8Encoding(true).GetBytes(sb.ToString());
            fs.Write(row, 0, row.Length);

            for (int i = 0; i < DFTVM.NumSamples / 2; i++) // Only interested in bottom half of frequency buckets
            {
                sb.Clear();

                freq = Math.Round(DFTVM.FrequencyResolution * i, 1);
                sb.Append(freq);
                sb.Append('\t');

                sb.Append(DFTVM.Results[i]);
                sb.Append('\t');
                sb.Append('\n');
                
                row = new UTF8Encoding(true).GetBytes(sb.ToString());
                fs.Write(row, 0, row.Length);
            }
        }

        public static void SaveSpecificDFT()
        {
            DFTVM.FilePathName = @"C:\temp\resultsDFT.txt";

            StringBuilder sb = new StringBuilder();
            double freq;

            if (File.Exists(DFTVM.FilePathName))
            {
                File.Delete(DFTVM.FilePathName);
            }

            // Create a new file     
            using FileStream fs = File.Create(DFTVM.FilePathName);

            Byte[] row = new UTF8Encoding(true).GetBytes("Bins down the page, Amplitude over time across the page\n");
            fs.Write(row, 0, row.Length);

            sb.Clear();

            sb.Append("Hz \\ sec\t");
            double startTime = 0.069;
            double endTime = 2.5;
            sb.Append(startTime);
            sb.Append('-');
            sb.Append(endTime);
            sb.Append("s\t");
            sb.Append('\n');

            row = new UTF8Encoding(true).GetBytes(sb.ToString());
            fs.Write(row, 0, row.Length);

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
        }
    }
}
