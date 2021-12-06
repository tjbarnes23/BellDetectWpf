using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BellDetectWpf.ViewModels.MainWin
{
    public static class L2Exit
    {
        public static void Exit()
        {
            MainWinVM.Mw.Close();
            Application.Current.Shutdown();
        }
    }
}
