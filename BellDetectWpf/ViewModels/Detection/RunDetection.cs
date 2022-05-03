using System.Collections.Generic;
using System.Threading.Tasks;
using BellDetectWpf.Detection;
using BellDetectWpf.Repository;

namespace BellDetectWpf.ViewModels
{
    public partial class DetectionVM
    {
        public async Task RunDetection()
        {
            await Task.Delay(25);
            /*
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

            // Load a list of filter coefficients
            coefs = new()
            {
                pm_B9_1,
                pm_C15_1,
                pm_C15_2
            };

            // Process the first channel of the input wav file
            idx = (int)Repo.FilterFIRLeft;

            // This will apply the selected coefficients in FIRCoefficients.cs to the input .wav file
            // First param specifies the input channel to use
            // Second param specifies the index of the filter array to store the results in
            ExecuteFIR(0, 0, coefs[idx], Repo.Gain);

            // This method looks for strikes in the filtered signal and creates a signal if found
            // First param specifies the index of the filter array to use
            // Second param specifies the index of the filter array to store the detection signal in
            Detect.DetectionV2(Repo.FIRFilteredWaveformArr, 0, 1, Repo.AmplitudeCutoff,
                    Repo.LeftLowLow, Repo.LeftLowHigh, Repo.LeftMid, Repo.LeftHighLow, Repo.LeftHighHigh,
                    true);

            if (Repo.WavNumChannels > 1)
            {
                // Process the second channel of the input wav file, if it exists
                idx = (int)Repo.FilterFIRRight;

                // This will apply the selected coefficients in FIRCoefficients.cs to the input .wav file
                // First param specifies the input channel to use
                // Second param specifies the index of the filter array to store the results in
                ExecuteFIR(1, 2, coefs[idx], Repo.Gain);

                // This method looks for strikes in the filtered signal and creates a signal if found
                // First param specifies the index of the filter array to use
                // Second param specifies the index of the filter array to store the detection signal in
                Detect.DetectionV2(Repo.FIRFilteredWaveformArr, 2, 3, Repo.AmplitudeCutoff,
                    Repo.RightLowLow, Repo.RightLowHigh, Repo.RightMid, Repo.RightHighLow, Repo.RightHighHigh,
                    false);
            }

            FIRStatus = "FIR filter applied";
            await Task.Delay(25);
            */
        }
    }
}
