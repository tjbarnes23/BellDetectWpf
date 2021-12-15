using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf.ViewModels.DFT
{
    public static partial class C_DFT
    {
        public static void RunDFT()
        {
            // Operate on CreateWaveformVM.Waveform, a double array
            // Send outputs to CosDFT and SinDFT, which are double arrays
            double amplitude;

            DFTVM.CosDFT = new double[DFTVM.NumSamples / 2];
            DFTVM.SinDFT = new double[DFTVM.NumSamples / 2];
            DFTVM.Results = new double[DFTVM.NumSamples / 2];

            for (uint i = 0; i < DFTVM.NumSamples / 2; i++)
            {
                double cos = 0.0;
                double sin = 0.0;

                for (uint j = 0; j < DFTVM.NumSamples; j++)
                {
                    cos += CreateWaveformVM.Waveform[DFTVM.Offset + j] * 
                            Math.Cos(2 * Math.PI * i / DFTVM.NumSamples * j);
                    sin += CreateWaveformVM.Waveform[DFTVM.Offset + j] *
                            Math.Sin(2 * Math.PI * i / DFTVM.NumSamples * j);
                }

                DFTVM.CosDFT[i] = cos;
                DFTVM.SinDFT[i] = sin;
            }

            // Create the Results array
            // Double the amplitudes to adjust the 2-sided frequency plot, then divide by the number of samples
            for (uint i = 0; i < DFTVM.NumSamples / 2; i++)
            {
                amplitude = Math.Sqrt(Math.Pow(DFTVM.CosDFT[i], 2) + Math.Pow(DFTVM.SinDFT[i], 2));
                amplitude = (amplitude * 2) / DFTVM.NumSamples;
                DFTVM.Results[i] = Math.Round(amplitude, 0);
            }
        }

        public static void RunSpecificDFT()
        {
            // Operate on CreateWaveformVM.Waveform, a double array
            // Send outputs to CosDFT and SinDFT, which are double arrays
            uint j;
            double amplitude;

            DFTVM.CosDFT = new double[10000];
            DFTVM.SinDFT = new double[10000];
            DFTVM.Results = new double[10000];

            for (uint i = 0; i < 10000; i++)
            {
                double cos = 0.0;
                double sin = 0.0;

                for (uint jx = 0; jx < 20000; jx++)
                {
                    j = (uint)(6589 + (jx * 9.6));

                    cos += CreateWaveformVM.Waveform[j] *
                            Math.Cos((double)(2.0 * Math.PI * i / 20000.0 * jx));
                    sin += CreateWaveformVM.Waveform[j] *
                            Math.Sin((double)(2.0 * Math.PI * i / 20000.0 * jx));
                }

                DFTVM.CosDFT[i] = cos;
                DFTVM.SinDFT[i] = sin;
            }

            // Create the Results array
            // Double the amplitudes to adjust the 2-sided frequency plot, then divide by the number of samples
            for (uint i = 0; i < 10000; i++)
            {
                amplitude = Math.Sqrt(Math.Pow(DFTVM.CosDFT[i], 2) + Math.Pow(DFTVM.SinDFT[i], 2));
                amplitude = (amplitude * 2) / 20000;
                DFTVM.Results[i] = Math.Round(amplitude, 0);
            }
        }
    }
}
