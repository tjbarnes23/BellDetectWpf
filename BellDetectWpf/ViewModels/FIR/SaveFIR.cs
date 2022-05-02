using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Detection;
using BellDetectWpf.Repository;
using Microsoft.Win32;

namespace BellDetectWpf.ViewModels
{
    public partial class FIRVM
    {
        public async Task SaveFIR()
        {
            string initialDirectory;

            if (string.IsNullOrEmpty(Repo.FIRInitialDirectory) == true)
            {
                initialDirectory = @"C:\ProgramData\BellDetect";
            }
            else
            {
                initialDirectory = Repo.FIRInitialDirectory;
            }

            SaveFileDialog saveDlg = new SaveFileDialog
            {
                Filter = string.Empty,
                InitialDirectory = initialDirectory
            };

            if (saveDlg.ShowDialog() == true)
            {
                FIRFilePathName = saveDlg.FileName;

                FileInfo fi = new FileInfo(FIRFilePathName);
                DirectoryInfo di = fi.Directory;
                Repo.FIRInitialDirectory = di.FullName;

                await WriteFIR();
            }
        }
    }
}
