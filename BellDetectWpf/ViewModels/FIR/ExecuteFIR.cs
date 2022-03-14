using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Repository;

namespace BellDetectWpf.ViewModels
{
    public partial class FIRVM
    {
        public static void ExecuteFIR(double[] coefs, double gain)
        {
            int m;
            double t;

            m = coefs.Length; // 593 = 0..592 = 592 order; or 276 = 0..275 = 275-order

            // y[n] = b[0].x[n] + b[1].x[n-1] + ... + b[592].x[n-592]

            // Loop through audio samples
            for (int i = 0; i < Repo.NumSamples; i++)
            {
                t = 0.0;
                
                for (int j = 0; j < m; j++)
                {
                    if (i - j < 0)
                    {
                        continue;
                    }

                    // Use waveform data in the 0th channel of the source .wav file
                    t += coefs[j] * gain * Repo.WavDataInt[0, i - j];
                }

                // Store the result in index 0 of the array - this will be put in channel 2 of the output .wav file
                Repo.FIRFilteredWaveformArr[0, i] = (short)Math.Round(t);
            }
        }
    }
}
