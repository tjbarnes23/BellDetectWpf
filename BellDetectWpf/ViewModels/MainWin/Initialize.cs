using BellDetectWpf.Views;

namespace BellDetectWpf.ViewModels.MainWin
{
    public static partial class C_MainWin
    {
        public static void Initialize()
        {
            MainWinVM.Mw = new MainWindow();
            KeyPressVM.StartStopTxt = "Start detecting";
            MainWinVM.Mw.Show();
        }
    }
}
