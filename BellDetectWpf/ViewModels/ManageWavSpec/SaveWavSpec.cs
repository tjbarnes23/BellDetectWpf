using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BellDetectWpf.Models;
using BellDetectWpf.Repository;
using Microsoft.Win32;

namespace BellDetectWpf.ViewModels
{
    public partial class ManageWavSpecVM
    {
        public async Task SaveWavSpec()
        {
            string initialDirectory;

            if (string.IsNullOrEmpty(Repo.WavSpecInitialDirectory) == true)
            {
                initialDirectory = @"C:\ProgramData\BellDetect";
            }
            else
            {
                initialDirectory = Repo.WavSpecInitialDirectory;
            }

            SaveFileDialog saveDlg = new SaveFileDialog
            {
                Filter = string.Empty,
                InitialDirectory = initialDirectory
            };

            if (saveDlg.ShowDialog() == true)
            {
                WavSpecFilePathName = saveDlg.FileName;

                FileInfo fi = new FileInfo(WavSpecFilePathName);
                DirectoryInfo di = fi.Directory;
                Repo.WavSpecInitialDirectory = di.FullName;

                await WriteWavSpec();
            }
        }
    }
}
