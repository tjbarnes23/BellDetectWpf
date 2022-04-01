using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Repository;
using Microsoft.Win32;

namespace BellDetectWpf.ViewModels
{
    public partial class ButterworthVM
    {
        public async Task RunButterworth()
        {
            List<List<double[,]>> coefs;
            int idx;
            double gain;

            ButterworthStatus = "Applying Butterworth filter...";
            await Task.Delay(25);

            Repo.ButterworthNumChannels = 2; // This is the number of filter output channels that will be in the output .wav file
                                          // The output file also includes the first channel of the input .wav file

            Repo.ButterworthFilteredWaveformArr = new short[Repo.ButterworthNumChannels, Repo.NumSamples];

            // Load a list of filter coefficients
            coefs = new()
            {
                new List<double[,]>() { pm_C15_1_num, pm_C15_1_denom }
            };

            idx = (int)Repo.FilterButterworth;

            gain = 1.0;

            // This will apply the selected coefficients to the first channel of the input .wav file,
            // and store the output in index 0 of Repo.ButterworthFilteredWaveformArr
            ExecuteButterworth(0, coefs[idx], gain);

            // This method looks for phase changes in the filtered signal and puts a phase change signal in index 1
            // of Repo.ButterworthFilteredWaveformArr
            // AddPhase(1);

            ButterworthStatus = "Butterworth filter applied";
            await Task.Delay(25);
        }
    }
}
