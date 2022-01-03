using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Enums;
using BellDetectWpf.ViewModels.FFT;
using BellDetectWpf.ViewModels.MainWin;
using BellDetectWpf.ViewModels.Transcribe;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace BellDetectWpf.ViewModels.MicStream
{
    public static partial class C_MicStream
    {
        private static WaveInEvent waveIn;
        private static WaveFormat waveFormat;
        private static WasapiLoopbackCapture capture;

        public static void StartAudioStream()
        {
            if (MicStreamVM.SourceId == 0)
            {
                // Initialize FFT variables
                MicStreamVM.SampleFrequency = 20480;
                MicStreamVM.SampleDepth = 16;
                MicStreamVM.NumChannels = 1;

                FFTVM.Log2N = 10;
                FFTVM.N = 1024;
                FFTVM.NA = 1; // Number of FFTs to be run

                // Set up NAudio variables
                waveIn = new();
                waveFormat = new(MicStreamVM.SampleFrequency, MicStreamVM.SampleDepth, MicStreamVM.NumChannels);
                waveIn.DeviceNumber = 0; // Uses input device specified in Windows Sound settings
                waveIn.WaveFormat = waveFormat;
                waveIn.BufferMilliseconds = 50;
                waveIn.NumberOfBuffers = 2;
                waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(MicDataAvailable);
            }
            else
            {
                MicStreamVM.SampleFrequency = 48000;

                FFTVM.Log2N = 11;
                FFTVM.N = 2048;
                FFTVM.NA = 1; // Number of FFTs to be run

                // Set up NAudio Wasapi variables
                // *** For now, use the output device specified in Windows Sound settings ***
                // MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
                // MMDeviceCollection deviceCollection = enumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);
                // MMDevice device = deviceCollection.FirstOrDefault(d => d.FriendlyName == MicStreamVM.SourceDict[MicStreamVM.SourceId]);

                capture = new(); // Uses output device specified in Windows Sound settings
                capture.DataAvailable += new EventHandler<WaveInEventArgs>(LoopBackDataAvailable);

                // For testing
                MainWinVM.Logger.Info($"SampleRate: {capture.WaveFormat.SampleRate}");
                MainWinVM.Logger.Info($"Bits per sample: {capture.WaveFormat.BitsPerSample}");
                MainWinVM.Logger.Info($"Num channels: {capture.WaveFormat.Channels}");
                MainWinVM.Logger.Info(string.Empty);
            }

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

            // If output is transcription, initialize transcription array
            if (MicStreamVM.Output == OutputEnum.Transcription)
            {
                C_Transcribe.InitializeTranscriptionArr();
                MainWinVM.Tp.TranscriptionDataGrid.ItemsSource = TranscribeVM.TranscriptionArr;
            }

            // Initialize stopwatch
            MicStreamVM.SW = new Stopwatch();
            MicStreamVM.SW.Start();

            // Initalize buffer
            MicStreamVM.AudioBuffer = new();
            
            // Start recording
            if (MicStreamVM.SourceId == 0)
            {
                waveIn.StartRecording();
            }
            else
            {
                capture.StartRecording();
            }
        }

        public static void MicDataAvailable(object sender, WaveInEventArgs e)
        {
            int numBytes;
            int bytePos;
            short amp;

            // Convert 2 bytes (16-bit signed int) into int16 and enqueue
            numBytes = e.BytesRecorded;
            MainWinVM.Logger.Info($"Bytes recorded: {e.BytesRecorded}");
            bytePos = 0;

            for (int i = 0; i < numBytes / 2; i++)
            {
                amp = BitConverter.ToInt16(e.Buffer, bytePos);
                bytePos += 2;

                MicStreamVM.AudioBuffer.Enqueue(amp);
            }

            while (MicStreamVM.AudioBuffer.Count >= FFTVM.N)
            {
                RunAudioFFT();
            }
        }

        public static void LoopBackDataAvailable(object sender, WaveInEventArgs e)
        {
            int numBytes;
            int bytePos;
            float flAmp;
            short amp;

            // Convert 4 bytes (32-bit float) into int16 and enqueue
            // Note that 2 channels are received, so ignore every other 8 bytes
            numBytes = e.BytesRecorded;
            
            MainWinVM.Logger.Info($"Bytes recorded: {e.BytesRecorded}");
            
            // Check num bytes recorded is divisible by 8
            if (numBytes % 8 != 0)
            {
                MainWinVM.Logger.Info("Warning: Bytes recorded are not divisible by 8");
            }

            bytePos = 0;

            for (int i = 0; i < numBytes / 8; i++)
            {
                flAmp = BitConverter.ToSingle(e.Buffer, bytePos);
                bytePos += 8;

                amp = (short)(flAmp * 32767);
                MicStreamVM.AudioBuffer.Enqueue(amp);
            }

            MainWinVM.Logger.Info($"Queue length before FFT: {MicStreamVM.AudioBuffer.Count}");

            while (MicStreamVM.AudioBuffer.Count >= FFTVM.N)
            {
                RunAudioFFT();
                MainWinVM.Logger.Info($"Queue length after FFT: {MicStreamVM.AudioBuffer.Count}");
            }

            MainWinVM.Logger.Info($"Queue length on exit: {MicStreamVM.AudioBuffer.Count}");
            MainWinVM.Logger.Info(string.Empty);
        }
    }
}
