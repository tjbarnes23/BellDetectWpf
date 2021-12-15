using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.ViewModels.DFT;

namespace BellDetectWpf.ViewModels
{
    public static class DFTVM
    {
        public static double StartTime { get; set; }
        public static double FrequencyResolution { get; set; }
        public static uint NumSamples { get; set; }
        public static uint Offset { get; set; }

        public static double[] CosDFT { get; set; }
        public static double[] SinDFT { get; set; }

        public static double[] Results { get; set; }

        public static string FilePathName { get; set; }

        public static void RunDFT()
        {
            FrequencyResolution = 0.5;
            NumSamples = (uint)(WavFileVM.SampleFrequency / FrequencyResolution);

            C_DFT.RunSpecificDFT();
            C_DFT.SaveSpecificDFT();
        }
    }
}
