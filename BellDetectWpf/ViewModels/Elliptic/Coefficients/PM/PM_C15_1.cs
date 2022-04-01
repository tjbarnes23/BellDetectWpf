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
        * PM C15: 495-515-530-550 Hz; 6th order; 48 kHz; -40/-3 dB
        *************************************************/
        static readonly double[,] pm_C15_1_num = new double[7, 3]
        {
            { 0.2350644232864, 0, 0 },
            { 1, -1.995013577472, 1 },
            { 0.2350644232864, 0, 0 },
            { 1, -1.995616800165, 1 },
            { 0.0009814366648576, 0, 0 },
            { 1, 0, -1 },
            { 1, 0, 0 }
        };

        static readonly double[,] pm_C15_1_denom = new double[7, 3]
        {
            { 1, 0, 0 },
            { 1, -1.994934115842, 0.9997335870796 },
            { 1, 0, 0 },
            { 1, -1.995187758938, 0.9997405163304 },
            { 1, 0, 0 },
            { 1, -1.994692092596, 0.9993657134183 },
            { 1, 0, 0 }
        };
    }
}
