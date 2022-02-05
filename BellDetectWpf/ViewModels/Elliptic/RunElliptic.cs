using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.ViewModels.Shared;

namespace BellDetectWpf.ViewModels.Elliptic
{
    public static partial class C_Elliptic
    {
        public static async Task RunElliptic()
        {
            await C_Shared.Status("Applying elliptic filter...", "black", 10, false);

            EllipticVM.FilteredWaveformArr = new short[WaveformVM.NumSamples, 8];
            EllipticVM.NumChannels = 8;
            double gain;

            gain = 0.55;

            ExecuteElliptic(0, num1, denom1, 1.9 * gain);
            ExecuteElliptic(1, num2, denom2, 3 * gain);
            ExecuteElliptic(2, num3, denom3, 3 * gain);
            ExecuteElliptic(3, num4, denom4, 2 * gain);
            ExecuteElliptic(4, num5, denom5, 3 * gain);
            ExecuteElliptic(5, num6, denom6, 2.5 * gain);
            ExecuteElliptic(6, num7, denom7, 3.5 * gain);
            ExecuteElliptic(7, num8, denom8, 3 * gain);

            await C_Shared.Status("Elliptic filter applied", "black", 3000, true);
        }
    }
}
