using BellDetectWpf.Models;
using BellDetectWpf.ViewModels.FFT.L2FFT;

namespace BellDetectWpf.ViewModels.FFT
{
    public static partial class C_FFT
    {
        public static void InitializeFFT()
        {
            // Initialize arrays
            FFTVM.XRe = new double[FFTVM.N];
            FFTVM.XIm = new double[FFTVM.N];

            // Initialize elements for linked list of complex numbers.
            FFTVM.X = new FFTElement[FFTVM.N];

            for (uint i = 0; i < FFTVM.N; i++)
            {
                FFTVM.X[i] = new FFTElement(); // Initialize array elements
            }

            // Set up 'next' pointers.
            for (uint i = 0; i < FFTVM.N - 1; i++)
            {
                FFTVM.X[i].Next = FFTVM.X[i + 1];
            }

            // Specify target for bit reversal re-ordering.
            for (uint i = 0; i < FFTVM.N; i++)
            {
                FFTVM.X[i].RevTarget = C_L2FFT.BitReverse(i, FFTVM.Log2N);
            }
        }
    }
}
