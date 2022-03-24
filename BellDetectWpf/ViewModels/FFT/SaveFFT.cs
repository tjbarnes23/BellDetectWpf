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
    public partial class FFTVM
    {
        public async Task SaveFFT()
        {
            string initialDirectory;

            if (string.IsNullOrEmpty(Repo.FFTInitialDirectory) == true)
            {
                initialDirectory = @"C:\ProgramData\BellDetect";
            }
            else
            {
                initialDirectory = Repo.FFTInitialDirectory;
            }

            SaveFileDialog saveDlg = new SaveFileDialog
            {
                Filter = string.Empty,
                InitialDirectory = initialDirectory
            };

            if (saveDlg.ShowDialog() == true)
            {
                FFTFilePathName = saveDlg.FileName;

                FileInfo fi = new FileInfo(FFTFilePathName);
                DirectoryInfo di = fi.Directory;
                Repo.FFTInitialDirectory = di.FullName;

                await WriteFFT();
            }
        }
    }
}
