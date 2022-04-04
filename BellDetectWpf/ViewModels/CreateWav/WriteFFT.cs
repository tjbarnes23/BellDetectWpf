using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Repository;

namespace BellDetectWpf.ViewModels
{
    public partial class CreateWavVM
    {
        public async Task WriteWav()
        {
            CreateWavStatus = "Saving .wav file...";
            await Task.Delay(25);

            // Delete file if it already exists
            if (File.Exists(WavFilePathName))
            {
                File.Delete(WavFilePathName);
            }

            // Write .wav file here

            CreateWavStatus = ".wav file saved";
            await Task.Delay(25);
        }
    }
}
