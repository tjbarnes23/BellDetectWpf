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
        * TJB E6: 1283-1308-1388-1442 Hz; 8th order; 44.1kHz; -40/-3 dB
        * 5th
        *************************************************/
        static readonly double[,] tjb_E6_1_num = new double[9, 3]
        {
            { 0.572824832521, 0, 0 },
            { 1, -1.956139984246, 1 },
            { 0.572824832521, 0, 0 },
            { 1, -1.969231597554, 1 },
            { 0.1743163731043, 0, 0 },
            { 1, -1.960037760352, 1 },
            { 0.1743163731043, 0, 0 },
            { 1, -1.966222907262, 1 },
            { 1, 0, 0 }
        };

        static readonly double[,] tjb_E6_1_denom = new double[9, 3]
        {
            { 1, 0, 0 },
            { 1, -1.959646801085, 0.9973765462495 },
            { 1, 0, 0 },
            { 1, -1.961762640779, 0.9974479101591 },
            { 1, 0, 0 },
            { 1, -1.960413176043, 0.9993030829203 },
            { 1, 0, 0 },
            { 1, -1.964655320335, 0.9993414703729 },
            { 1, 0, 0 }
        };
    }
}
