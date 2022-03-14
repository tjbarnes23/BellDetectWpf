using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace BellDetectWpf.ViewModels
{
    public partial class FIRVM
    {
        public async Task SaveFIR()
        {
            if (string.IsNullOrEmpty(FIRFilePathName))
            {
                SaveFileDialog saveDlg = new SaveFileDialog
                {
                    Filter = string.Empty,
                    InitialDirectory = @"C:\ProgramData\BellDetect"
                };

                if (saveDlg.ShowDialog() == true)
                {
                    FIRFilePathName = saveDlg.FileName;
                    await WriteFIROrigAndFiltered();
                }
            }
            else
            {
                await WriteFIROrigAndFiltered();
            }
        }
    }
}
