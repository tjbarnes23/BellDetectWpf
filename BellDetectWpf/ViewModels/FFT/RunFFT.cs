using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.ViewModels.Shared;

namespace BellDetectWpf.ViewModels.FFT
{
    public static partial class C_FFT
    {
        public static async Task RunFFT()
        {
            double amplitude;
            Stopwatch sw;
            StringBuilder sb;
            TimeSpan currElapsed;
            TimeSpan prevElapsed;
            TimeSpan interval;

            SharedVM.StatusMsg = "Running FFTs...";
            SharedVM.StatusForeground = "black";

            FFTVM.Log2N = (uint)Math.Log2(FFTVM.N);

            FFTVM.N = (uint)(1 << (int)FFTVM.Log2N); // number of bins recalculated in case a non-power of 2 was entered
            FFTVM.NA = (uint)(WaveformVM.NumSamples / FFTVM.N); // NA is number of FFTs that will be run
            FFTVM.Results = new double[FFTVM.N / 2, FFTVM.NA];

            C_FFT.InitializeFFT();

            sw = new Stopwatch();
            sb = new StringBuilder();
            prevElapsed = new TimeSpan(0);

            sw.Start();

            for (int i = 0; i < FFTVM.NA; i++)
            {
                FFTVM.Offset = (uint)(FFTVM.N * i);

                // Copy required items in the Waveform array into XRe array, and set XIm array values to zero
                for (int j = 0; j < FFTVM.N; j++)
                {
                    FFTVM.XRe[j] = WaveformVM.WaveformArr[FFTVM.Offset + j];
                    FFTVM.XIm[j] = 0;
                }

                ExecuteFFT();

                // Add to Results array
                // Double the amplitudes to adjust the 2-sided frequency plot, then divide by the number of samples
                for (int j = 0; j < (FFTVM.N / 2); j++)
                {
                    amplitude = Math.Sqrt(Math.Pow(FFTVM.XRe[j], 2) + Math.Pow(FFTVM.XIm[j], 2));
                    amplitude = (amplitude * 2) / FFTVM.N;
                    FFTVM.Results[j, i] = Math.Round(amplitude, 0);
                }

                currElapsed = sw.Elapsed;
                interval = currElapsed - prevElapsed;

                sb.Append("FFT time: ");
                sb.Append(interval);
                sb.Append('\t');
                sb.Append("FFT cumulative time: ");
                sb.Append(currElapsed);
                sb.Append('\n');

                MainWinVM.Logger.Info(sb.ToString());
                prevElapsed = currElapsed;
            }

            sw.Stop();
            
            FFTVM.FilePathName = String.Empty;

            await C_Shared.Status("FFTs completed", "black", 3000);
        }
    }
}
