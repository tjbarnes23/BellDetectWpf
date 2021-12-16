using System;

namespace BellDetectWpf.ViewModels.Waveform
{
    public static partial class C_Waveform
    {
        public static void GenerateWaveform()
        {
            double x;
            double w;
            
            double u = 0.2;
            double o = 1.0;
            double a = 0.3;
            double logNormDist;
            
            double wSum;

            WaveformVM.NumSamples = (uint)(WavFileVM.SampleFrequency * WavFileVM.SampleLengthSeconds);

            WaveformVM.NumWaves = 12;
            WaveformVM.WaveSpec = new double[WaveformVM.NumWaves, 2];

            WaveformVM.WaveSpec[0, 0] = WaveformVM.Wave1Freq;
            WaveformVM.WaveSpec[0, 1] = WaveformVM.Wave1Amp;
            WaveformVM.WaveSpec[1, 0] = WaveformVM.Wave2Freq;
            WaveformVM.WaveSpec[1, 1] = WaveformVM.Wave2Amp;
            WaveformVM.WaveSpec[2, 0] = WaveformVM.Wave3Freq;
            WaveformVM.WaveSpec[2, 1] = WaveformVM.Wave3Amp;
            WaveformVM.WaveSpec[3, 0] = WaveformVM.Wave4Freq;
            WaveformVM.WaveSpec[3, 1] = WaveformVM.Wave4Amp;
            WaveformVM.WaveSpec[4, 0] = WaveformVM.Wave5Freq;
            WaveformVM.WaveSpec[4, 1] = WaveformVM.Wave5Amp;
            WaveformVM.WaveSpec[5, 0] = WaveformVM.Wave6Freq;
            WaveformVM.WaveSpec[5, 1] = WaveformVM.Wave6Amp;
            WaveformVM.WaveSpec[6, 0] = WaveformVM.Wave7Freq;
            WaveformVM.WaveSpec[6, 1] = WaveformVM.Wave7Amp;
            WaveformVM.WaveSpec[7, 0] = WaveformVM.Wave8Freq;
            WaveformVM.WaveSpec[7, 1] = WaveformVM.Wave8Amp;
            WaveformVM.WaveSpec[8, 0] = WaveformVM.Wave9Freq;
            WaveformVM.WaveSpec[8, 1] = WaveformVM.Wave9Amp;
            WaveformVM.WaveSpec[9, 0] = WaveformVM.Wave10Freq;
            WaveformVM.WaveSpec[9, 1] = WaveformVM.Wave10Amp;
            WaveformVM.WaveSpec[10, 0] = WaveformVM.Wave11Freq;
            WaveformVM.WaveSpec[10, 1] = WaveformVM.Wave11Amp;
            WaveformVM.WaveSpec[11, 0] = WaveformVM.Wave12Freq;
            WaveformVM.WaveSpec[11, 1] = WaveformVM.Wave12Amp;

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
                    if (WaveformVM.WaveSpec[j, 0] != 0.0)
                    {
                        x = WaveformVM.Time[i] * WaveformVM.WaveSpec[j, 0] * 2.0 * Math.PI;
                        w = Math.Sin(x) * WaveformVM.WaveSpec[j, 1];

                        // Now scale w by the lognormal distribution curve (approximates a bell amplitude envelope)
                        if (i == 0)
                        {
                            logNormDist = 0;
                        }
                        else
                        {
                            logNormDist = (a / (o * Math.Sqrt(2.0 * Math.PI) * WaveformVM.Time[i])) *
                                    Math.Pow(Math.E, -1 * (Math.Pow(Math.Log(WaveformVM.Time[i]) - Math.Log(u), 2) /
                                    (2 * Math.Pow(o, 2))));
                        }

                        w *= logNormDist;

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
