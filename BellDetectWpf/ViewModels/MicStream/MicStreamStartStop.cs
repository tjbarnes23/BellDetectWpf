using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Repository;

namespace BellDetectWpf.ViewModels
{
    public partial class MicStreamVM
    {
        public async Task MicStreamStartStop()
        {
            if (Repo.Recording == false) // Not currently recording
            {
                Repo.Recording = true;

                MicStreamStartStopTxt = "Stop mic stream";

                MicStreamStatus = "Listening for bell strikes";
                await Task.Delay(25);

                MicStreamStart();
            }
            else // Currently recording
            {
                Repo.Recording = false;

                MicStreamStartStopTxt = "Start mic stream";

                MicStreamStatus = "Size of audio buffer: " + AudioBuffer.Count.ToString();
                await Task.Delay(25);

                waveIn.StopRecording();
                waveIn.Dispose();
            }
        }
    }
}
