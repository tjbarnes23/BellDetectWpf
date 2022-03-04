using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace BellDetectWpf.ViewModels
{
    public partial class EllipticVM
    {
        public async Task SaveAsElliptic()
        {
            SaveFileDialog saveDlg = new SaveFileDialog
            {
                Filter = string.Empty,
                InitialDirectory = @"C:\ProgramData\BellDetect"
            };

            if (saveDlg.ShowDialog() == true)
            {
                EllipticFilePathName = saveDlg.FileName;
                await WriteEllipticOrigAndFiltered();
            }
        }
    }
}
