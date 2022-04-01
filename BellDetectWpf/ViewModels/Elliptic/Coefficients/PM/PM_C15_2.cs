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
        * PM C15: 480-515-530-565 Hz; 4th order; 48 kHz; -40/-3 dB
        *************************************************/
        static readonly double[,] pm_C15_2_num = new double[5, 3]
        {
            { 0.09997098946781, 0, 0 },
            { 1, -1.994268040969, 1 },
            { 0.09997098946781, 0, 0 },
            { 1, -1.99618707559, 1 },
            { 1, 0, 0 }
        };

        static readonly double[,] pm_C15_2_denom = new double[5, 3]
        {
            { 1, 0, 0 },
            { 1, -1.994584786167, 0.9993645636537 },
            { 1, 0, 0 },
            { 1, -1.994808810126, 0.9993786562321 },
            { 1, 0, 0 }
        };
    }
}
