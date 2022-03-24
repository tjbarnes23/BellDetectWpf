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
    public partial class DFTVM
    {
        public async Task SaveDFT()
        {
            string initialDirectory;

            if (string.IsNullOrEmpty(Repo.DFTInitialDirectory) == true)
            {
                initialDirectory = @"C:\ProgramData\BellDetect";
            }
            else
            {
                initialDirectory = Repo.DFTInitialDirectory;
            }

            SaveFileDialog saveDlg = new SaveFileDialog
            {
                Filter = string.Empty,
                InitialDirectory = initialDirectory
            };

            if (saveDlg.ShowDialog() == true)
            {
                DFTFilePathName = saveDlg.FileName;

                FileInfo fi = new FileInfo(DFTFilePathName);
                DirectoryInfo di = fi.Directory;
                Repo.DFTInitialDirectory = di.FullName;

                await WriteDFT();
            }
        }
    }
}
