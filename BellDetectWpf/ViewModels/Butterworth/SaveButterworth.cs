using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Repository;
using Microsoft.Win32;

namespace BellDetectWpf.ViewModels
{
    public partial class ButterworthVM
    {
        public async Task SaveButterworth()
        {
            string initialDirectory;

            if (string.IsNullOrEmpty(Repo.ButterworthInitialDirectory) == true)
            {
                initialDirectory = @"C:\ProgramData\BellDetect";
            }
            else
            {
                initialDirectory = Repo.ButterworthInitialDirectory;
            }

            SaveFileDialog saveDlg = new SaveFileDialog
            {
                Filter = string.Empty,
                InitialDirectory = initialDirectory
            };

            if (saveDlg.ShowDialog() == true)
            {
                ButterworthFilePathName = saveDlg.FileName;

                FileInfo fi = new FileInfo(ButterworthFilePathName);
                DirectoryInfo di = fi.Directory;
                Repo.ButterworthInitialDirectory = di.FullName;

                await WriteButterworth();

                // Detect.WriteDetection(ButterworthFilePathName);
            }
        }
    }
}
