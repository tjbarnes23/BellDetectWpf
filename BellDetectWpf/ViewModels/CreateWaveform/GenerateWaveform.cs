using System;

namespace BellDetectWpf.ViewModels.CreateWaveform
{
    public static partial class C_CreateWaveform
    {
        public static void GenerateWaveform()
        {
            double x;
            double w;
            double wSum;

            CreateWaveformVM.NumSamples = (uint)(WavFileVM.SampleFrequency * WavFileVM.SampleLengthSeconds);

            CreateWaveformVM.NumWaves = 2; // Will eventually be set from the UI
            CreateWaveformVM.WaveSpec = new double[CreateWaveformVM.NumWaves, 2]; // Will eventually be set from UI

            CreateWaveformVM.WaveSpec[0, 0] = CreateWaveformVM.Wave1Freq;
            CreateWaveformVM.WaveSpec[0, 1] = CreateWaveformVM.Wave1Amp;
            CreateWaveformVM.WaveSpec[1, 0] = CreateWaveformVM.Wave2Freq;
            CreateWaveformVM.WaveSpec[1, 1] = CreateWaveformVM.Wave2Amp;

            CreateWaveformVM.Time = new double[CreateWaveformVM.NumSamples];
            CreateWaveformVM.Waves = new double[CreateWaveformVM.NumWaves, CreateWaveformVM.NumSamples];
            CreateWaveformVM.Waveform = new short[CreateWaveformVM.NumSamples];

            // Create time array
            for (int i = 0; i < CreateWaveformVM.NumSamples; i++)
            {
                CreateWaveformVM.Time[i] = (double)i / WavFileVM.SampleFrequency;
            }

            // Create waves 1 (zero based)
            for (int i = 0; i < CreateWaveformVM.NumSamples; i++)
            {
                wSum = 0.0;

                for (int j = 0; j < CreateWaveformVM.NumWaves; j++)
                {
                    if (CreateWaveformVM.WaveSpec[j, 0] != 0.0)
                    {
                        x = CreateWaveformVM.Time[i] * CreateWaveformVM.WaveSpec[j, 0] * 2.0 * Math.PI;
                        w = Math.Sin(x) * CreateWaveformVM.WaveSpec[j, 1];
                        CreateWaveformVM.Waves[j, i] = w;
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

                CreateWaveformVM.Waveform[i] = (short)Math.Round(wSum);
            }
        }
    }
}
