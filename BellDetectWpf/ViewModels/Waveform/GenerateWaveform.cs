using System;

namespace BellDetectWpf.ViewModels.WaveformSpec
{
    public static partial class C_WaveformSpec
    {
        public static void GenerateWaveform()
        {
            double x;
            double w;
            double scale;

            int freq;
            int amp;
            double peak;
            double decay;

            double wSum;

            WaveformVM.SampleFrequency = 96000;
            WaveformVM.SampleLengthSeconds = 5.0;
            WaveformVM.NumWaves = 20;

            WaveformVM.NumSamples = (uint)(WaveformVM.SampleFrequency * WaveformVM.SampleLengthSeconds);
            WaveformVM.Time = new double[WaveformVM.NumSamples];
            WaveformVM.Waves = new double[WaveformVM.NumWaves, WaveformVM.NumSamples];
            WaveformVM.Waveform = new short[WaveformVM.NumSamples];

            // Create time array
            for (int i = 0; i < WaveformVM.NumSamples; i++)
            {
                WaveformVM.Time[i] = (double)i / WaveformVM.SampleFrequency;
            }

            // Create waves (zero based)
            for (int i = 0; i < WaveformVM.NumSamples; i++)
            {
                wSum = 0.0;

                for (int j = 0; j < WaveformVM.NumWaves; j++)
                {
                    freq = WaveformSpecVM.WaveformSpecArr[j].Frequency;
                    amp = WaveformSpecVM.WaveformSpecArr[j].Amplitude;
                    peak = WaveformSpecVM.WaveformSpecArr[j].TimeToPeak;
                    decay = WaveformSpecVM.WaveformSpecArr[j].TimeToDecayTo50pc;

                    if (freq != 0)
                    {
                        x = WaveformVM.Time[i] * freq * 2.0 * Math.PI;
                        w = Math.Sin(x) * amp;
                        
                        // At this point, w is the max amplitude of the wave at all points.
                        // Now scale w by the bell envelope function
                        if (i == 0)
                        {
                            scale = 0;
                        }
                        else
                        {
                            if (WaveformVM.Time[i] <= peak)
                            {
                                scale = (peak / WaveformVM.Time[i]) * Math.Pow(Math.E,1 - (peak / WaveformVM.Time[i]));
                            }
                            else
                            {
                                scale = Math.Pow(0.5, (WaveformVM.Time[i] - peak) / decay);
                            }
                        }

                        w *= scale;

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
