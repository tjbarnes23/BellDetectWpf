using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.ViewModels.Shared;

namespace BellDetectWpf.ViewModels.WaveformSpec
{
    public static partial class C_WaveformSpec
    { 
        public static async Task WriteWaveformSpec()
        {
            StringBuilder sb;
            bool hasNonZeros;

            await C_Shared.Status("Saving waveform specification...", "black", 10, false);

            // Check grid contains at least one row of non-zeros
            hasNonZeros = false;

            for (int i = 0; i < 20; i++)
            {
                if (WaveformSpecVM.WaveformSpecArr[i].Frequency != 0 &&
                        WaveformSpecVM.WaveformSpecArr[i].Amplitude != 0 &&
                        WaveformSpecVM.WaveformSpecArr[i].TimeToPeak != 0.0 &&
                        WaveformSpecVM.WaveformSpecArr[i].TimeToDecayTo50pc != 0.0)
                {
                    hasNonZeros = true;
                    break;
                }
            }

            if (hasNonZeros == false)
            {
                await C_Shared.Status("Grid has all zeros. Waveform specification not saved", "red", 7000, true);
                return;
            }

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

            await C_Shared.Status("Waveform specification saved", "black", 3000, true);
        }
    }
}
