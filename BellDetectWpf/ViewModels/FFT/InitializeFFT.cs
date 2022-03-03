using BellDetectWpf.Models;

namespace BellDetectWpf.ViewModels
{
    public partial class FFTVM
    {
        public void InitializeFFT()
        {
            // Initialize arrays
            xRe = new double[N];
            xIm = new double[N];

            // Initialize elements for linked list of complex numbers.
            x = new FFTElement[N];

            for (uint i = 0; i < N; i++)
            {
                x[i] = new FFTElement(); // Initialize array elements
            }

            // Set up 'next' pointers.
            for (uint i = 0; i < N - 1; i++)
            {
                x[i].Next = x[i + 1];
            }

            // Specify target for bit reversal re-ordering.
            for (uint i = 0; i < N; i++)
            {
                x[i].RevTarget = BitReverse(i, log2N);
            }
        }
    }
}
