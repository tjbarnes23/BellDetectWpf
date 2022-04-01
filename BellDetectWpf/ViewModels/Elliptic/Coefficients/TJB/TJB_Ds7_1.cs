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
        * TJB Ds7: 1163-1211-1283-1308 Hz; 8th order; 44.1 kHz; -40/-3 dB
        * 6th
        *************************************************/
        static readonly double[,] tjb_Ds7_1_num = new double[9, 3]
        {
            { 0.5728834589769, 0, 0 },
            { 1, -1.962623639221, 1 },
            { 0.5728834589769, 0, 0 },
            { 1, -1.973531393315, 1 },
            { 0.1743216967309, 0, 0 },
            { 1, -1.965861570607, 1 },
            { 0.1743216967309, 0, 0 },
            { 1, -1.971015605825, 1 },
            { 1, 0, 0 }
        };

        static readonly double[,] tjb_Ds7_1_denom = new double[9, 3]
        {
            { 1, 0, 0 },
            { 1, -1.965355698953, 0.9976393984693 },
            { 1, 0, 0 },
            { 1, -1.967122341459, 0.9977020125669 },
            { 1, 0, 0 },
            { 1, -1.966125958653, 0.9993731884407 },
            { 1, 0, 0 },
            { 1, -1.969662838394, 0.9994068636127 },
            { 1, 0, 0 }
        };
    }
}
