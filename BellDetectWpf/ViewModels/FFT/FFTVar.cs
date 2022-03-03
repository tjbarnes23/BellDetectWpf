using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Models;

namespace BellDetectWpf.ViewModels
{
    public partial class FFTVM
    {
        double[] xRe;

        double[] xIm;

        FFTElement[] x;

        uint log2N;

        uint nA; // number of amplitude readings (= NumSamples / N)

        uint offset;

        double[,] results;
    }
}
