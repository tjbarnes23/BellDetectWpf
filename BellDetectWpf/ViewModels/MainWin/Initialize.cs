using BellDetectWpf.Views;
using NLog;

namespace BellDetectWpf.ViewModels.MainWin
{
    public static partial class C_MainWin
    {
        public static void Initialize()
        {
            MainWinVM.Logger = LogManager.GetCurrentClassLogger();

            WaveformVM.FilePathName = @"C:\temp\waveform.wav";
            WavFileVM.FilePathName = @"C:\Users\Tim\source\repos\tjbarnes23\BellDetectWpf\BellDetectWpf\BellSamples\Trinity-8.wav";
            FFTVM.FilePathName = @"C:\temp\resultsFFT.txt";
            DFTVM.FilePathName = @"C:\temp\resultsDFT.txt";
            KeyPressVM.StartStopTxt = "Start detecting";

            MainWinVM.Mw = new MainWindow();
            MainWinVM.Mw.Show();
        }
    }
}
