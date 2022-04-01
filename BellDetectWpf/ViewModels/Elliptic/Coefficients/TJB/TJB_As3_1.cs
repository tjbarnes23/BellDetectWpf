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
        * TJB As3: 1749-1817-1924-1962 Hz; 8th order; 44.1 kHz; -40/-3 dB
        * 2nd
        *************************************************/
        static readonly double[,] tjb_As3_1_num = new double[9, 3]
        {
            { 0.5726391146315, 0, 0 },
            { 1, -1.916370642467, 1 },
            { 0.5726391146315, 0, 0 },
            { 1, -1.940519221976, 1 },
            { 0.1742988622681, 0, 0 },
            { 1, -1.923519686128, 1 },
            { 0.1742988622681, 0, 0 },
            { 1, -1.934932867763, 1 },
            { 1, 0, 0 }
        };

        static readonly double[,] tjb_As3_1_denom = new double[9, 3]
        {
            { 1, 0, 0 },
            { 1, -1.924159518991, 0.9964949291871 },
            { 1, 0, 0 },
            { 1, -1.928020872119, 0.9965857834282 },
            { 1, 0, 0 },
            { 1, -1.924571044544, 0.9990692111069 },
            { 1, 0, 0 },
            { 1, -1.93237608598, 0.9991181142418 },
            { 1, 0, 0 }
        };
    }
}
