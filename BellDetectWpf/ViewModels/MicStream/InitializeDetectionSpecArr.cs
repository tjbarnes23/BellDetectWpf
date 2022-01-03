using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Models;
using Microsoft.Win32;

namespace BellDetectWpf.ViewModels.MicStream
{
    public static partial class C_MicStream
    {
        public static void InitializeDetectionSpecArr()
        {
            MicStreamVM.DetectionSpecArr = new ObservableCollection<DetectionSpec>();

            for (int i = 0; i < 12; i++)
            {
                MicStreamVM.DetectionSpecArr.Add(new DetectionSpec
                {
                    Bell = ' ',
                    FrequencyBin = 0,
                    AmplitudeLow = 0,
                    AmplitudeHigh = 0,
                    MinTimeBetweenDetectionsMs = 0,
                    Key = ' '
                });
            }
        }
    }
}
