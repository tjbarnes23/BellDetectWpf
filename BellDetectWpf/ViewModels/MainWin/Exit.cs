using System.Windows;

namespace BellDetectWpf.ViewModels.MainWin
{
    public static partial class C_MainWin
    {
        public static void Exit()
        {
            MainWinVM.Mw.Close();
            Application.Current.Shutdown();
        }
    }
}
