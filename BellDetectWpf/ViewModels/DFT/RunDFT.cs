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
            uint j;
            uint offset;
            double scalingFactor;
            double amplitude;

            DFTVM.CosDFT = new double[10000];
            DFTVM.SinDFT = new double[10000];
            DFTVM.Results = new double[10000];

            offset = (uint)(DFTVM.StartTime * WavFileVM.SampleFrequency);
            scalingFactor = WavFileVM.SampleFrequency / 10000.0;

            for (uint i = 0; i < 10000; i++)
            {
                double cos = 0.0;
                double sin = 0.0;

                for (uint jx = 0; jx < 20000; jx++)
                {
                    j = (uint)(offset + (jx * scalingFactor));

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
