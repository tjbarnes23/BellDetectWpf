using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Repository;
using Microsoft.Win32;

namespace BellDetectWpf.ViewModels
{
    public partial class EllipticVM
    {
        public async Task RunElliptic()
        {
            EllipticStatus = "Applying elliptic filter...";
            await Task.Delay(25);

            Repo.EllipticNumChannels = 1; // Set this to number of elliptic filtered channels to include in output .wav file
            Repo.EllipticFilteredWaveformArr = new short[Repo.EllipticNumChannels, Repo.NumSamples];

            // This will apply the coefficients in num12 and denom12 to the first channel of the input .wav file,
            // and store the output in the 0th first index of Repo.FilteredWaveformArr
            ExecuteElliptic(0, num12, denom12, 1);

            /*
            double gain;
            gain = 0.55;
            
            // 
            ExecuteElliptic(0, num1, denom1, 1.9 * gain);
            ExecuteElliptic(1, num2, denom2, 3 * gain);
            ExecuteElliptic(2, num3, denom3, 3 * gain);
            ExecuteElliptic(3, num4, denom4, 2 * gain);
            ExecuteElliptic(4, num5, denom5, 3 * gain);
            ExecuteElliptic(5, num6, denom6, 2.5 * gain);
            ExecuteElliptic(6, num7, denom7, 3.5 * gain);
            ExecuteElliptic(7, num8, denom8, 3 * gain);
            */

            EllipticStatus = "Elliptic filter applied";
            await Task.Delay(25);
        }
    }
}
