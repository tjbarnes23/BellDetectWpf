using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf.Models
{
    public class WaveSpec
    {
        public uint Frequency { get; set; }

        public uint Amplitude { get; set; }

        public double Mean { get; set; }

        public double StdDev { get; set; }

        public double Scale { get; set; }
    }
}
