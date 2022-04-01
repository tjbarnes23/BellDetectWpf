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
        * TJB B9: 953-971-1032-1074 Hz; 8th order; 44.1 kHz; -40/-3 dB
        * Tenor
        *************************************************/
        static readonly double[,] tjb_B9_1_num = new double[9, 3]
        {
            { 0.5729667590975, 0, 0 },
            { 1, -1.975637277802, 1 },
            { 0.5729667590975, 0, 0 },
            { 1, -1.98307626305, 1 },
            { 0.1743291176597, 0, 0 },
            { 1, -1.977860576241, 1 },
            { 0.1743291176597, 0, 0 },
            { 1, -1.981374284943, 1 },
            { 1, 0, 0 }
        };

        static readonly double[,] tjb_B9_1_denom = new double[9, 3]
        {
            { 1, 0, 0 },
            { 1, -1.977121838417, 0.9979981231089 },
            { 1, 0, 0 },
            { 1, -1.978339935529, 0.9980543176236 },
            { 1, 0, 0 },
            { 1, -1.977937169348, 0.9994680832226 },
            { 1, 0, 0 },
            { 1, -1.980355683915, 0.9994982969825 },
            { 1, 0, 0 }
        };
    }
}
