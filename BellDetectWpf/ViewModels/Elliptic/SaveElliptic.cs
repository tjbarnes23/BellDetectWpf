using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace BellDetectWpf.ViewModels.Elliptic
{
    public static partial class C_Elliptic
    {
        public static async Task SaveElliptic()
        {
            if (string.IsNullOrEmpty(EllipticVM.FilePathName))
            {
                SaveFileDialog saveDlg = new SaveFileDialog
                {
                    Filter = string.Empty,
                    InitialDirectory = @"C:\ProgramData\BellDetect"
                };

                if (saveDlg.ShowDialog() == true)
                {
                    EllipticVM.FilePathName = saveDlg.FileName;
                    await WriteElliptic();
                }
            }
            else
            {
                await WriteElliptic();
            }
        }
    }
}
