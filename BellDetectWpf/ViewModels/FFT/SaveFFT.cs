using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace BellDetectWpf.ViewModels
{
    public partial class FFTVM
    {
        public async Task SaveFFT()
        {
            if (string.IsNullOrEmpty(FFTFilePathName))
            {
                SaveFileDialog saveDlg = new SaveFileDialog
                {
                    Filter = string.Empty,
                    InitialDirectory = @"C:\ProgramData\BellDetect"
                };

                if (saveDlg.ShowDialog() == true)
                {
                    FFTFilePathName = saveDlg.FileName;
                    await WriteFFT();
                }
            }
            else
            {
                await WriteFFT();
            }
        }
    }
}
