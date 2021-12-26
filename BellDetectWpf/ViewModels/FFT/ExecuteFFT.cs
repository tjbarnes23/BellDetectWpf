using System;
using BellDetectWpf.Models;

namespace BellDetectWpf.ViewModels.FFT
{
    public static partial class C_FFT
    {
        // Perform FFT
        // xRe parameter is the real part of the input/output
        // xIm parameter is the imaginary part of the input/output
        // inverse parameter set to true to do an inverse FFT
        public static void ExecuteFFT()
        {
            bool inverse = false; // below can handle an inverse FFT - we don't need this
            uint numFlies = FFTVM.N >> 1; // Number of butterflies per sub-FFT
            uint span = FFTVM.N >> 1; // Width of the butterfly
            uint spacing = FFTVM.N; // Distance between start of sub-FFTs
            uint wIndexStep = 1; // Increment for twiddle table index

            // Copy data into linked complex number objects
            // If it's an IFFT, we divide by N while we're at it
            FFTElement x = FFTVM.X[0];
            uint k = 0;
            double scale = inverse ? 1.0 / FFTVM.N : 1.0;

            while (x != null)
            {
                x.Re = scale * FFTVM.XRe[k];
                x.Im = scale * FFTVM.XIm[k];
                x = x.Next;
                k++;
            }

            // For each stage of the FFT
            for (uint stage = 0; stage < FFTVM.Log2N; stage++)
            {
                // Compute a multiplier factor for the "twiddle factors".
                // The twiddle factors are complex unit vectors spaced at
                // regular angular intervals. The angle by which the twiddle
                // factor advances depends on the FFT stage. In many FFT
                // implementations the twiddle factors are cached, but because
                // array lookup is relatively slow in C#, it's just
                // as fast to compute them on the fly.
                double wAngleInc = wIndexStep * 2.0 * Math.PI / FFTVM.N;

                if (inverse == false)
                {
                    wAngleInc *= -1;
                }

                double wMulRe = Math.Cos(wAngleInc);
                double wMulIm = Math.Sin(wAngleInc);

                for (uint start = 0; start < FFTVM.N; start += spacing)
                {
                    FFTElement xTop = FFTVM.X[start];
                    FFTElement xBot = FFTVM.X[start + span];

                    double wRe = 1.0;
                    double wIm = 0.0;

                    // For each butterfly in this stage
                    for (uint flyCount = 0; flyCount < numFlies; ++flyCount)
                    {
                        // Get the top & bottom values
                        double xTopRe = xTop.Re;
                        double xTopIm = xTop.Im;
                        double xBotRe = xBot.Re;
                        double xBotIm = xBot.Im;

                        // Top branch of butterfly has addition
                        xTop.Re = xTopRe + xBotRe;
                        xTop.Im = xTopIm + xBotIm;

                        // Bottom branch of butterly has subtraction,
                        // followed by multiplication by twiddle factor
                        xBotRe = xTopRe - xBotRe;
                        xBotIm = xTopIm - xBotIm;
                        xBot.Re = xBotRe * wRe - xBotIm * wIm;
                        xBot.Im = xBotRe * wIm + xBotIm * wRe;

                        // Advance butterfly to next top & bottom positions
                        xTop = xTop.Next;
                        xBot = xBot.Next;

                        // Update the twiddle factor, via complex multiply
                        // by unit vector with the appropriate angle
                        // (wRe + j wIm) = (wRe + j wIm) x (wMulRe + j wMulIm)
                        double tRe = wRe;
                        wRe = wRe * wMulRe - wIm * wMulIm;
                        wIm = tRe * wMulIm + wIm * wMulRe;
                    }
                }

                numFlies >>= 1;     // Divide by 2 by right shift
                span >>= 1;
                spacing >>= 1;
                wIndexStep <<= 1;   // Multiply by 2 by left shift
            }

            // The algorithm leaves the result in a scrambled order.
            // Unscramble while copying values from the complex
            // linked list elements back to the input/output vectors.
            x = FFTVM.X[0];

            while (x != null)
            {
                uint target = x.RevTarget;
                FFTVM.XRe[target] = x.Re;
                FFTVM.XIm[target] = x.Im;
                x = x.Next;
            }
        }
    }
}
