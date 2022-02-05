using System;
using System.Diagnostics;
using System.Text;
using BellDetectWpf.Models;
using BellDetectWpf.ViewModels.FFT;

namespace BellDetectWpf.ViewModels
{
    public static class EllipticVM
    {
        private static string filePathName;

        public static event EventHandler FilePathNameChanged;

        public static double[] FilteredWaveformArrDbl { get; set; } // Double array to use while processing

        public static short[,] FilteredWaveformArr { get; set; } // Array to hold filtered waveforms for each bell

        public static ushort NumChannels { get; set; }

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
