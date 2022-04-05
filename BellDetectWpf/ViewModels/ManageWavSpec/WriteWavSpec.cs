using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BellDetectWpf.Models;
using BellDetectWpf.Repository;

namespace BellDetectWpf.ViewModels
{
    public partial class ManageWavSpecVM
    {
        public async Task WriteWavSpec()
        {
            ManageWavSpecStatus = "Saving wave spec...";
            await Task.Delay(25);

            // Delete file if it already exists
            if (File.Exists(WavSpecFilePathName))
            {
                File.Delete(WavSpecFilePathName);
            }

            SavedWavSpec sws = new()
            {
                SavedWavSpecs = Repo.WavSpecs
            };

            JsonSerializerOptions options = new()
            {
                WriteIndented = true
            };

            string jsonStr = JsonSerializer.Serialize(sws, options);

            File.WriteAllText(WavSpecFilePathName, jsonStr);

            ManageWavSpecStatus = "Wave spec saved";
            await Task.Delay(25);
        }
    }
}
