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
                // Set parameters for audio stream
                MicStreamVM.SampleFrequency = 20480;
                MicStreamVM.SampleDepth = 16;
                MicStreamVM.NumChannels = 1;

                // Set FFT parameters
                FFTVM.Log2N = 11;
                FFTVM.N = (uint)1 << (int)FFTVM.Log2N;
                FFTVM.NA = 1; // Number of FFTs to be run

                // Set up NAudio variables
                waveIn = new();
                waveFormat = new(MicStreamVM.SampleFrequency, MicStreamVM.SampleDepth, MicStreamVM.NumChannels);
                waveIn.DeviceNumber = 0; // Uses input device specified in Windows Sound settings
                waveIn.WaveFormat = waveFormat;
                waveIn.BufferMilliseconds = 100;
                waveIn.NumberOfBuffers = 2;
                waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(MicDataAvailable);
            }
            else
            {
                // Set FFT parameters
                FFTVM.Log2N = 11;
                FFTVM.N = (uint)1 << (int)FFTVM.Log2N;
                FFTVM.NA = 1; // Number of FFTs to be run

                // Set up NAudio Wasapi variables
                // *** For now, use the output device specified in Windows Sound settings ***
                capture = new(); // Uses output device specified in Windows Sound settings
                capture.DataAvailable += new EventHandler<WaveInEventArgs>(LoopBackDataAvailable);

                // MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
                // MMDeviceCollection deviceCollection = enumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);
                // MMDevice device = deviceCollection.FirstOrDefault(d => d.FriendlyName == MicStreamVM.SourceDict[MicStreamVM.SourceId]);

                // Obtain parameters for Wasapi audio stream
                MicStreamVM.SampleFrequency = capture.WaveFormat.SampleRate;
                MicStreamVM.SampleDepth = capture.WaveFormat.BitsPerSample;
                MicStreamVM.NumChannels = capture.WaveFormat.Channels;

                // Log parameters used by Wasapi
                MainWinVM.Logger.Info($"Sample rate: {MicStreamVM.SampleFrequency}");
                MainWinVM.Logger.Info($"Sample depth: {MicStreamVM.SampleDepth}");
                MainWinVM.Logger.Info($"Num channels: {MicStreamVM.NumChannels}");
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
            bytePos = 0;

            for (int i = 0; i < numBytes / 8; i++)
            {
                flAmp = BitConverter.ToSingle(e.Buffer, bytePos);
                amp = (short)(flAmp * 32767);
                bytePos += 8;
                
                MicStreamVM.AudioBuffer.Enqueue(amp);
            }

            while (MicStreamVM.AudioBuffer.Count >= FFTVM.N)
            {
                RunAudioFFT();
            }
        }
    }
}
