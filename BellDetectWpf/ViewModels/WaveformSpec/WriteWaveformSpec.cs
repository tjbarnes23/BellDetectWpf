using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf.ViewModels.WaveformSpec
{
    public static partial class C_WaveformSpec
    { 
        public static async Task WriteWaveformSpec()
        {
            StringBuilder sb;

            WaveformSpecVM.Message = "Saving...";

            if (File.Exists(WaveformSpecVM.FilePathName))
            {
                File.Delete(WaveformSpecVM.FilePathName);
            }

            // Create a new file     
            using FileStream fs = File.Create(WaveformSpecVM.FilePathName);

            sb = new StringBuilder();

            for (int i = 0; i < 20; i++)
            {
                sb.Clear();
                sb.Append(WaveformSpecVM.WaveformSpecArr[i].Frequency);
                sb.Append('\t');
                sb.Append(WaveformSpecVM.WaveformSpecArr[i].Amplitude);
                sb.Append('\t');
                sb.Append(WaveformSpecVM.WaveformSpecArr[i].TimeToPeak);
                sb.Append('\t');
                sb.Append(WaveformSpecVM.WaveformSpecArr[i].TimeToDecayTo50pc);
                sb.Append('\n');

                Byte[] row = new UTF8Encoding(true).GetBytes(sb.ToString());
                fs.Write(row, 0, row.Length);
            }

            await Message("Saved");
        }
    }
}
