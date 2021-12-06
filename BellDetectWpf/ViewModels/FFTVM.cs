using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.ViewModels.FFT;
using BellDetectWpf.ViewModels.WavFile;

namespace BellDetectWpf.ViewModels
{
    public static class FFTVM
    {
        internal static double[] XRe;
        internal static double[] XIm;

        public static void RunFFT()
        {
            L2ExecuteFFT.ExecuteFFT();
            L2SaveFFT.SaveFFT();
        }
    }
}
