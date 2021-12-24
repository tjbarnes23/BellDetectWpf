using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf.ViewModels.WaveformSpec
{
    public static partial class C_WaveformSpec
    {
        public static async Task Message(string msg)
        {
            WaveformSpecVM.Message = msg;
            await Task.Delay(3000);
            WaveformSpecVM.Message = string.Empty;
        }
    }
}
