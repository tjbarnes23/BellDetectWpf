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
    public partial class DetectionVM
    {
        public async Task SaveDetection()
        {
            string initialDirectory;

            if (string.IsNullOrEmpty(Repo.DetectionInitialDirectory) == true)
            {
                initialDirectory = @"C:\ProgramData\BellDetect";
            }
            else
            {
                initialDirectory = Repo.DetectionInitialDirectory;
            }

            SaveFileDialog saveDlg = new SaveFileDialog
            {
                Filter = string.Empty,
                InitialDirectory = initialDirectory
            };

            if (saveDlg.ShowDialog() == true)
            {
                DetectionFilePathName = saveDlg.FileName;

                FileInfo fi = new FileInfo(DetectionFilePathName);
                DirectoryInfo di = fi.Directory;
                Repo.FIRInitialDirectory = di.FullName;

                await WriteDetection();
            }
        }
    }
}
