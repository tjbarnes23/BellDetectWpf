using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf.ViewModels.FFT
{
    public static class L2ExecuteFFT
    {
        public static void ExecuteFFT()
        {
            // Copy the first 256 items in the Waveform array into XRe array, and set XIm array values to zero
            FFTVM.XRe = new double[256];
            FFTVM.XIm = new double[256];

            for (int i = 0; i < 256; i++)
            {
                FFTVM.XRe[i] = WaveformVM.Waveform[i];
                FFTVM.XIm[i] = 0;
            }

            // Initalize FFT routine
            L2FFT fft = new L2FFT();
            fft.Init(8); // argument is LogN where N is 256 bins
            fft.Run();
        }
    }
}
