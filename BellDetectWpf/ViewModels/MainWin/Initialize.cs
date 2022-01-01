using System;
using System.IO;
using BellDetectWpf.Enums;
using BellDetectWpf.ViewModels.MicStream;
using BellDetectWpf.ViewModels.Transcribe;
using BellDetectWpf.ViewModels.WaveformSpec;
using BellDetectWpf.Views;
using NLog;

namespace BellDetectWpf.ViewModels.MainWin
{
    public static partial class C_MainWin
    {
        public static void Initialize()
        {
            // Set up NLog
            MainWinVM.Logger = LogManager.GetCurrentClassLogger();

            // Ensure there is a BellDetect folder in C:\ProgramData
            if (Directory.Exists(@"C:\ProgramData\BellDetect") == false)
            {
                Directory.CreateDirectory(@"C:\ProgramData\BellDetect");
            }

            // Populate zeros into waveform spec array
            C_WaveformSpec.InitializeWaveformSpecArr();

            // Populate zeros into detection spec array
            C_MicStream.InitializeDetectionSpecArr();

            // Initialize transcription array
            C_Transcribe.InitializeTranscriptionArr();

            // Set default file path names
            WaveformSpecVM.FilePathName = string.Empty;
            WaveformVM.FilePathName = string.Empty;
            FFTVM.FilePathName = string.Empty;
            DFTVM.FilePathName = string.Empty;
            MicStreamVM.FilePathName = string.Empty;
            TranscribeVM.FilePathName = string.Empty;

            // Set defaults for status
            SharedVM.StatusMsg = "Status:";
            SharedVM.StatusForeground = "black";

            // Default text for key press button
            KeyPressVM.StartStopTxt = "Start key presses";

            // Default text for mic stream button
            MicStreamVM.StartStopTxt = "Detect / generate key presses";

            // Default number of bells for transcription
            TranscribeVM.Stage = StageEnum.Eight;

            // Default text for transcribe button
            TranscribeVM.StartStopTxt = "Start transcribing";

            // Load main window
            MainWinVM.Mw = new MainWindow();
            MainWinVM.Mw.Show();
        }
    }
}
