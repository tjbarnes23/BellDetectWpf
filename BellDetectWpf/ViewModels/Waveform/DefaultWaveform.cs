using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf.ViewModels.Waveform
{
    public static partial class C_Waveform
    {
        public static void DefaultWaveform()
        {
            // Hum
            WaveformVM.Wave1Freq = 220.0;
            WaveformVM.Wave1Amp = 1500.0;

            // Prime
            WaveformVM.Wave2Freq = 440.0;
            WaveformVM.Wave2Amp = 2100.0;

            // Tierce
            WaveformVM.Wave3Freq = 528.0;
            WaveformVM.Wave3Amp = 4800.0;

            // Quint
            WaveformVM.Wave4Freq = 660.0;
            WaveformVM.Wave4Amp = 1050.0;

            // Nominal
            WaveformVM.Wave5Freq = 880.0;
            WaveformVM.Wave5Amp = 15000.0;

            // Superquint
            WaveformVM.Wave6Freq = 1320.0;
            WaveformVM.Wave6Amp = 4500.0;

            // Octave nominal
            WaveformVM.Wave7Freq = 1760.0;
            WaveformVM.Wave7Amp = 3000.0;

            WaveformVM.Wave8Freq = 0.0;
            WaveformVM.Wave8Amp = 0.0;

            WaveformVM.Wave9Freq = 0.0;
            WaveformVM.Wave9Amp = 0.0;

            WaveformVM.Wave10Freq = 0.0;
            WaveformVM.Wave10Amp = 0.0;

            WaveformVM.Wave11Freq = 0.0;
            WaveformVM.Wave11Amp = 0.0;

            WaveformVM.Wave12Freq = 0.0;
            WaveformVM.Wave12Amp = 0.0;

            WavFileVM.SampleFrequency = 96000;
            WavFileVM.SampleLengthSeconds = 5.0;
        }
    }
}
