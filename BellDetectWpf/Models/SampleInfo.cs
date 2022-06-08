using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Enums;

namespace BellDetectWpf.Models
{
    public class SampleInfo
    {
        public int SampleNum { get; set; }

        public double Time { get; set; }

        public short Amplitude { get; set; }

        public bool Crossing { get; set; }

        public CrossingTypeEnum CrossingType { get; set; }

        public int NearestCrossingMidPrior { get; set; }

        public double ImpliedFrequency { get; set; }

        public bool ImpFreqInShiftRange { get; set; }

        public bool AmpCutoffMet { get; set; }

        public bool AmpIncreaseMet { get; set; }

        public bool StrikeDetected { get; set; }
    }
}
