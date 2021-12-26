using System;
using BellDetectWpf.ViewModels.Waveform;

namespace BellDetectWpf.ViewModels
{
    public static class WaveformVM
    {
        private static string filePathName;
        private static uint sampleFrequency;
        private static ushort sampleDepth;
        private static ushort numChannels;
        private static uint lengthBytes;
        private static double lengthSeconds;
        private static string wavFileFormatValid;

        public static event EventHandler FilePathNameChanged;
        public static event EventHandler SampleFrequencyChanged;
        public static event EventHandler SampleDepthChanged;
        public static event EventHandler NumChannelsChanged;
        public static event EventHandler LengthBytesChanged;
        public static event EventHandler LengthSecondsChanged;
        public static event EventHandler WavFileFormatValidChanged;

        public static uint NumSamples { get; set; } // Total number of samples in waveform

        public static int NumWaves { get; set; } // The number of waves underlying the waveform

        public static double[] Time { get; set; } // Array to hold elapsed time

        public static double[,] Waves { get; set; } // Array to hold underlying waves

        public static short[] WaveformArr { get; set; } // Array to hold resulting waveform

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

        public static uint SampleFrequency
        {
            get
            {
                return sampleFrequency;
            }
            set
            {
                if (sampleFrequency != value)
                {
                    sampleFrequency = value;
                    SampleFrequencyChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static ushort SampleDepth
        {
            get
            {
                return sampleDepth;
            }
            set
            {
                if (sampleDepth != value)
                {
                    sampleDepth = value;
                    SampleDepthChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static ushort NumChannels
        {
            get
            {
                return numChannels;
            }
            set
            {
                if (numChannels != value)
                {
                    numChannels = value;
                    NumChannelsChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static uint LengthBytes
        {
            get
            {
                return lengthBytes;
            }
            set
            {
                if (lengthBytes != value)
                {
                    lengthBytes = value;
                    LengthBytesChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static double LengthSeconds
        {
            get
            {
                return lengthSeconds;
            }
            set
            {
                if (lengthSeconds != value)
                {
                    lengthSeconds = value;
                    LengthSecondsChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static string WavFileFormatValid
        {
            get
            {
                return wavFileFormatValid;
            }
            set
            {
                if (wavFileFormatValid != value)
                {
                    wavFileFormatValid = value;
                    WavFileFormatValidChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }
    }
}
