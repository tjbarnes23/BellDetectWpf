using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf.ViewModels.Waveform
{
    public static partial class C_Waveform
    {
        public static async Task Message(string msg)
        {
            WaveformVM.Message = msg;
            await Task.Delay(3000);
            WaveformVM.Message = string.Empty;
        }
    }
}
