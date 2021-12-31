using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.ViewModels.Shared;
using Microsoft.Win32;

namespace BellDetectWpf.ViewModels.MicStream
{
    public static partial class C_MicStream
    {
        public static async Task LoadDetectionSpec()
        {
            OpenFileDialog openDlg = new OpenFileDialog
            {
                Filter = string.Empty,
                InitialDirectory = @"C:\ProgramData\BellDetect"
            };

            if (openDlg.ShowDialog() == true)
            {
                await C_Shared.Status("Loading bell detection spec...", "black", 10, false);

                MicStreamVM.FilePathName = openDlg.FileName;
                int i = 0;

                foreach (var line in File.ReadLines(MicStreamVM.FilePathName))
                {
                    var tempLine = line.Split('\t');
                    MicStreamVM.DetectionSpecArr[i].Frequency = Convert.ToInt32(tempLine[0]);
                    MicStreamVM.DetectionSpecArr[i].AmplitudeLow = Convert.ToInt32(tempLine[1]);
                    MicStreamVM.DetectionSpecArr[i].AmplitudeHigh = Convert.ToInt32(tempLine[2]);
                    MicStreamVM.DetectionSpecArr[i].MinTimeBetweenDetectionsMs = Convert.ToInt32(tempLine[3]);
                    MicStreamVM.DetectionSpecArr[i].MinTimeBetweenDetectionsMs = Convert.ToChar(tempLine[4]);

                    i++;
                }

                await C_Shared.Status("Bell detection spec loaded", "black", 3000, true);
            }
        }
    }
}
