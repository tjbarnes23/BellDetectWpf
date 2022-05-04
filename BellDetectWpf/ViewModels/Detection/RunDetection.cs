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
            
            DetectionStatus = "Applying detection...";
            await Task.Delay(25);

            if (Repo.FIRNumChannels == 2)   // A mono wav file was loaded
            {
                Repo.DetectionNumChannels = 3;    // 3 channels - original, filtered and detection
            }
            else                            // A stereo (or higher) wav file was loaded. Input channels greater than 2 are ignored
            {
                Repo.DetectionNumChannels = 6;    // 6 channels - original, filtered and detection * 2
            }

            Repo.DetectionWaveformArr = new short[Repo.DetectionNumChannels, Repo.NumSamples];


            // Copy the original and filtered channels
            for (int i = 0; i < Repo.NumSamples; i++)
            {
                for (int j = 0; j < Repo.FIRNumChannels; j++)
                {
                    int k = j;

                    if (k > 1) k++;

                    Repo.DetectionWaveformArr[k, i] = Repo.FIRFilteredWaveformArr[j, i];
                }
            }

            // This method looks for strikes in the filtered signal and creates a signal if found
            // First param specifies the index of the filter array to use
            // Second param specifies the index of the filter array to store the detection signal in
            // Other params are frequencies for strike detection
            ExecuteDetection(1, 2,
                    Repo.LeftLowLow, Repo.LeftLowHigh, Repo.LeftMid, Repo.LeftHighLow, Repo.LeftHighHigh);

            if (Repo.DetectionNumChannels == 6)
            {
                // Process the right hand bell, if it exists
                ExecuteDetection(4, 5,
                        Repo.RightLowLow, Repo.RightLowHigh, Repo.RightMid, Repo.RightHighLow, Repo.RightHighHigh);
            }

            DetectionStatus = "Detection applied";
            await Task.Delay(25);
        }
    }
}
