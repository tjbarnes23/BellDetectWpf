using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf.Models
{
    public class WavFileData
    {
        public double[] Time { get; set; }

        public int[,] IntData { get; set; }

        public double[,] DoubleData { get; set; }
    }
}
