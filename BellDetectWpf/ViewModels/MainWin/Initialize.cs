using System.IO;
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
            C_WaveformSpec.InitializeWaveformSpec();

            // Set default file path names for loading
            WaveformVM.FilePathName = string.Empty;

            // Set default file path names for saving
            WaveformVM.FilePathName = @"C:\ProgramData\BellDetect\waveform.wav";
            FFTVM.FilePathName = @"C:\ProgramData\BellDetect\resultsFFT.txt";
            DFTVM.FilePathName = @"C:\ProgramData\BellDetect\resultsDFT.txt";
            
            // Default text for key press button
            KeyPressVM.StartStopTxt = "Start detecting";

            // Load main window
            MainWinVM.Mw = new MainWindow();
            MainWinVM.Mw.Show();
        }
    }
}
