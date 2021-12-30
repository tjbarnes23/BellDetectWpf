﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.ViewModels.Shared;
using Microsoft.Win32;

namespace BellDetectWpf.ViewModels.WaveformSpec
{
    public static partial class C_WaveformSpec
    {
        public static async Task LoadWaveformSpec()
        {
            OpenFileDialog openDlg = new OpenFileDialog
            {
                Filter = string.Empty,
                InitialDirectory = @"C:\ProgramData\BellDetect"
            };

            if (openDlg.ShowDialog() == true)
            {
                await C_Shared.Status("Loading waveform spec...", "black", 10, false);

                WaveformSpecVM.FilePathName = openDlg.FileName;
                int i = 0;

                foreach (var line in File.ReadLines(WaveformSpecVM.FilePathName))
                {
                    var tempLine = line.Split('\t');
                    WaveformSpecVM.WaveformSpecArr[i].Frequency = Convert.ToInt32(tempLine[0]);
                    WaveformSpecVM.WaveformSpecArr[i].Amplitude = Convert.ToInt32(tempLine[1]);
                    WaveformSpecVM.WaveformSpecArr[i].TimeToPeak = Convert.ToDouble(tempLine[2]);
                    WaveformSpecVM.WaveformSpecArr[i].TimeToDecayTo50pc = Convert.ToDouble(tempLine[3]);

                    i++;
                }

                await C_Shared.Status("Waveform spec loaded", "black", 3000, true);
            }
        }
    }
}