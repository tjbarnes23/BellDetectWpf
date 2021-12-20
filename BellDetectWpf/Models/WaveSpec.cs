using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf.Models
{
    public class WaveSpec
    {
        public int Frequency { get; set; }

        public int Amplitude { get; set; }

        public double TimeToPeak { get; set; }

        public double TimeToDecayTo50pc { get; set; }
    }
}
