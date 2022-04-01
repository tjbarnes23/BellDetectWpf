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
        * TJB Fs5: 1388-1442-1558-1619 Hz; 6th order; 44.1 kHz; -40/-3 dB
        * 4th
        *************************************************/
        static readonly double[,] tjb_Fs5_1_num = new double[7, 3]
        {
            { 0.2348686434752, 0, 0 },
            { 1, -1.945995899939, 1 },
            { 0.2348686434752, 0, 0 },
            { 1, -1.961790477833, 1 },
            { 0.008241779178991, 0, 0 },
            { 1, 0, -1 },
            { 1, 0, 0 }
        };

        static readonly double[,] tjb_Fs5_1_denom = new double[7, 3]
        {
            { 1, 0, 0 },
            { 1, -1.948998629065, 0.9977116023673 },
            { 1, 0, 0 },
            { 1, -1.955587972878, 0.9978662738906 },
            { 1, 0, 0 },
            { 1, -1.949361201099, 0.9946734719317 },
            { 1, 0, 0 }
        };
    }
}
