using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Models;
using Microsoft.Win32;

namespace BellDetectWpf.ViewModels.WaveformSpec
{
    public static partial class C_WaveformSpec
    {
        public static void InitializeWaveformSpecArr()
        {
            WaveformSpecVM.WaveformSpecArr = new ObservableCollection<WaveSpec>();

            for (int i = 0; i < 20; i++)
            {
                WaveformSpecVM.WaveformSpecArr.Add(new WaveSpec
                {
                    Frequency = 0,
                    Amplitude = 0,
                    TimeToPeak = 0.0,
                    TimeToDecayTo50pc = 0.0
                });
            }
        }
    }
}
