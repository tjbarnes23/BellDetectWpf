using System.Collections.Generic;
using System.Threading.Tasks;
using BellDetectWpf.Detection;
using BellDetectWpf.Repository;

namespace BellDetectWpf.ViewModels
{
    public partial class FIRVM
    {
        public async Task RunFIR()
        {
            List<double[]> coefs;
            int idx;
            double gain;

            FIRStatus = "Applying FIR filter...";
            await Task.Delay(25);

            Repo.FIRNumChannels = 2; // This is the number of filter output channels that will be in the output .wav file
                                     // The output file also includes the first channel of the input .wav file
            
            Repo.FIRFilteredWaveformArr = new short[Repo.FIRNumChannels, Repo.NumSamples];

            // Load a list of filter coefficients
            coefs = new()
            {
                pm_B9_1,
                pm_C15_1,
                pm_C15_2
            };

            idx = (int)Repo.FilterFIR;

            gain = 3.0;

            // This will apply the selected coefficients in FIRCoefficients.cs to the first channel of the input .wav file,
            // and store the output in index 0 of Repo.FIRFilteredWaveformArr
            ExecuteFIR(0, coefs[idx], gain);

            // This method looks for phase changes in the filtered signal and puts a phase change signal in index 1
            // of Repo.FIRFilteredWaveformArr
            Detect.DetectionV2(Repo.FIRFilteredWaveformArr);

            FIRStatus = "FIR filter applied";
            await Task.Delay(25);
        }
    }
}
