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
