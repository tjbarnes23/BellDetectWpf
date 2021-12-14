using System;
using System.Diagnostics;
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

        public static void RunFFT()
        {
            double amplitude;

            Log2N = (uint)Math.Log2(N);

            N = (uint)(1 << (int)Log2N); // number of bins recalculated in case a non-power of 2 was entered
            NA = (uint)(CreateWaveformVM.NumSamples / N);
            Results = new double[N / 2, NA];

            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Start();

            C_FFT.InitializeFFT();

            for (int i = 0; i < NA; i++)
            {
                Offset = (uint)(N * i);

                // Copy required items in the Waveform array into XRe array, and set XIm array values to zero
                for (int j = 0; j < N; j++)
                {
                    XRe[j] = CreateWaveformVM.Waveform[Offset + j];
                    XIm[j] = 0;
                }

                C_FFT.RunFFT();

                for (int j = 0; j < (N / 2); j++) // Only the bottom half of frequency buckets have valid data
                {
                    amplitude = Math.Sqrt(Math.Pow(FFTVM.XRe[j], 2) + Math.Pow(FFTVM.XIm[j], 2));
                    amplitude = Math.Round(amplitude, 0);
                    Results[j, i] = amplitude;
                }

                MainWinVM.Logger.Info(sw.Elapsed.ToString());
            }

            sw.Stop();

            C_FFT.SaveFFT();
        }
    }
}
