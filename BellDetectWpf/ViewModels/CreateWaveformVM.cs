using System;
using BellDetectWpf.ViewModels.CreateWaveform;

namespace BellDetectWpf.ViewModels
{
    public static class CreateWaveformVM
    {
        private static double wave1Freq;
        private static double wave1Amp;
        private static double wave2Freq;
        private static double wave2Amp;

        // Events
        public static event EventHandler Wave1FreqChanged;
        public static event EventHandler Wave1AmpChanged;
        public static event EventHandler Wave2FreqChanged;
        public static event EventHandler Wave2AmpChanged;

        public static uint NumSamples { get; set; } // Total number of samples in waveform

        public static int NumWaves { get; set; } // The number of waves underlying the waveform

        public static double[,] WaveSpec { get; set; } // Holds the specifications of the underlying waves: freq, amplitude, envelope

        public static double[] Time { get; set; } // Array to hold elapsed time

        public static double[,] Waves { get; set; } // Array to hold underlying waves

        public static short[] Waveform { get; set; } // Array to hold resulting waveform
                
        public static double Wave1Freq
        {
            get
            {
                return wave1Freq;
            }
            set
            {
                if (wave1Freq != value)
                {
                    wave1Freq = value;
                    Wave1FreqChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static double Wave1Amp
        {
            get
            {
                return wave1Amp;
            }
            set
            {
                if (wave1Amp != value)
                {
                    wave1Amp = value;
                    Wave1AmpChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static double Wave2Freq
        {
            get
            {
                return wave2Freq;
            }
            set
            {
                if (wave2Freq != value)
                {
                    wave2Freq = value;
                    Wave2FreqChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static double Wave2Amp
        {
            get
            {
                return wave2Amp;
            }
            set
            {
                if (wave2Amp != value)
                {
                    wave2Amp = value;
                    Wave2AmpChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static void CreateWaveform()
        {
            C_CreateWaveform.GenerateWaveform();
            C_CreateWaveform.SaveWav();
        }
    }
}
