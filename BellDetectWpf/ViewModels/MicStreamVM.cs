using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using BellDetectWpf.Models;
using BellDetectWpf.ViewModels.MicStream;
using BellDetectWpf.ViewModels.Shared;
using NAudio.Wave;

namespace BellDetectWpf.ViewModels
{
    public static class MicStreamVM
    {
        private static string startStopTxt;
        public static event EventHandler StartStopTxtChanged;

        internal static WaveInEvent waveIn;
        internal static WaveFormat waveFormat;

        public static ObservableCollection<DetectionSpec> DetectionSpecArr { get; set; }

        public static Stopwatch SW { get; set; }
        public static TimeSpan[] LastDetectionArr { get; set; }

        public static double[,] DetectionArr { get; set; }
        public static List<double[]> ResultArr { get; set; }

        public static string FilePathName { get; set; }

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
            if (StartStopTxt == "Start detection")
            {
                await C_Shared.Status("Listening for bell strikes", "red", 10, false);
                StartStopTxt = "Stop detection";
                
                C_MicStream.StartMicStream();
            }
            else
            {
                await C_Shared.Status(string.Empty, "black", 10, false);
                C_MicStream.StopMicStream();
                StartStopTxt = "Start detection";
            }
        }
    }
}
