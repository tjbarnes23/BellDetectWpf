using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Enums;
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
        public static string WavInitialDirectory { get; set; }

        public static string WavFilePathName { get; set; }

        public static string LoadWavStatus { get; set; }

        public static uint SampleFrequency { get; set; }

        public static ushort SampleDepth { get; set; }

        public static ushort WavNumChannels { get; set; }

        public static ushort BlockAlignment { get; set; }

        public static uint DataRate { get; set; }

        public static uint DataSize { get; set; }

        public static double LengthSeconds { get; set; }

        public static uint NumSamples { get; set; }

        public static double[] Time { get; set; }

        public static int[,] WavDataInt { get; set; }


        /**************************************************
        * CreateWav
        **************************************************/
        public static string CreateWavInitialDirectory { get; set; }

        public static string CreateWavFilePathName { get; set; }

        public static string CreateWavStatus { get; set; }


        /**************************************************
        * FFT
        **************************************************/
        public static uint N { get; set; }

        public static string FFTInitialDirectory { get; set; }

        public static string FFTFilePathName { get; set; }

        public static string FFTStatus { get; set; }


        /**************************************************
        * DFT
        **************************************************/
        public static string DFTInitialDirectory { get; set; }

        public static string DFTFilePathName { get; set; }

        public static string DFTStatus { get; set; }


        /**************************************************
        * Butterworth
        **************************************************/
        public static string ButterworthInitialDirectory { get; set; }

        public static string ButterworthFilePathName { get; set; }

        public static FilterButterworthEnum FilterButterworth { get; set; }

        public static string ButterworthStatus { get; set; }

        public static ushort ButterworthNumChannels { get; set; }

        public static short[,] ButterworthFilteredWaveformArr { get; set; }


        /**************************************************
        * Elliptic
        **************************************************/
        public static string EllipticInitialDirectory { get; set; }
        
        public static string EllipticFilePathName { get; set; }

        public static FilterEllipticEnum FilterElliptic { get; set; }

        public static string EllipticStatus { get; set; }

        public static ushort EllipticNumChannels { get; set; }

        public static short[,] EllipticFilteredWaveformArr { get; set; }


        /**************************************************
        * FIR
        **************************************************/
        public static string FIRInitialDirectory { get; set; }

        public static string FIRFilePathName { get; set; }

        public static FilterFIREnum FilterFIR { get; set; }

        public static string FIRStatus { get; set; }

        public static ushort FIRNumChannels { get; set; }

        public static short[,] FIRFilteredWaveformArr { get; set; }


        /**************************************************
        * Key Press
        **************************************************/
        public static KeyPressModeEnum KeyPressMode { get; set; }
        
        public static string StartStopTxt { get; set; }

        public static string KeyPressStatus { get; set; }
    }
}
