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
    public partial class EllipticVM
    {
        public async Task SaveElliptic()
        {
            string initialDirectory;

            if (string.IsNullOrEmpty(Repo.EllipticInitialDirectory) == true)
            {
                initialDirectory = @"C:\ProgramData\BellDetect";
            }
            else
            {
                initialDirectory = Repo.EllipticInitialDirectory;
            }

            SaveFileDialog saveDlg = new SaveFileDialog
            {
                Filter = string.Empty,
                InitialDirectory = initialDirectory
            };

            if (saveDlg.ShowDialog() == true)
            {
                EllipticFilePathName = saveDlg.FileName;

                FileInfo fi = new FileInfo(EllipticFilePathName);
                DirectoryInfo di = fi.Directory;
                Repo.EllipticInitialDirectory = di.FullName;

                await WriteElliptic();

                // Detect.WriteDetection(EllipticFilePathName);
            }
        }
    }
}
