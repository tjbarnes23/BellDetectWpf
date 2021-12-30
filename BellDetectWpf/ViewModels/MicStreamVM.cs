using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using BellDetectWpf.ViewModels.MicStream;
using NAudio.Wave;

namespace BellDetectWpf.ViewModels
{
    public static class MicStreamVM
    {
        private static string startStopTxt;
        public static event EventHandler StartStopTxtChanged;

        internal static WaveInEvent waveIn;
        internal static WaveFormat waveFormat;

        public static Stopwatch SW { get; set; }

        public static TimeSpan TS3 { get; set; }
        public static TimeSpan TS4 { get; set; }

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

        public static void StartStop()
        {
            if (StartStopTxt == "Start streaming")
            {
                StartStopTxt = "Stop streaming";
                
                C_MicStream.StartMicStream();
            }
            else
            {
                C_MicStream.StopMicStream();
                StartStopTxt = "Start streaming";
            }
        }
    }
}
