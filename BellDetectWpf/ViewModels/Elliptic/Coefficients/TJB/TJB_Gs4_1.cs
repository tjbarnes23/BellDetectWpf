using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf.ViewModels
{
    public partial class EllipticVM
    {
        /*************************************************
        * TJB Gs4: 1558-1619-1749-1817 Hz; 6th order; 44.1 kHz; -40/-3 dB
        * 3rd
        *************************************************/
        static readonly double[,] tjb_Gs4_1_num = new double[7, 3]
        {
            { 0.2348459339308, 0, 0 },
            { 1, -1.932036476467, 1 },
            { 0.2348459339308, 0, 0 },
            { 1, -1.951867678716, 1 },
            { 0.009233562389253, 0, 0 },
            { 1, 0, -1 },
            { 1, 0, 0 }
        };

        static readonly double[,] tjb_Gs4_1_denom = new double[7, 3]
        {
            { 1, 0, 0 },
            { 1, -1.936120403453, 0.9974362961076 },
            { 1, 0, 0 },
            { 1, -1.944370858376, 0.99760858173 },
            { 1, 0, 0 },
            { 1, -1.936996036723, 0.9940324985457 },
            { 1, 0, 0 }
        };
    }
}
