﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace BellDetectWpf.Repository
{
    public static class Repo
    {
        /**************************************************
        * App
        **************************************************/
        public static Logger Logger { get; set; }

        /**************************************************
        * LoadWav
        **************************************************/
        public static string WavFilePathName { get; set; }

        public static string LoadWavStatus { get; set; }

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

        /**************************************************
        * FFT
        **************************************************/
        public static uint N { get; set; }

        public static string FFTFilePathName { get; set; }

        public static string FFTStatus { get; set; }

        /**************************************************
        * Elliptic
        **************************************************/
        public static string EllipticFilePathName { get; set; }

        public static string EllipticStatus { get; set; }

        public static double[] FilteredWaveformArrDbl { get; set; } // Double array to use while processing

        public static short[,] FilteredWaveformArr { get; set; } // Array to hold filtered waveforms for each bell
    }
}
