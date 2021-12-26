using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf.ViewModels.Shared
{
    public static partial class C_Shared
    {
        public static async Task Status(string msg, string foreground, int durationMs)
        {
            SharedVM.StatusMsg = msg;
            SharedVM.StatusForeground = foreground;

            await Task.Delay(durationMs);
            
            SharedVM.StatusMsg = string.Empty;
            SharedVM.StatusForeground = "black";
        }
    }
}
