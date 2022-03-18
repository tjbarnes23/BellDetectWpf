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

            Repo.FIRNumChannels = 3; // This is the number of channels that will be in the output .wav file
            
            Repo.FIRFilteredWaveformArr = new short[Repo.FIRNumChannels - 1, Repo.NumSamples]; // FIRNumChannels - 1 because 1 channel in the
                                                                                               // output .wav file is for the original signal

            gain = 3.0;

            // This will apply the coefficients in FIRCoefficients.cs to the first channel of the input .wav file,
            // and store the output in index 0 of Repo.FIRFilteredWaveformArr
            ExecuteFIR(c, gain);

            // This method looks for phase changes in the filtered signal and puts a phase change signal in channel 3
            // of the output .wav file
            AddPhase();

            FIRStatus = "FIR filter applied";
            await Task.Delay(25);
        }
    }
}
