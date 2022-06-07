using System.Collections.Generic;
using System.Threading.Tasks;
using BellDetectWpf.Enums;
using BellDetectWpf.Repository;

namespace BellDetectWpf.ViewModels
{
    public partial class FIRVM
    {
        public async Task RunFIR()
        {
            List<double[]> coefs;
            int idx;

            FIRStatus = "Applying FIR filter...";
            await Task.Delay(25);

            if (Repo.WavNumChannels == 1)   // A mono wav file was loaded
            {
                Repo.FIRNumChannels = 2;    // 2 filter output channels - one for the filtered waveform and the other for detection signals
            }
            else                            // A stereo (or higher) wav file was loaded. Input channels greater than 2 are ignored
            {
                Repo.FIRNumChannels = 4;    // 4 filter output channels - for each of the left and right input channels,
                                            // there is one output channel for the filtered waveform and another output channel for detection signals
            }

            Repo.FIRFilteredWaveformArr = new short[Repo.FIRNumChannels, Repo.NumSamples];

            // Copy WavFile into FIRFilteredWaveformArr
            for (int i = 0; i < Repo.NumSamples; i++)
            {
                for (int j = 0; j < Repo.FIRNumChannels; j += 2)
                {
                    Repo.FIRFilteredWaveformArr[j, i] = (short)Repo.WavDataInt[j / 2, i];
                }
            }

            // Load a list of filter coefficients
            coefs = new()
            {
                pm_C8_1,
                pm_C8_2,
                pm_C8_3,
                pm_B9_1,
                pm_B9_2,
                pm_B9_3,
                pm_D14_1,
                pm_D14_2,
                pm_D14_3,
                pm_C15_1,
                pm_C15_2,
                pm_C15_3
            };

            // Process the first channel of the input wav file
            idx = (int)Repo.FilterFIRLeft;

            // This will apply the selected coefficients in FIRCoefficients.cs to the input .wav file
            // First param specifies the input channel to use
            // Second param specifies the index of the filter array to store the results in
            ExecuteFIR(0, 1, coefs[idx]);

            if (Repo.WavNumChannels > 1)
            {
                // Process the second channel of the input wav file, if it exists
                idx = (int)Repo.FilterFIRRight;

                // This will apply the selected coefficients in FIRCoefficients.cs to the input .wav file
                // First param specifies the input channel to use
                // Second param specifies the index of the filter array to store the results in
                ExecuteFIR(1, 3, coefs[idx]);
            }

            // Set FilterType for use by detection page
            Repo.FilterType = FilterTypeEnum.FIR;

            FIRStatus = "FIR filter applied";
            await Task.Delay(25);
        }
    }
}
