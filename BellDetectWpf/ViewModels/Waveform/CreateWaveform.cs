using System;
using System.Threading.Tasks;
using BellDetectWpf.ViewModels.Shared;

namespace BellDetectWpf.ViewModels.Waveform
{
    public static partial class C_Waveform
    {
        public static async Task CreateWaveform()
        {
            double x;
            double w;
            double scale;

            int freq;
            int amp;
            double peak;
            double decay;

            double wSum;

            WaveformVM.SampleFrequency = 44100;
            WaveformVM.SampleDepth = 16;
            WaveformVM.NumChannels = 1;
            WaveformVM.LengthSeconds = 5.0;
            WaveformVM.LengthBytes = Convert.ToUInt32(WaveformVM.SampleFrequency * (WaveformVM.SampleDepth / 8) *
                    WaveformVM.LengthSeconds);
            WaveformVM.WavFileFormatValid = "Yes";
            WaveformVM.FilePathName = string.Empty;
            
            WaveformVM.NumSamples = (uint)(WaveformVM.SampleFrequency * WaveformVM.LengthSeconds);
            WaveformVM.NumWaves = 20;
            WaveformVM.Time = new double[WaveformVM.NumSamples];
            WaveformVM.Waves = new double[WaveformVM.NumWaves, WaveformVM.NumSamples];
            WaveformVM.WaveformArr = new short[WaveformVM.NumSamples];

            SharedVM.StatusMsg = "Creating waveform...";
            SharedVM.StatusForeground = "black";

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

                WaveformVM.WaveformArr[i] = (short)Math.Round(wSum);
            }

            await C_Shared.Status("Waveform created", "black", 3000);
        }
    }
}
