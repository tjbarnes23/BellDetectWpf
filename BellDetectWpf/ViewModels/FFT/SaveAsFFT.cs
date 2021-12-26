using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace BellDetectWpf.ViewModels.FFT
{
    public static partial class C_FFT
    {
        public static async Task SaveAsFFT()
        {
            SaveFileDialog saveDlg = new SaveFileDialog
            {
                Filter = string.Empty,
                InitialDirectory = @"C:\ProgramData\BellDetect"
            };

            if (saveDlg.ShowDialog() == true)
            {
                FFTVM.FilePathName = saveDlg.FileName;
                await WriteFFT();
            }
        }
    }
}
