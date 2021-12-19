using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Models;
using Microsoft.Win32;

namespace BellDetectWpf.ViewModels
{
    public static class SpecifyWaveformVM
    {
        private static string filePathName;

        public static event EventHandler FilePathNameChanged;
        public static event EventHandler WaveformSpecChanged;

        public static string FilePathName
        {
            get
            {
                return filePathName;
            }
            set
            {
                if (filePathName != value)
                {
                    filePathName = value;
                    FilePathNameChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static List<WaveSpec> WaveformSpec { get; set; }

        public static void InitializeWaveformSpec()
        {
            WaveformSpec = new List<WaveSpec>();

            for (int i = 0; i < 12; i++)
            {
                WaveformSpec.Add(new WaveSpec
                {
                    Frequency = 0,
                    Amplitude = 0,
                    Mean = 0.0,
                    StdDev = 0.0,
                    Scale = 0.0
                });
            }
        }

        public static void Clear()
        {
            for (int i = 0; i < 12; i++)
            {
                WaveformSpec[i].Frequency = 0;
                WaveformSpec[i].Amplitude = 0;
                WaveformSpec[i].Mean = 0.0;
                WaveformSpec[i].StdDev = 0.0;
                WaveformSpec[i].Scale = 0.0;
            }
        }

        public static void LoadWaveformSpec()
        {
            OpenFileDialog openDlg = new OpenFileDialog
            {
                Filter = string.Empty,
                InitialDirectory = @"C:\ProgramData\BellDetect"
            };

            if (openDlg.ShowDialog() == true)
            {
                FilePathName = openDlg.FileName;
                int i = 0;

                foreach (var line in File.ReadLines(FilePathName))
                {
                    var tempLine = line.Split('\t');
                    WaveformSpec[i].Frequency = Convert.ToUInt32(tempLine[0]);
                    WaveformSpec[i].Amplitude = Convert.ToUInt32(tempLine[1]);
                    WaveformSpec[i].Mean = Convert.ToDouble(tempLine[2]);
                    WaveformSpec[i].StdDev = Convert.ToDouble(tempLine[3]);
                    WaveformSpec[i].Scale = Convert.ToDouble(tempLine[4]);

                    i++;
                }

                WaveformSpecChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        public static void SaveWaveformSpec()
        {
            StringBuilder sb = new StringBuilder();

            if (File.Exists(FilePathName))
            {
                File.Delete(FilePathName);
            }

            // Create a new file     
            using FileStream fs = File.Create(FilePathName);

            for (int i = 0; i < 12; i++)
            {
                sb.Clear();
                sb.Append(WaveformSpec[i].Frequency);
                sb.Append('\t');
                sb.Append(WaveformSpec[i].Amplitude);
                sb.Append('\t');
                sb.Append(WaveformSpec[i].Mean);
                sb.Append('\t');
                sb.Append(WaveformSpec[i].StdDev);
                sb.Append('\t');
                sb.Append(WaveformSpec[i].Scale);
                sb.Append('\n');

                Byte[] row = new UTF8Encoding(true).GetBytes(sb.ToString());
                fs.Write(row, 0, row.Length);
            }
        }
    }
}
