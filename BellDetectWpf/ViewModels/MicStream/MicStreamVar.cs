using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Enums;
using BellDetectWpf.Repository;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace BellDetectWpf.ViewModels
{
    public partial class MicStreamVM
    {
        private int SampleFrequency;
        private int SampleDepth;
        private int NumChannels;
        
        private WaveFormat waveFormat;
        private WaveInEvent waveIn;

        private Queue<short> AudioBuffer;

        // Constructor
        public MicStreamVM()
        {
            Repo.Recording = false;
            Repo.MicStreamStartStopTxt = "Start mic stream";
            Repo.MicStreamStatus = string.Empty;
        }
    }
}
