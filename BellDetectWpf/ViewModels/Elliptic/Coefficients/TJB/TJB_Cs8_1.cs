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
        * TJB Cs8: 1032-1074-1163-1211 Hz; 6th order; 44.1 kHz; -40/-3 dB
        * 7th
        *************************************************/
        static readonly double[,] tjb_Cs8_1_num = new double[7, 3]
        {
            { 0.2349152311755, 0, 0 },
            { 1, -1.969762728342, 1 },
            { 0.2349152311755, 0, 0 },
            { 1, -1.97883207118, 1 },
            { 0.006327297215008, 0, 0 },
            { 1, 0, -1 },
            { 1, 0, 0 }
        };

        static readonly double[,] tjb_Cs8_1_denom = new double[7, 3]
        {
            { 1, 0, 0 },
            { 1, -1.971046501715, 0.9982416149975 },
            { 1, 0, 0 },
            { 1, -1.974864716079, 0.9983646258184 },
            { 1, 0, 0 },
            { 1, -1.970660521344, 0.9959107705411 },
            { 1, 0, 0 }
        };
    }
}
