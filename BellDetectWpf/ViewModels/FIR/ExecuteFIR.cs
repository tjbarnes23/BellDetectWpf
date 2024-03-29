﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Repository;

namespace BellDetectWpf.ViewModels
{
    public partial class FIRVM
    {
        public static void ExecuteFIR(int inputChannel, int outputChannel, double[] coefs)
        {
            int m;
            double t;

            m = coefs.Length; // e.g. a 592 order filter has coefficients for 0..592, so length = 593

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

                    // Use waveform data in the specified input channel of the source .wav file (zero-based)
                    t += coefs[j] * Repo.Gain * Repo.WavDataInt[inputChannel, i - j];
                }

                // Convert to short (16-bit) and write into specified index of output array
                Repo.FIRFilteredWaveformArr[outputChannel, i] = (short)Math.Round(t);
            }
        }
    }
}
