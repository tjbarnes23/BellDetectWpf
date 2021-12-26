using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace BellDetectWpf.ViewModels.DFT
{
    public static partial class C_DFT
    {
        public static async Task SaveDFT()
        {
            if (string.IsNullOrEmpty(DFTVM.FilePathName))
            {
                SaveFileDialog saveDlg = new SaveFileDialog
                {
                    Filter = string.Empty,
                    InitialDirectory = @"C:\ProgramData\BellDetect"
                };

                if (saveDlg.ShowDialog() == true)
                {
                    DFTVM.FilePathName = saveDlg.FileName;
                    await WriteDFT();
                }
            }
            else
            {
                await WriteDFT();
            }
        }
    }
}
