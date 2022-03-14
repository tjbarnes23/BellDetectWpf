using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Repository;

namespace BellDetectWpf.ViewModels
{
    public partial class EllipticVM
    {
        public static void ExecuteElliptic(int channel, double[,] num, double[,] denom, double gain)
        {
            double[,] y;
            double nu;
            double de;
            int o;

            y = new double[Repo.NumSamples, num.GetLength(0)];

            // Loop through audio samples
            for (int i = 0; i < Repo.NumSamples; i++)
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
                                // If there are multiple channels, only process the first one
                                nu += num[j, k] * Repo.WavDataInt[0, o];
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

            // Convert to short (16-bit) and write into output array
            for (int i = 0; i < Repo.NumSamples; i++)
            {
                Repo.EllipticFilteredWaveformArr[channel, i] = (short)Math.Round(y[i, num.GetLength(0) - 1] * gain);
            }
        }
    }
}
