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
        * TJB B2:1924-1962-2081-2163 Hz; 8th order; 44.1 kHz; -40/-3 dB
        * Treble
        *************************************************/
        static readonly double[,] tjb_B2_1_num = new double[9, 3]
        {
            { 0.5725625830828, 0, 0 },
            { 1, -1.901964650886, 1 },
            { 0.5725625830828, 0, 0 },
            { 1, -1.930934444181, 1 },
            { 0.1742913056963, 0, 0 },
            { 1, -1.910564643415, 1 },
            { 0.1742913056963, 0, 0 },
            { 1, -1.924255247085, 1 },
            { 1, 0, 0 }
        };

        static readonly double[,] tjb_B2_1_denom = new double[9, 3]
        {
            { 1, 0, 0 },
            { 1, -1.911656618733, 0.9961013413994 },
            { 1, 0, 0 },
            { 1, -1.91628161941, 0.9962048575498 },
            { 1, 0, 0 },
            { 1, -1.911913703262, 0.9989642105762 },
            { 1, 0, 0 },
            { 1, -1.92127243328, 0.9990199433871 },
            { 1, 0, 0 }
        };
    }
}
