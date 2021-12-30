using System;
using System.Collections.Generic;
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
        // internal static List<byte> buffer;
        internal static WaveFileWriter writer;

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
