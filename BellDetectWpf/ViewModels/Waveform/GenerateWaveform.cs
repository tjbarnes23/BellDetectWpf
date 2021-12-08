using System;

namespace BellDetectWpf.ViewModels.Waveform
{
    public static class C_Waveform
    {
        public static void GenerateWaveform()
        {
            double x;
            double w;
            double wSum;

            // Create time array
            for (int i = 0; i < WaveformVM.NumSamples; i++)
            {
                WaveformVM.Time[i] = (double)i / WaveformVM.SampleFrequency;
            }

            // Create waves 1 (zero based)
            for (int i = 0; i < WaveformVM.NumSamples; i++)
            {
                wSum = 0.0;

                for (int j = 0; j < WaveformVM.NumWaves; j++)
                {
                    if (WaveformVM.WaveSpec[j, 0] != 0.0)
                    {
                        x = WaveformVM.Time[i] * WaveformVM.WaveSpec[j, 0] * 2.0 * Math.PI;
                        w = Math.Sin(x) * WaveformVM.WaveSpec[j, 1];
                        WaveformVM.Waves[j, i] = w;
                        wSum += w;
                    }
                }

                // Prevent any clipping
                if (wSum > 32767)
                {
                    wSum = 32767;
                }
                else if (wSum < -32767)
                {
                    wSum = -32767;
                }

                WaveformVM.Waveform[i] = (short)Math.Round(wSum);
            }
        }
    }
}
