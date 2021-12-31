using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.ViewModels.FFT;
using NAudio.Wave;

namespace BellDetectWpf.ViewModels.MicStream
{
    public static partial class C_MicStream
    {
        public static void StartMicStream()
        {
            // Set up NAudio variables
            MicStreamVM.waveIn = new();
            MicStreamVM.waveFormat = new(20480, 16, 1);
            MicStreamVM.waveIn.DeviceNumber = 0;
            MicStreamVM.waveIn.WaveFormat = MicStreamVM.waveFormat;
            MicStreamVM.waveIn.BufferMilliseconds = 50;
            MicStreamVM.waveIn.NumberOfBuffers = 2;
            MicStreamVM.waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(MicDataAvailable);

            // Initialize FFT variables
            FFTVM.Log2N = 10;
            FFTVM.N = 1024;
            FFTVM.NA = 1; // Number of FFTs to be run

            // Initialize FFT results storage

            MicStreamVM.DetectionArr = new double[FFTVM.N / 2, 3]; // Detection uses the most recent 3 FFTs

            MicStreamVM.ResultArr = new List<double[]>();

            // Populate DetectionArr with zeros
            for (int i = 0; i < FFTVM.N / 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    MicStreamVM.DetectionArr[i, j] = 0.0;
                }
            }

            // Initialize FFT elements
            C_FFT.InitializeFFT();

            // Initialize last detection array
            InitializeLastDetection();

            // Initialize stopwatch
            MicStreamVM.SW = new Stopwatch();
            MicStreamVM.SW.Start();

            MicStreamVM.waveIn.StartRecording();
        }

        public static void MicDataAvailable(object sender, WaveInEventArgs e)
        {
            C_MicStream.RunMicFFT(e.Buffer, e.BytesRecorded);
        }
    }
}
