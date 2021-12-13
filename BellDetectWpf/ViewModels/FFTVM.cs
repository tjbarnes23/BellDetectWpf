using System;
using System.Diagnostics;
using BellDetectWpf.Models;
using BellDetectWpf.ViewModels.FFT;

namespace BellDetectWpf.ViewModels
{
    public static class FFTVM
    {
        public static double[] XRe { get; set; }
        public static double[] XIm { get; set; }
        public static FFTElement[] X { get; set; }
        public static uint Log2N { get; set; }
        public static uint N { get; set; } // number of bins
        public static uint NA { get; set; } // number of amplitude readings (= NumSamples / N)
        public static uint Offset { get; set; }
        public static double[,] Results {get; set;}

        public static void RunFFT()
        {
            double amplitude;

            Log2N = 13; // Use 8192 bins

            N = (uint)(1 << (int)Log2N); // number of bins
            NA = (uint)(CreateWaveformVM.NumSamples / N);
            Results = new double[N / 4, NA];
            
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

                for (int j = 0; j < (N / 4); j++) // Only interested in bottom 1/4 of frequency buckets
                {
                    amplitude = Math.Sqrt(Math.Pow(FFTVM.XRe[j], 2) + Math.Pow(FFTVM.XIm[j], 2));
                    amplitude = Math.Round(amplitude, 3);
                    Results[j, i] = amplitude;
                }

                MainWinVM.Logger.Info(sw.Elapsed.ToString());
            }

            sw.Stop();

            C_FFT.SaveFFT();
        }
    }
}
