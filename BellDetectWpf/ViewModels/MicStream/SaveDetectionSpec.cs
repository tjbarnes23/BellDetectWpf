using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace BellDetectWpf.ViewModels.MicStream
{
    public static partial class C_MicStream
    {
        public static async Task SaveDetectionSpec()
        {
            if (string.IsNullOrEmpty(MicStreamVM.FilePathName))
            {
                SaveFileDialog saveDlg = new SaveFileDialog
                {
                    Filter = string.Empty,
                    InitialDirectory = @"C:\ProgramData\BellDetect"
                };

                if (saveDlg.ShowDialog() == true)
                {
                    MicStreamVM.FilePathName = saveDlg.FileName;
                    await WriteDetectionSpec();
                }
            }
            else
            {
                await WriteDetectionSpec();
            }
        }
    }
}
