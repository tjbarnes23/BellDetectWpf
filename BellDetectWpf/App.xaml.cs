using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BellDetectWpf.Repository;
using BellDetectWpf.Utilities;
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

            /**************************************************
            * Initialize working directories
            **************************************************/

            // Ensure there is a BellDetect directory in C:\ProgramData
            // and also a subdirectory for Settings
            if (Directory.Exists(@"C:\ProgramData\BellDetect") == false)
            {
                Directory.CreateDirectory(@"C:\ProgramData\BellDetect");
            }

            if (Directory.Exists(@"C:\ProgramData\BellDetect\Settings") == false)
            {
                Directory.CreateDirectory(@"C:\ProgramData\BellDetect\Settings");
            }


            /**************************************************
            * Load saved settings if available
            **************************************************/

            if (File.Exists(@"C:\ProgramData\BellDetect\Settings\settings.json") == true)
            {
                Utils.LoadSettings();
            }


            /**************************************************
            * Initialize app
            **************************************************/

            // Set up NLog
            Repo.Logger = LogManager.GetCurrentClassLogger();

            // Initialize StartStop button text
            Repo.StartStopTxt = "Start key presses";

            // Initialize WaveSpec list
            Repo.WavSpecs = new();

            // Set default gain for FIR filter
            Repo.Gain = 3.0;

            // Set default detection frequencies
            Repo.LeftLowLow = 475.0;
            Repo.LeftLowHigh = 500.0;
            Repo.LeftMid = 522.5;
            Repo.LeftHighLow = 550.0;
            Repo.LeftHighHigh = 575.0;
            Repo.RightLowLow = 475.0;
            Repo.RightLowHigh = 500.0;
            Repo.RightMid = 522.5;
            Repo.RightHighLow = 550.0;
            Repo.RightHighHigh = 575.0;

            // Set default amplitude cutoff for detection
            Repo.AmplitudeCutoff = 2000;

            // Set default amplitude lookback
            Repo.AmplitudeLookback = 3;


            /**************************************************
            * Load main window
            **************************************************/
            mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
