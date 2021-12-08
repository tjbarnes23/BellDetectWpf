using BellDetectWpf.Models;
using BellDetectWpf.ViewModels.FFT.L2FFT;

namespace BellDetectWpf.ViewModels.FFT
{
    public static partial class C_FFT
    {
        public static void InitializeFFT()
        {
            uint n = (uint)(1 << (int)FFTVM.Log2N); // number of bins

            // Initialize arrays
            FFTVM.XRe = new double[n];
            FFTVM.XIm = new double[n];

            // Copy required items in the Waveform array into XRe array, and set XIm array values to zero
            for (int i = 0; i < n; i++)
            {
                FFTVM.XRe[i] = WaveformVM.Waveform[i + FFTVM.Offset];
                FFTVM.XIm[i] = 0;
            }

            // Initialize elements for linked list of complex numbers.
            FFTVM.X = new FFTElement[n];

            for (uint i = 0; i < n; i++)
            {
                FFTVM.X[i] = new FFTElement(); // Initialize array elements
            }

            // Set up 'next' pointers.
            for (uint i = 0; i < n - 1; i++)
            {
                FFTVM.X[i].Next = FFTVM.X[i + 1];
            }

            // Specify target for bit reversal re-ordering.
            for (uint i = 0; i < n; i++)
            {
                FFTVM.X[i].RevTarget = C_L2FFT.BitReverse(i, FFTVM.Log2N);
            }
        }
    }
}
