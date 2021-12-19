using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BellDetectWpf.ViewModels;
using BellDetectWpf.ViewModels.MainWin;

namespace BellDetectWpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Ensure there is a BellDetect folder in C:\ProgramData
            if (Directory.Exists(@"C:\ProgramData\BellDetect") == false)
            {
                Directory.CreateDirectory(@"C:\ProgramData\BellDetect");
            }

            C_MainWin.Initialize();
            SpecifyWaveformVM.InitializeWaveformSpec();
        }
    }
}
