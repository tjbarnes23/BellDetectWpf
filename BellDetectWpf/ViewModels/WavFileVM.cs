using System;
using BellDetectWpf.ViewModels.LoadWav;

namespace BellDetectWpf.ViewModels
{
    public static class WavFileVM
    {
        private static string filePathName;
        private static uint sampleFrequency;
        private static ushort sampleDepth;
        private static ushort numChannels;
        private static uint sampleLengthBytes;
        private static double sampleLengthSeconds;
        private static string wavFileFormatValid;

        public static event EventHandler FilePathNameChanged;
        public static event EventHandler SampleFrequencyChanged;
        public static event EventHandler SampleDepthChanged;
        public static event EventHandler NumChannelsChanged;
        public static event EventHandler SampleLengthBytesChanged;
        public static event EventHandler SampleLengthSecondsChanged;
        public static event EventHandler WavFileFormatValidChanged;

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

        public static uint SampleLengthBytes
        {
            get
            {
                return sampleLengthBytes;
            }
            set
            {
                if (sampleLengthBytes != value)
                {
                    sampleLengthBytes = value;
                    SampleLengthBytesChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static double SampleLengthSeconds
        {
            get
            {
                return sampleLengthSeconds;
            }
            set
            {
                if (sampleLengthSeconds != value)
                {
                    sampleLengthSeconds = value;
                    SampleLengthSecondsChanged?.Invoke(null, EventArgs.Empty);
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

        public static void LoadWav()
        {
            C_LoadWav.LoadWav();
        }
    }
}
