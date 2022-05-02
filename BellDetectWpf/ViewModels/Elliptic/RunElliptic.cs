using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Detection;
using BellDetectWpf.Repository;
using Microsoft.Win32;

namespace BellDetectWpf.ViewModels
{
    public partial class EllipticVM
    {
        public async Task RunElliptic()
        {
            List<List<double[,]>> coefs;
            int idx;
            double gain;

            EllipticStatus = "Applying elliptic filter...";
            await Task.Delay(25);

            Repo.EllipticNumChannels = 2; // This is the number of filter output channels that will be in the output .wav file
                                          // The output file also includes the first channel of the input .wav file

            Repo.EllipticFilteredWaveformArr = new short[Repo.EllipticNumChannels, Repo.NumSamples];

            // Load a list of filter coefficients
            coefs = new()
            {
                new List<double[,]>() { pm_B9_1_num, pm_B9_1_denom },
                new List<double[,]>() { pm_C15_1_num, pm_C15_1_denom },
                new List<double[,]>() { pm_C15_2_num, pm_C15_2_denom },
                new List<double[,]>() { tjb_B2_1_num, tjb_B2_1_denom },
                new List<double[,]>() { tjb_As3_1_num, tjb_As3_1_denom },
                new List<double[,]>() { tjb_Gs4_1_num, tjb_Gs4_1_denom },
                new List<double[,]>() { tjb_Fs5_1_num, tjb_Fs5_1_denom },
                new List<double[,]>() { tjb_E6_1_num, tjb_E6_1_denom },
                new List<double[,]>() { tjb_Ds7_1_num, tjb_Ds7_1_denom },
                new List<double[,]>() { tjb_Cs8_1_num, tjb_Cs8_1_denom },
                new List<double[,]>() { tjb_B9_1_num, tjb_B9_1_denom }
            };

            idx = (int)Repo.FilterElliptic;

            gain = 1.2;

            // This will apply the selected coefficients to the first channel of the input .wav file,
            // and store the output in index 0 of Repo.EllipticFilteredWaveformArr
            ExecuteElliptic(0, coefs[idx], gain);

            // This method looks for phase changes in the filtered signal and puts a phase change signal in index 1
            // of Repo.EllipticFilteredWaveformArr
            // Detect.DetectionV2(Repo.EllipticFilteredWaveformArr);

            EllipticStatus = "Elliptic filter applied";
            await Task.Delay(25);
        }
    }
}
