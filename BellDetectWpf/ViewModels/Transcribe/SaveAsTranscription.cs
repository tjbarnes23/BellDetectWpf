using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace BellDetectWpf.ViewModels.Transcribe
{
    public static partial class C_Transcribe
    {
        public static async Task SaveAsTranscription()
        {
            SaveFileDialog saveDlg = new SaveFileDialog
            {
                Filter = string.Empty,
                InitialDirectory = @"C:\ProgramData\BellDetect"
            };

            if (saveDlg.ShowDialog() == true)
            {
                TranscribeVM.FilePathName = saveDlg.FileName;
                await WriteTranscription();
            }
        }
    }
}
