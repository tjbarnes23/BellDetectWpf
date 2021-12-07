using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf.ViewModels.FFT
{
    public static class L2ExecuteFFT
    {
        public static void ExecuteFFT()
        {
            // Copy 1024 items in the Waveform array into XRe array, and set XIm array values to zero
            FFTVM.XRe = new double[1024];
            FFTVM.XIm = new double[1024];

            int offset = 13440; // At 96kHz, this runs the FFT starting 0.14 seconds into the .wav file

            for (int i = 0; i < 1024; i++)
            {
                FFTVM.XRe[i] = WaveformVM.Waveform[i + offset];
                FFTVM.XIm[i] = 0;
            }

            // Initalize FFT routine
            L2FFT fft = new L2FFT();
            fft.Init(10); // argument is LogN where N is 256 bins. 9 is for 512 bins; 10 is for 1024 bins

            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Start();

            fft.Run();

            sw.Stop();
            
            Debug.Print(sw.Elapsed.ToString());
        }
    }
}
