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

            Repo.FIRNumChannels = 2; // This is the number of filter output channels that will be in the output .wav file
                                     // The output file also includes the first channel of the input .wav file
            
            Repo.FIRFilteredWaveformArr = new short[Repo.FIRNumChannels, Repo.NumSamples];

            // Load a list of filter coefficients
            List<double[]> coefs = new()
            {
                pm_b9_1,
                pm_c15_1,
                pm_c15_2
            };

            int idx = (int)Repo.FilterFIR;

            gain = 3.0;

            // This will apply the coefficients in FIRCoefficients.cs to the first channel of the input .wav file,
            // and store the output in index 0 of Repo.FIRFilteredWaveformArr
            ExecuteFIR(coefs[idx], gain);

            // This method looks for phase changes in the filtered signal and puts a phase change signal in index 1
            // of Repo.FIRFilteredWaveformArr
            AddPhase();

            FIRStatus = "FIR filter applied";
            await Task.Delay(25);
        }
    }
}
