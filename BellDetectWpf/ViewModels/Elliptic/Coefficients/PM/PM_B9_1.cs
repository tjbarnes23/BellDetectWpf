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
        * PM B9: 1096-1136-1178-1218 Hz; 4th order; 44.1 kHz; -30/-5 dB
        * Potential transient
        *************************************************/
        static readonly double[,] pm_B9_1_num = new double[5, 3]
        {
            { 0.1777184294709, 0, 0 },
            { 1, -1.969399446863, 1 },
            { 0.1777184294709, 0, 0 },
            { 1, -1.975996462499, 1 },
            { 1, 0, 0 }
        };

        static readonly double[,] pm_B9_1_denom = new double[5, 3]
        {
            { 1, 0, 0 },
            { 1, -1.970809866049, 0.9986547267676 },
            { 1, 0, 0 },
            { 1, -1.972344571504, 0.9986911613473 },
            { 1, 0, 0 }
        };
    }
}
