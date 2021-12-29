using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf.ViewModels.Shared
{
    public static partial class C_Shared
    {
        public static async Task Status(string msg, string foreground, int durationMs, bool revertToDefault)
        {
            SharedVM.StatusMsg = "Status: " + msg;
            SharedVM.StatusForeground = foreground;

            await Task.Delay(durationMs);

            if (revertToDefault == true)
            {
                SharedVM.StatusMsg = "Status:";
                SharedVM.StatusForeground = "black";
            }
        }
    }
}
