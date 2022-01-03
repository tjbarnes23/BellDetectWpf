using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BellDetectWpf.Enums;
using BellDetectWpf.Models;
using BellDetectWpf.ViewModels.FFT;
using BellDetectWpf.ViewModels.KeyPress;
using BellDetectWpf.ViewModels.Shared;

namespace BellDetectWpf.ViewModels.MicStream
{
    public static partial class C_MicStream
    {
        public const byte JKey = 0x24;
        public const byte FKey = 0x21;
        public const byte RKey = 0x13;
        public const byte UKey = 0x16;

        public static void RunAudioFFT()
        {
            short amp;
            double amplitude;
            double[] fftResult;
            int idx;

            TimeSpan currSw;
            TimeSpan interval;

            // For testing: log number of bytes recorded
            // MainWinVM.Logger.Info("Bytes Recorded: " + bytesRecorded.ToString());

            // This method should only be called when the buffer has at least FFTVM.N items,
            // but double check that here
            if (MicStreamVM.AudioBuffer.Count < FFTVM.N)
            {
                return;
            }

            // Move cols 1 and 2 in Results to become cols 0 and 1; new results go in col 2
            for (int i = 0; i < (FFTVM.N / 2); i++)
            {
                for (int j = 0; j < 2; j++)
                { 
                    MicStreamVM.DetectionArr[i, j] = MicStreamVM.DetectionArr[i, j + 1];
                }                
            }
            
            // Pop items in the buffer into XRe array, and set XIm array values to zero
            for (int i = 0; i < FFTVM.N; i++)
            {
                amp = MicStreamVM.AudioBuffer.Dequeue();
                FFTVM.XRe[i] = amp;
                FFTVM.XIm[i] = 0;
            }

            // Run FFT
            C_FFT.ExecuteFFT();

            // Add to col 2 of Results array
            // Double the amplitudes to adjust the 2-sided frequency plot, then divide by the number of samples
            for (int i = 0; i < (FFTVM.N / 2); i++)
            {
                amplitude = Math.Sqrt(Math.Pow(FFTVM.XRe[i], 2) + Math.Pow(FFTVM.XIm[i], 2));
                amplitude = (amplitude * 2) / FFTVM.N;
                MicStreamVM.DetectionArr[i, 2] = Math.Round(amplitude, 0);
            }

            // Add FFT result to the ResultArr list
            // Only do this for the first 60 seconds of audio streaming, or the first 8192 FFT results, whichever is less
            if ((MicStreamVM.ResultArr.Count * FFTVM.N) / MicStreamVM.SampleFrequency < 60 && MicStreamVM.ResultArr.Count < 8192)
            {
                fftResult = new double[FFTVM.N / 2];

                for (int i = 0; i < (FFTVM.N / 2); i++)
                {
                    fftResult[i] = MicStreamVM.DetectionArr[i, 2];
                }

                MicStreamVM.ResultArr.Add(fftResult);
            }

            // Detect whether bells have struck
            for (int i = 0; i < 12; i++)
            {
                // Frequency bin in detection array must be non-zero
                if (MicStreamVM.DetectionSpecArr[i].FrequencyBin != 0)
                {

                    // if output is KeyPress, Key must be populated
                    if (MicStreamVM.Output == OutputEnum.Transcription || MicStreamVM.DetectionSpecArr[i].Key != ' ')
                    {
                        idx = MicStreamVM.DetectionSpecArr[i].FrequencyBin;

                        // Amplitude tests
                        if (MicStreamVM.DetectionArr[idx, 2] > MicStreamVM.DetectionSpecArr[i].AmplitudeHigh &&
                                ((MicStreamVM.DetectionArr[idx, 1] < MicStreamVM.DetectionSpecArr[i].AmplitudeLow) ||
                                (MicStreamVM.DetectionArr[idx, 0] < MicStreamVM.DetectionSpecArr[i].AmplitudeLow)))
                        {
                            currSw = MicStreamVM.SW.Elapsed;
                            interval = currSw - MicStreamVM.LastDetectionArr[i];

                            // Min time interval tests
                            if (interval.TotalMilliseconds > MicStreamVM.DetectionSpecArr[i].MinTimeBetweenDetectionsMs ||
                                    MicStreamVM.LastDetectionArr[i].Ticks == 0)
                            {

                                // Test which type of output
                                if (MicStreamVM.Output == OutputEnum.KeyPress)
                                {
                                    switch (MicStreamVM.DetectionSpecArr[i].Key)
                                    {
                                        case 'j':
                                            C_KeyPress.Press(JKey);
                                            break;

                                        case 'f':
                                            C_KeyPress.Press(FKey);
                                            break;

                                        case 'r':
                                            C_KeyPress.Press(RKey);
                                            break;

                                        case 'u':
                                            C_KeyPress.Press(UKey);
                                            break;

                                        default:
                                            break;
                                    }
                                }
                                else
                                {
                                    TranscribeVM.TranscriptionArr[TranscribeVM.CurrRowNum][TranscribeVM.CurrPlace] = MicStreamVM.DetectionSpecArr[i].Bell;
                                    TranscribeVM.BlowCount++;
                                    TranscribeVM.CurrPlace = TranscribeVM.BlowCount % StageEnumExt.StageBells(TranscribeVM.Stage);

                                    if (TranscribeVM.CurrPlace == 0)
                                    {
                                        TranscribeVM.CurrRowNum++;
                                        Application.Current.Dispatcher.BeginInvoke(new Action(() => TranscribeVM.TranscriptionArr.Add(new Row())));
                                        Application.Current.Dispatcher.BeginInvoke(new Action(() => MainWinVM.Tp.TranscriptionDataGrid.ScrollIntoView(MainWinVM.Tp.TranscriptionDataGrid.Items[^1])));
                                    }
                                }

                                MicStreamVM.LastDetectionArr[i] = currSw;
                            }
                        }
                    }
                }
            }
        }
    }
}
