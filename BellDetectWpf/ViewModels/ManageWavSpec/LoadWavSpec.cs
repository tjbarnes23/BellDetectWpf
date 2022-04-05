using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using BellDetectWpf.Models;
using BellDetectWpf.Repository;
using Microsoft.Win32;

namespace BellDetectWpf.ViewModels
{
    public partial class ManageWavSpecVM
    {
        public async Task LoadWavSpec()
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

            OpenFileDialog openDlg = new OpenFileDialog
            {
                Filter = string.Empty,
                InitialDirectory = initialDirectory
            };

            if (openDlg.ShowDialog() == true)
            {
                WavSpecFilePathName = openDlg.FileName;

                FileInfo fi = new FileInfo(WavSpecFilePathName);
                DirectoryInfo di = fi.Directory;
                Repo.WavSpecInitialDirectory = di.FullName;

                ManageWavSpecStatus = "Loading wave spec...";
                await Task.Delay(25);

                SavedWavSpec sws;

                string jsonStr = File.ReadAllText(WavSpecFilePathName);

                sws = JsonSerializer.Deserialize<SavedWavSpec>(jsonStr);

                WavSpecs = sws.SavedWavSpecs;

                ManageWavSpecStatus = "Wave spec loaded";
                await Task.Delay(25);
            }
        }
    }
}
