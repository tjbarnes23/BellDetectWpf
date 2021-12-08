using BellDetectWpf.Views;
using NLog;

namespace BellDetectWpf.ViewModels.MainWin
{
    public static partial class C_MainWin
    {
        public static void Initialize()
        {
            MainWinVM.Logger = LogManager.GetCurrentClassLogger();

            MainWinVM.Mw = new MainWindow();
            KeyPressVM.StartStopTxt = "Start detecting";
            MainWinVM.Mw.Show();
        }
    }
}
