using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Repository;

namespace BellDetectWpf.ViewModels
{
    public partial class DFTVM
    {
        public async Task RunDFT()
        {
            // Operate on channel 1 of Repo.WavDataInt, an int array
            // Send outputs to CosDFT and SinDFT, which are double arrays
            uint jx;
            double scalingFactor;
            double amplitude;

            Stopwatch sw;
            StringBuilder sb;
            TimeSpan currElapsed;

            DFTStatus = "Running DFT...";
            await Task.Delay(25);

            // We are calculating one DFT with buckets from 0 - 4,999.5 Hz and 0.5 Hz
            // frequency resolution, hence 10,000 array size
            cosDFT = new double[10000];
            sinDFT = new double[10000];
            results = new double[10000];

            scalingFactor = Repo.SampleFrequency / 10000.0;

            sw = new Stopwatch();
            sb = new StringBuilder();

            sw.Start();

            // Calculate energy levels for 10,000 buckets,
            // i.e. 0 - 4,999.5 Hz at 0.5 Hz frequency resolution
            for (uint i = 0; i < 10000; i++)
            {
                double cos = 0.0;
                double sin = 0.0;

                // Look at 20000 waveform amplitude points, scaled to a sample rate of 10 kHz,
                // i.e. look at 2 seconds of waveform data to give 0.5 Hz frequency resolution
                for (uint j = 0; j < 20000; j++)
                {
                    jx = (uint)(j * scalingFactor);

                    cos += Repo.WavDataInt[0, jx] *
                            Math.Cos((double)(2.0 * Math.PI * i / 20000.0 * j));
                    sin += Repo.WavDataInt[0, jx] *
                            Math.Sin((double)(2.0 * Math.PI * i / 20000.0 * j));
                }

                cosDFT[i] = cos;
                sinDFT[i] = sin;
            }

            // Create the Results array
            // Double the amplitudes to adjust the 2-sided frequency plot, then divide by the number of samples
            for (uint i = 0; i < 10000; i++)
            {
                amplitude = Math.Sqrt(Math.Pow(cosDFT[i], 2) + Math.Pow(sinDFT[i], 2));
                amplitude = (amplitude * 2) / 20000;
                results[i] = Math.Round(amplitude, 0);
            }

            currElapsed = sw.Elapsed;

            sb.Clear();
            sb.Append("DFT time: ");
            sb.Append(currElapsed);

            Repo.Logger.Info(sb.ToString());

            sw.Stop();

            DFTFilePathName = string.Empty;

            DFTStatus = "DFT completed";
            await Task.Delay(25);
        }
    }
}
