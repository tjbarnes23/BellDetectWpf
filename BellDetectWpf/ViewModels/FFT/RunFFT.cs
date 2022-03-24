using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Repository;

namespace BellDetectWpf.ViewModels
{
    public partial class FFTVM
    {
        public async Task RunFFT()
        {
            double amplitude;
            Stopwatch sw;
            StringBuilder sb;
            TimeSpan currElapsed;
            TimeSpan prevElapsed;
            TimeSpan interval;

            FFTStatus = "Running FFTs...";
            await Task.Delay(25);

            log2N = (uint)Math.Log2(N);

            N = (uint)(1 << (int)log2N); // number of bins recalculated in case a non-power of 2 was entered
            nA = (uint)(Repo.NumSamples / N); // NA is number of FFTs that will be run
            results = new double[N / 2, nA];

            InitializeFFT();

            sw = new Stopwatch();
            sb = new StringBuilder();
            prevElapsed = new TimeSpan(0);

            sw.Start();

            for (int i = 0; i < nA; i++)
            {
                offset = (uint)(N * i);

                // Copy required items in the Waveform array into XRe array, and set XIm array values to zero
                for (int j = 0; j < N; j++)
                {
                    // If the wav file is multi-channel, only channel 0 will be processed
                    xRe[j] = Repo.WavDataInt[0, offset + j];
                    xIm[j] = 0;
                }

                ExecuteFFT();

                // Add to Results array
                // Double the amplitudes to adjust the 2-sided frequency plot, then divide by the number of samples
                for (int j = 0; j < (N / 2); j++)
                {
                    amplitude = Math.Sqrt(Math.Pow(xRe[j], 2) + Math.Pow(xIm[j], 2));
                    amplitude = (amplitude * 2) / N;
                    results[j, i] = Math.Round(amplitude, 0);
                }

                currElapsed = sw.Elapsed;
                interval = currElapsed - prevElapsed;

                sb.Clear();
                sb.Append("FFT time: ");
                sb.Append(interval);
                sb.Append('\t');
                sb.Append("FFT cumulative time: ");
                sb.Append(currElapsed);

                Repo.Logger.Info(sb.ToString());
                prevElapsed = currElapsed;
            }

            sw.Stop();
            
            FFTFilePathName = string.Empty;

            FFTStatus = "FFTs completed";
            await Task.Delay(25);
        }
    }
}
