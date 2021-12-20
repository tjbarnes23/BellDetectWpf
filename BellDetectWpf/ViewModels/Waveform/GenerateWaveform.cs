using System;

namespace BellDetectWpf.ViewModels.Waveform
{
    public static partial class C_Waveform
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

            WavFileVM.SampleFrequency = 96000;
            WavFileVM.SampleLengthSeconds = 5.0;
            WaveformVM.NumWaves = 20;

            WaveformVM.NumSamples = (uint)(WavFileVM.SampleFrequency * WavFileVM.SampleLengthSeconds);
            WaveformVM.Time = new double[WaveformVM.NumSamples];
            WaveformVM.Waves = new double[WaveformVM.NumWaves, WaveformVM.NumSamples];
            WaveformVM.Waveform = new short[WaveformVM.NumSamples];

            // Create time array
            for (int i = 0; i < WaveformVM.NumSamples; i++)
            {
                WaveformVM.Time[i] = (double)i / WavFileVM.SampleFrequency;
            }

            // Create waves (zero based)
            for (int i = 0; i < WaveformVM.NumSamples; i++)
            {
                wSum = 0.0;

                for (int j = 0; j < WaveformVM.NumWaves; j++)
                {
                    freq = SpecifyWaveformVM.WaveformSpec[j].Frequency;
                    amp = SpecifyWaveformVM.WaveformSpec[j].Amplitude;
                    peak = SpecifyWaveformVM.WaveformSpec[j].TimeToPeak;
                    decay = SpecifyWaveformVM.WaveformSpec[j].TimeToDecayTo50pc;

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
