using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace BellDetectWpf.ViewModels.Waveform
{
    public static partial class C_Waveform
    {
        public static async Task SaveWaveform()
        {
            if (string.IsNullOrEmpty(WaveformVM.FilePathName))
            {
                SaveFileDialog saveDlg = new SaveFileDialog
                {
                    Filter = string.Empty,
                    InitialDirectory = @"C:\ProgramData\BellDetect"
                };

                if (saveDlg.ShowDialog() == true)
                {
                    WaveformVM.FilePathName = saveDlg.FileName;
                    await WriteWaveform();
                }
            }
            else
            {
                await WriteWaveform();
            }
        }
    }
}
