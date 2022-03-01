using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf.Repository
{
    public static class Repo
    {
        /**************************************************
        * LoadWav
        **************************************************/
        public static string FilePathName { get; set; }

        public static string Status { get; set; }

        public static uint SampleFrequency { get; set; }

        public static ushort SampleDepth { get; set; }

        public static ushort NumChannels { get; set; }

        public static ushort BlockAlignment { get; set; }

        public static uint DataRate { get; set; }

        public static uint DataSize { get; set; }

        public static double LengthSeconds { get; set; }

        public static uint NumSamples { get; set; }

        public static double[] Time { get; set; }

        public static int[,] WavDataInt { get; set; }

        public static double[,] WavDataFloat { get; set; }
    }
}
