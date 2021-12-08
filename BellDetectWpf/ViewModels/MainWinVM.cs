using BellDetectWpf.Views;
using NLog;

namespace BellDetectWpf.ViewModels
{
    public static class MainWinVM
    {
        // Logger properties
        public static Logger Logger { get; set; }

        // Window properties
        public static MainWindow Mw { get; set; }
    }
}
