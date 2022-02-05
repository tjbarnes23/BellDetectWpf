using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.ViewModels.Shared;

namespace BellDetectWpf.ViewModels.Elliptic
{
    public static partial class C_Elliptic
    {
        public static void ExecuteElliptic(int bell, double[,] num, double[,] denom, double gain)
        {
            double[,] y;
            double nu;
            double de;
            int o;

            y = new double[WaveformVM.NumSamples, num.GetLength(0)];

            // Loop through audio samples
            for (int i = 0; i < WaveformVM.NumSamples; i++)
            {
                // Loop through rows of coefficients
                for (int j = 0; j < num.GetLength(0); j++)
                {
                    nu = 0.0;
                    de = 0.0;

                    // Loop through columns of numerator coefficients
                    for (int k = 0; k < num.GetLength(1); k++)
                    {
                        o = i - k;

                        if (o >= 0)
                        { 
                            if (j == 0)
                            {
                                nu += num[j, k] * WaveformVM.WaveformArr[o];
                            }
                            else
                            {
                                nu += num[j, k] * y[o, j - 1];
                            }
                        }
                    }

                    // Loop through columns of denominator coefficients
                    for (int k = 1; k < num.GetLength(1); k++)
                    {
                        o = i - k;

                        if (o >= 0)
                        {
                            de += denom[j, k] * y[o, j];
                        }
                    }

                    y[i, j] = nu - de;
                }
            }

            // Convert to short (16-bit) and overwrite original values in .wav file
            for (int i = 0; i < WaveformVM.NumSamples; i++)
            {
                EllipticVM.FilteredWaveformArr[i, bell] = (short)Math.Round(y[i, num.GetLength(0) - 1] * gain);
            }
        }
    }
}
