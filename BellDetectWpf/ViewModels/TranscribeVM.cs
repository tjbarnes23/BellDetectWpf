using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Enums;
using BellDetectWpf.Models;
using BellDetectWpf.ViewModels.MicStream;
using BellDetectWpf.ViewModels.Shared;
using BellDetectWpf.ViewModels.Transcribe;

namespace BellDetectWpf.ViewModels
{
    public static class TranscribeVM
    {
        private static StageEnum stage;
        private static string filePathName;
        private static string startStopTxt;

        public static event EventHandler StageChanged;
        public static event EventHandler FilePathNameChanged;
        public static event EventHandler StartStopTxtChanged;

        public static ObservableCollection<Row> TranscriptionArr { get; set; }
        public static int BlowCount { get; set; }
        public static int CurrRowNum { get; set; }
        public static int CurrPlace { get; set; }

        public static StageEnum Stage
        {
            get
            {
                return stage;
            }

            set
            {
                if (stage != value)
                {
                    stage = value;
                    StageChanged?.Invoke(null, EventArgs.Empty);
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
            if (StartStopTxt == "Start transcribing")
            {
                MicStreamVM.Output = OutputEnum.Transcription;
                await C_Shared.Status("Listening for bell strikes", "red", 10, false);
                StartStopTxt = "Stop transcribing";

                C_MicStream.StartAudioStream();
            }
            else
            {
                await C_Shared.Status(string.Empty, "black", 10, false);
                C_MicStream.StopAudioStream();
                StartStopTxt = "Start transcribing";
            }
        }
    }
}
