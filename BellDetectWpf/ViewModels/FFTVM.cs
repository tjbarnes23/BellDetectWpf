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
        public static uint Offset { get; set; }

        public static void RunFFT()
        {
            Offset = 13440; // At 96 kHz sample rate, this runs the FFT starting 0.14 seconds into the .wav file
            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Start();
            
            C_FFT.InitializeFFT();
            C_FFT.RunFFT();
            C_FFT.SaveFFT();

            sw.Stop();

            Debug.Print(sw.Elapsed.ToString());
        }
    }
}
