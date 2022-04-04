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
    public partial class CreateWavVM
    {
        public async Task SaveWav()
        {
            string initialDirectory;

            if (string.IsNullOrEmpty(Repo.WavInitialDirectory) == true)
            {
                initialDirectory = @"C:\ProgramData\BellDetect";
            }
            else
            {
                initialDirectory = Repo.WavInitialDirectory;
            }

            SaveFileDialog saveDlg = new SaveFileDialog
            {
                Filter = string.Empty,
                InitialDirectory = initialDirectory
            };

            if (saveDlg.ShowDialog() == true)
            {
                WavFilePathName = saveDlg.FileName;

                FileInfo fi = new FileInfo(WavFilePathName);
                DirectoryInfo di = fi.Directory;
                Repo.WavInitialDirectory = di.FullName;

                await WriteWav();
            }
        }
    }
}
