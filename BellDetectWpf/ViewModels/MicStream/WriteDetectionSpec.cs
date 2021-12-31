using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.ViewModels.Shared;

namespace BellDetectWpf.ViewModels.MicStream
{
    public static partial class C_MicStream
    { 
        public static async Task WriteDetectionSpec()
        {
            StringBuilder sb;
            bool hasNonZeros;

            await C_Shared.Status("Saving bell detection specification...", "black", 10, false);

            // Check grid contains at least one row of non-zeros
            hasNonZeros = false;

            for (int i = 0; i < 4; i++)
            {
                if (MicStreamVM.DetectionSpecArr[i].Frequency != 0 &&
                        MicStreamVM.DetectionSpecArr[i].AmplitudeLow != 0 &&
                        MicStreamVM.DetectionSpecArr[i].AmplitudeHigh != 0 &&
                        MicStreamVM.DetectionSpecArr[i].MinTimeBetweenDetectionsMs != 0 &&
                        MicStreamVM.DetectionSpecArr[i].Key != ' ')
                {
                    hasNonZeros = true;
                    break;
                }
            }

            if (hasNonZeros == false)
            {
                await C_Shared.Status("Grid is empty. Bell detection specification not saved", "red", 7000, true);
                return;
            }

            if (File.Exists(MicStreamVM.FilePathName))
            {
                File.Delete(MicStreamVM.FilePathName);
            }

            // Create a new file     
            using FileStream fs = File.Create(MicStreamVM.FilePathName);

            sb = new StringBuilder();

            for (int i = 0; i < 4; i++)
            {
                sb.Clear();
                sb.Append(MicStreamVM.DetectionSpecArr[i].Frequency);
                sb.Append('\t');
                sb.Append(MicStreamVM.DetectionSpecArr[i].AmplitudeLow);
                sb.Append('\t');
                sb.Append(MicStreamVM.DetectionSpecArr[i].AmplitudeHigh);
                sb.Append('\t');
                sb.Append(MicStreamVM.DetectionSpecArr[i].MinTimeBetweenDetectionsMs);
                sb.Append('\t');
                sb.Append(MicStreamVM.DetectionSpecArr[i].Key);
                sb.Append('\n');

                Byte[] row = new UTF8Encoding(true).GetBytes(sb.ToString());
                fs.Write(row, 0, row.Length);
            }

            await C_Shared.Status("Bell detection specification saved", "black", 3000, true);
        }
    }
}
