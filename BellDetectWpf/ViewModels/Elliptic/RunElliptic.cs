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
        public static async Task RunElliptic()
        {
            double[,] num = new double[9,3]
            {
                {
                    0.5728834589769, 0, 0
                },
                {
                    1, -1.962623639221, 1
                },
                {
                    0.5728834589769, 0, 0
                },
                {
                    1, -1.973531393315, 1
                },
                {
                    0.1743216967309, 0, 0
                },
                {
                    1, -1.965861570607, 1
                },
                {
                    0.1743216967309, 0, 0
                },
                {
                    1, -1.971015605825, 1
                },
                {
                    1, 0, 0
                }
            };
        
            double[,] denom = new double[9,3]
            {
                {
                    1, 0, 0
                },
                {
                    1, -1.965355698953, 0.9976393984693
                },
                {
                    1, 0, 0
                },
                {
                    1, -1.967122341459, 0.9977020125669
                },
                {
                    1, 0, 0 
                },
                {
                    1, -1.966125958653, 0.9993731884407
                },
                {
                    1, 0, 0
                },
                {
                    1, -1.969662838394, 0.9994068636127
                },
                {
                    1, 0, 0
                }
            };

            EllipticVM.FilteredWaveformArrDbl = new double[WaveformVM.NumSamples];

            double[,] y;
            double nu;
            double de;
            int o;

            await C_Shared.Status("Applying elliptic filter...", "black", 10, false);

            y = new double[WaveformVM.NumSamples, 9];

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
                WaveformVM.WaveformArr[i] = (short)Math.Round(y[i, 8] * 3);
            }

            EllipticVM.FilePathName = String.Empty;

            await C_Shared.Status("Elliptic filter applied", "black", 3000, true);
        }

        public static async Task RunEllipticFromTF()
        {
            double[] num = new double[]
            {
                0.009973221271135,
                -0.078519492294812,
                0.271712456001667,
                -0.539744680847009,
                0.673157001500768,
                -0.539744680847009,
                0.271712456001667,
                -0.078519492294812,
                0.009973221271135
            };

            double[] denom = new double[]
            {
                1,
                -7.868266837458246,
                27.210224819856858,
                -54.015175979700686,
                67.318192722601450,
                -53.935773202747710,
                27.130285127797570,
                -7.833618608471480,
                0.994132934395306
            };

            EllipticVM.FilteredWaveformArrDbl = new double[WaveformVM.NumSamples];

            short x;
            double y1;
            double y2;
            double yy;
            int k;

            await C_Shared.Status("Applying elliptic filter...", "black", 10, false);

            // Implement difference equation
            for (int i = 0; i < WaveformVM.NumSamples; i++)
            {
                y1 = 0.0;

                for (int j = 0; j < num.Length; j++)
                {
                    k = i - j;

                    if (k < 0)
                    {
                        x = 0;
                    }
                    else
                    {
                        x = WaveformVM.WaveformArr[k];
                    }

                    y1 += num[j] * x;
                }

                y2 = 0.0;

                for (int j = 1; j < denom.Length; j++)
                {
                    k = i - j;

                    if (k < 0)
                    {
                        yy = 0;
                    }
                    else
                    {
                        yy = EllipticVM.FilteredWaveformArrDbl[k];
                    }

                    y2 += denom[j] * yy;
                }

                EllipticVM.FilteredWaveformArrDbl[i] = y1 - y2;
            }

            // Convert to short (16-bit) and overwrite original values in .wav file
            for (int i = 0; i < WaveformVM.NumSamples; i++)
            {
                WaveformVM.WaveformArr[i] = (short)Math.Round(EllipticVM.FilteredWaveformArrDbl[i] * 3);
            }

            EllipticVM.FilePathName = String.Empty;

            await C_Shared.Status("Elliptic filter applied", "black", 3000, true);
        }
    }
}
