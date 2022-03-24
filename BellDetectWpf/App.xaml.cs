using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BellDetectWpf.Repository;
using BellDetectWpf.ViewModels;
using BellDetectWpf.Views;
using NLog;

namespace BellDetectWpf
{
    public partial class App : Application
    {
        MainWindow mainWindow;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Set up NLog
            Repo.Logger = LogManager.GetCurrentClassLogger();

            // Ensure there is a BellDetect folder in C:\ProgramData
            if (Directory.Exists(@"C:\ProgramData\BellDetect") == false)
            {
                Directory.CreateDirectory(@"C:\ProgramData\BellDetect");
            }

            /**************************************************
            * Load main window
            **************************************************/
            mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
