using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf.ViewModels.Waveform
{
    public static partial class C_Waveform
    {
        public static void ClearWaveform()
        {
            // Hum
            WaveformVM.Wave1Freq = 0.0;
            WaveformVM.Wave1Amp = 0.0;

            // Prime
            WaveformVM.Wave2Freq = 0.0;
            WaveformVM.Wave2Amp = 0.0;

            // Tierce
            WaveformVM.Wave3Freq = 0.0;
            WaveformVM.Wave3Amp = 0.0;

            // Quint
            WaveformVM.Wave4Freq = 0.0;
            WaveformVM.Wave4Amp = 0.0;

            // Nominal
            WaveformVM.Wave5Freq = 0.0;
            WaveformVM.Wave5Amp = 0.0;

            // Superquint
            WaveformVM.Wave6Freq = 0.0;
            WaveformVM.Wave6Amp = 0.0;

            // Octave nominal
            WaveformVM.Wave7Freq = 0.0;
            WaveformVM.Wave7Amp = 0.0;

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

            WavFileVM.SampleFrequency = 0;
            WavFileVM.SampleLengthSeconds = 0.0;
        }
    }
}
