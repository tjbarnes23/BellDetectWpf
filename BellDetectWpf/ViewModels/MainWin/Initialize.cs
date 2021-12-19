using BellDetectWpf.Views;
using NLog;

namespace BellDetectWpf.ViewModels.MainWin
{
    public static partial class C_MainWin
    {
        public static void Initialize()
        {
            MainWinVM.Logger = LogManager.GetCurrentClassLogger();

            // Set default file path names for loading
            WavFileVM.FilePathName = string.Empty;

            // Set default file path names for saving
            WaveformVM.FilePathName = @"C:\ProgramData\BellDetect\waveform.wav";
            FFTVM.FilePathName = @"C:\ProgramData\BellDetect\resultsFFT.txt";
            DFTVM.FilePathName = @"C:\ProgramData\BellDetect\resultsDFT.txt";
            
            KeyPressVM.StartStopTxt = "Start detecting";

            MainWinVM.Mw = new MainWindow();
            MainWinVM.Mw.Show();
        }
    }
}
