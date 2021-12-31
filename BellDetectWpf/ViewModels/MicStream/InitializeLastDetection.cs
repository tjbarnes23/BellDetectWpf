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
        public static void InitializeLastDetection()
        {
            MicStreamVM.LastDetectionArr = new TimeSpan[4];

            for (int i = 0; i < 4; i++)
            {
                MicStreamVM.LastDetectionArr[i] = new TimeSpan(0);
            }
        }
    }
}
