using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using BellDetectWpf.Enums;
using BellDetectWpf.Models;
using BellDetectWpf.ViewModels.MicStream;
using BellDetectWpf.ViewModels.Shared;
using NAudio.Wave;

namespace BellDetectWpf.ViewModels
{
    public static class MicStreamVM
    {
        private static int sourceId;
        private static string filePathName;
        private static string startStopTxt;

        public static event EventHandler SourceIdChanged;
        public static event EventHandler FilePathNameChanged;
        public static event EventHandler StartStopTxtChanged;
        
        public static Dictionary<int, string> SourceDict { get; set; }
        public static ObservableCollection<DetectionSpec> DetectionSpecArr { get; set; }
        public static Stopwatch SW { get; set; }
        public static TimeSpan[] LastDetectionArr { get; set; }
        public static double[,] DetectionArr { get; set; }
        public static List<double[]> ResultArr { get; set; }
        public static OutputEnum Output { get; set; }
        public static int SampleFrequency { get; set; }
        public static int SampleDepth { get; set; }
        public static int NumChannels { get; set; }
        public static Queue<short> AudioBuffer { get; set; }

        public static int SourceId
        {
            get
            {
                return sourceId;
            }

            set
            {
                if (sourceId != value)
                {
                    sourceId = value;
                    SourceIdChanged?.Invoke(null, EventArgs.Empty);
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

        public static string StartStopTxt
        {
            get
            {
                return startStopTxt;
            }

            set
            {
                if (startStopTxt != value)
                {
                    startStopTxt = value;
                    StartStopTxtChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static async Task StartStop()
        {
            if (StartStopTxt == "Detect & generate key presses")
            {
                Output = OutputEnum.KeyPress;
                await C_Shared.Status("Listening for bell strikes", "red", 10, false);
                StartStopTxt = "Stop detecting";
                
                C_MicStream.StartAudioStream();
            }
            else
            {
                await C_Shared.Status(string.Empty, "black", 10, false);
                C_MicStream.StopAudioStream();
                StartStopTxt = "Detect & generate key presses";
            }
        }
    }
}
