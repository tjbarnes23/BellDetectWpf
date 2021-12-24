using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace BellDetectWpf.ViewModels.WaveformSpec
{
    public static partial class C_WaveformSpec
    {
        public static async Task SaveWaveformSpec()
        {
            if (string.IsNullOrEmpty(WaveformSpecVM.FilePathName))
            {
                SaveFileDialog saveDlg = new SaveFileDialog
                {
                    Filter = string.Empty,
                    InitialDirectory = @"C:\ProgramData\BellDetect"
                };

                if (saveDlg.ShowDialog() == true)
                {
                    WaveformSpecVM.FilePathName = saveDlg.FileName;
                    await WriteWaveformSpec();
                }
            }
            else
            {
                await WriteWaveformSpec();
            }
        }
    }
}
