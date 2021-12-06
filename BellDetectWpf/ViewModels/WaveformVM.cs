using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf.ViewModels
{
    public static class WaveformVM
    {
        // Events
        public static event EventHandler Wave1FreqChanged;
        public static event EventHandler Wave1AmpChanged;
        public static event EventHandler Wave2FreqChanged;
        public static event EventHandler Wave2AmpChanged;

        public static int NumWaves { get; set; } // The number of waves underlying the waveform

        public static double ScaleFactor { get; set; } // For scaling wave amplitude

        public static uint SampleFrequency { get; set; } // Will eventually be set from UI

        public static uint TotalSampleTime { get; set; } // Length of waveform in seconds

        public static uint NumSamples { get; set; } // Total number of samples in waveform

        public static double[,] WaveSpec { get; set; } // Holds the specification of each underlying wave

        public static double[] Time { get; set; } // Array to hold elapsed time

        public static double[,] Waves { get; set; } // Array to hold underlying waves

        public static short[] Waveform { get; set; } // Array to hold resulting waveform

        static WaveformVM()
        {
            NumWaves = 2; // Will eventually be set from UI
            ScaleFactor = 1; // Will eventually be set from UI
            SampleFrequency = 44100; // Will eventually be set from UI
            TotalSampleTime = 2; // Will eventually be set from UI
            NumSamples = SampleFrequency * TotalSampleTime;

            WaveSpec = new double[NumWaves, 2]; // Will eventually be set from UI
            Time = new double[NumSamples];
            Waves = new double[NumWaves, NumSamples];
            Waveform = new short[NumSamples];
        }

        public static double Wave1Freq
        {
            get
            {
                return WaveSpec[0, 0];
            }
            set
            {
                if (WaveSpec[0, 0] != value)
                {
                    WaveSpec[0, 0] = value;
                    Wave1FreqChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static double Wave1Amp
        {
            get
            {
                return WaveSpec[0, 1];
            }
            set
            {
                if (WaveSpec[0, 1] != value)
                {
                    WaveSpec[0, 1] = value;
                    Wave1AmpChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static double Wave2Freq
        {
            get
            {
                return WaveSpec[1, 0];
            }
            set
            {
                if (WaveSpec[1, 0] != value)
                {
                    WaveSpec[1, 0] = value;
                    Wave2FreqChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static double Wave2Amp
        {
            get
            {
                return WaveSpec[1, 1];
            }
            set
            {
                if (WaveSpec[1, 1] != value)
                {
                    WaveSpec[1, 1] = value;
                    Wave2AmpChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }
    }
}
