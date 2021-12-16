using System;
using BellDetectWpf.ViewModels.Waveform;

namespace BellDetectWpf.ViewModels
{
    public static class WaveformVM
    {
        private static double wave1Freq;
        private static double wave1Amp;
        private static double wave2Freq;
        private static double wave2Amp;
        private static double wave3Freq;
        private static double wave3Amp;
        private static double wave4Freq;
        private static double wave4Amp;
        private static double wave5Freq;
        private static double wave5Amp;
        private static double wave6Freq;
        private static double wave6Amp;
        private static double wave7Freq;
        private static double wave7Amp;
        private static double wave8Freq;
        private static double wave8Amp;
        private static double wave9Freq;
        private static double wave9Amp;
        private static double wave10Freq;
        private static double wave10Amp;
        private static double wave11Freq;
        private static double wave11Amp;
        private static double wave12Freq;
        private static double wave12Amp;

        private static string filePathName;

        // Events
        public static event EventHandler Wave1FreqChanged;
        public static event EventHandler Wave1AmpChanged;
        public static event EventHandler Wave2FreqChanged;
        public static event EventHandler Wave2AmpChanged;
        public static event EventHandler Wave3FreqChanged;
        public static event EventHandler Wave3AmpChanged;
        public static event EventHandler Wave4FreqChanged;
        public static event EventHandler Wave4AmpChanged;
        public static event EventHandler Wave5FreqChanged;
        public static event EventHandler Wave5AmpChanged;
        public static event EventHandler Wave6FreqChanged;
        public static event EventHandler Wave6AmpChanged;
        public static event EventHandler Wave7FreqChanged;
        public static event EventHandler Wave7AmpChanged;
        public static event EventHandler Wave8FreqChanged;
        public static event EventHandler Wave8AmpChanged;
        public static event EventHandler Wave9FreqChanged;
        public static event EventHandler Wave9AmpChanged;
        public static event EventHandler Wave10FreqChanged;
        public static event EventHandler Wave10AmpChanged;
        public static event EventHandler Wave11FreqChanged;
        public static event EventHandler Wave11AmpChanged;
        public static event EventHandler Wave12FreqChanged;
        public static event EventHandler Wave12AmpChanged;

        public static event EventHandler FilePathNameChanged;

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

        public static double Wave3Freq
        {
            get
            {
                return wave3Freq;
            }
            set
            {
                if (wave3Freq != value)
                {
                    wave3Freq = value;
                    Wave3FreqChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static double Wave3Amp
        {
            get
            {
                return wave3Amp;
            }
            set
            {
                if (wave3Amp != value)
                {
                    wave3Amp = value;
                    Wave3AmpChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static double Wave4Freq
        {
            get
            {
                return wave4Freq;
            }
            set
            {
                if (wave4Freq != value)
                {
                    wave4Freq = value;
                    Wave4FreqChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static double Wave4Amp
        {
            get
            {
                return wave4Amp;
            }
            set
            {
                if (wave4Amp != value)
                {
                    wave4Amp = value;
                    Wave4AmpChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static double Wave5Freq
        {
            get
            {
                return wave5Freq;
            }
            set
            {
                if (wave5Freq != value)
                {
                    wave5Freq = value;
                    Wave5FreqChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static double Wave5Amp
        {
            get
            {
                return wave5Amp;
            }
            set
            {
                if (wave5Amp != value)
                {
                    wave5Amp = value;
                    Wave5AmpChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static double Wave6Freq
        {
            get
            {
                return wave6Freq;
            }
            set
            {
                if (wave6Freq != value)
                {
                    wave6Freq = value;
                    Wave6FreqChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static double Wave6Amp
        {
            get
            {
                return wave6Amp;
            }
            set
            {
                if (wave6Amp != value)
                {
                    wave6Amp = value;
                    Wave6AmpChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static double Wave7Freq
        {
            get
            {
                return wave7Freq;
            }
            set
            {
                if (wave7Freq != value)
                {
                    wave7Freq = value;
                    Wave7FreqChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static double Wave7Amp
        {
            get
            {
                return wave7Amp;
            }
            set
            {
                if (wave7Amp != value)
                {
                    wave7Amp = value;
                    Wave7AmpChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static double Wave8Freq
        {
            get
            {
                return wave8Freq;
            }
            set
            {
                if (wave8Freq != value)
                {
                    wave8Freq = value;
                    Wave8FreqChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static double Wave8Amp
        {
            get
            {
                return wave8Amp;
            }
            set
            {
                if (wave8Amp != value)
                {
                    wave8Amp = value;
                    Wave8AmpChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static double Wave9Freq
        {
            get
            {
                return wave9Freq;
            }
            set
            {
                if (wave9Freq != value)
                {
                    wave9Freq = value;
                    Wave9FreqChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static double Wave9Amp
        {
            get
            {
                return wave9Amp;
            }
            set
            {
                if (wave9Amp != value)
                {
                    wave9Amp = value;
                    Wave9AmpChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static double Wave10Freq
        {
            get
            {
                return wave10Freq;
            }
            set
            {
                if (wave10Freq != value)
                {
                    wave10Freq = value;
                    Wave10FreqChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static double Wave10Amp
        {
            get
            {
                return wave10Amp;
            }
            set
            {
                if (wave10Amp != value)
                {
                    wave10Amp = value;
                    Wave10AmpChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static double Wave11Freq
        {
            get
            {
                return wave11Freq;
            }
            set
            {
                if (wave11Freq != value)
                {
                    wave11Freq = value;
                    Wave11FreqChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static double Wave11Amp
        {
            get
            {
                return wave11Amp;
            }
            set
            {
                if (wave11Amp != value)
                {
                    wave11Amp = value;
                    Wave11AmpChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static double Wave12Freq
        {
            get
            {
                return wave12Freq;
            }
            set
            {
                if (wave12Freq != value)
                {
                    wave12Freq = value;
                    Wave12FreqChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static double Wave12Amp
        {
            get
            {
                return wave12Amp;
            }
            set
            {
                if (wave12Amp != value)
                {
                    wave12Amp = value;
                    Wave12AmpChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static string FilePathName
        {
            get
            {
                return filePathName;
            }

            set
            {
                if (filePathName != value)
                {
                    filePathName = value;
                    FilePathNameChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static void CreateWaveform()
        {
            C_Waveform.GenerateWaveform();
            C_Waveform.SaveWav();
        }
    }
}
