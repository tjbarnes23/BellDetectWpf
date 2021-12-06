using BellDetectWpf.Views;

namespace BellDetectWpf.ViewModels.MainWin
{
    public static class L2Initialize
    {
        public static void Initialize()
        {
            MainWinVM.Mw = new MainWindow();
            KeyPressVM.StartStopTxt = "Start detecting";
            MainWinVM.Mw.Show();
        }
    }
}
