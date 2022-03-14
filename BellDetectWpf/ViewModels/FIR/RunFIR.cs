using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Repository;
using Microsoft.Win32;

namespace BellDetectWpf.ViewModels
{
    public partial class FIRVM
    {
        public async Task RunFIR()
        {
            double gain;

            FIRStatus = "Applying FIR filter...";
            await Task.Delay(25);

            Repo.FIRNumChannels = 2;
            
            Repo.FIRFilteredWaveformArr = new short[1, Repo.NumSamples];

            gain = 2.0;

            // This will apply the coefficients in FIRCoefficients.cs to the first channel of the input .wav file,
            // and store the output in index 0 of Repo.FIRFilteredWaveformArr
            ExecuteFIR(c, gain);

            FIRStatus = "FIR filter applied";
            await Task.Delay(25);
        }
    }
}
