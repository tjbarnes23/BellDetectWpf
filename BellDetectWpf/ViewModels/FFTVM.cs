using System;
using System.Diagnostics;
using System.Text;
using BellDetectWpf.Models;
using BellDetectWpf.ViewModels.FFT;

namespace BellDetectWpf.ViewModels
{
    public static class FFTVM
    {
        private static uint n;
        private static string filePathName;

        public static event EventHandler NChanged;
        public static event EventHandler FilePathNameChanged;

        public static double[] XRe { get; set; }
        public static double[] XIm { get; set; }
        public static FFTElement[] X { get; set; }
        public static uint Log2N { get; set; }
        public static uint NA { get; set; } // number of amplitude readings (= NumSamples / N)
        public static uint Offset { get; set; }
        public static double[,] Results { get; set; }

        public static uint N // number of bins
        {
            get
            {
                return n;
            }
            set
            {
                if (n != value)
                {
                    n = value;
                    NChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

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
    }
}
