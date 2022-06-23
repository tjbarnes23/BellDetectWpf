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

        public int MaxAmplitudeFound { get; set; }

        public int MaxAmplitudeSampleNum { get; set; }

        public bool MinAmplitudeMet { get; set; }

        public int HalfCyclePeakPositiveValue { get; set; }

        public int HalfCyclePeakPositiveSampleNum { get; set; }

        public int HalfCyclePeakNegativeValue { get; set; }

        public int HalfCyclePeakNegativeSampleNum { get; set; }

        public int Denominator { get; set; }

        public int Numerator { get; set; }

        public int MaxAmplitudeIncreasePC { get; set; }

        public int MaxAmplitudeIncreaseSampleNum { get; set; }

        public bool MinAmplitudeIncreaseMet { get; set; }

        public bool StrikeDetected { get; set; }
    }
}
