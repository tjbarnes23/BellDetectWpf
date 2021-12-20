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

            for (int i = 0; i < 20; i++)
            {
                WaveformSpec.Add(new WaveSpec
                {
                    Frequency = 0,
                    Amplitude = 0,
                    TimeToPeak = 0.0,
                    TimeToDecayTo50pc = 0.0
                });
            }
        }

        public static void Clear()
        {
            for (int i = 0; i < 20; i++)
            {
                WaveformSpec[i].Frequency = 0;
                WaveformSpec[i].Amplitude = 0;
                WaveformSpec[i].TimeToPeak = 0.0;
                WaveformSpec[i].TimeToDecayTo50pc = 0.0;
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
                    WaveformSpec[i].Frequency = Convert.ToInt32(tempLine[0]);
                    WaveformSpec[i].Amplitude = Convert.ToInt32(tempLine[1]);
                    WaveformSpec[i].TimeToPeak = Convert.ToDouble(tempLine[2]);
                    WaveformSpec[i].TimeToDecayTo50pc = Convert.ToDouble(tempLine[3]);

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

            for (int i = 0; i < 20; i++)
            {
                sb.Clear();
                sb.Append(WaveformSpec[i].Frequency);
                sb.Append('\t');
                sb.Append(WaveformSpec[i].Amplitude);
                sb.Append('\t');
                sb.Append(WaveformSpec[i].TimeToPeak);
                sb.Append('\t');
                sb.Append(WaveformSpec[i].TimeToDecayTo50pc);
                sb.Append('\n');

                Byte[] row = new UTF8Encoding(true).GetBytes(sb.ToString());
                fs.Write(row, 0, row.Length);
            }
        }
    }
}
