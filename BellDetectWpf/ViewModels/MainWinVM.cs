using System;
using BellDetectWpf.Views;
using BellDetectWpf.Views.ViewPages;
using NLog;

namespace BellDetectWpf.ViewModels
{
    public static class MainWinVM
    {
        // Logger properties
        public static Logger Logger { get; set; }

        // Window property
        public static MainWindow Mw { get; set; }

        // Page property
        public static TranscribePage Tp { get; set; }

        public static void FrameContentRendered(object sender, EventArgs e)
        {
            Uri source = Mw.MainFrame.Source;
            string fileName = System.IO.Path.GetFileName(source.OriginalString);

            if (fileName == "TranscribePage.xaml")
            {
                Tp = (TranscribePage)Mw.MainFrame.Content;
                
            }
            else
            {
                Tp = null;
            }
        }
    }
}
