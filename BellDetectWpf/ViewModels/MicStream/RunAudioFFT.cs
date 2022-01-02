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

        public static void RunAudioFFT(byte[] buffer, int bytesRecorded)
        {
            int bytePos;
            short amp;
            double amplitude;
            double[] fftResult;
            int idx;

            TimeSpan currSw;
            TimeSpan interval;

            // For testing: log number of bytes recorded
            // MainWinVM.Logger.Info("Bytes Recorded: " + bytesRecorded.ToString());

            // Don't run FFT if the buffer is not 2048 bytes in length
            // A shorter buffer will likely be received when the recording is stopped
            if (bytesRecorded != 2048)
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
            
            // Copy items in the buffer into XRe array, and set XIm array values to zero
            bytePos = 0;

            for (int i = 0; i < FFTVM.N; i++)
            {
                // Note: little endianess
                amp = BitConverter.ToInt16(buffer, bytePos);
                FFTVM.XRe[i] = amp;
                FFTVM.XIm[i] = 0;

                bytePos += 2;
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

            // For the first 30 seconds of mic streaming only, add FFT result to the ResultArr list
            if (MicStreamVM.SW.Elapsed.TotalSeconds < 30)
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
                // Frequency in detection array must be non-zero
                if (MicStreamVM.DetectionSpecArr[i].Frequency != 0)
                {

                    // if output is KeyPress, Key must be populated
                    if (MicStreamVM.Output == OutputEnum.Transcription || MicStreamVM.DetectionSpecArr[i].Key != ' ')
                    {
                        idx = MicStreamVM.DetectionSpecArr[i].Frequency / 20;

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
                                    MainWinVM.Logger.Info("BlowCount: " + TranscribeVM.BlowCount.ToString());
                                    MainWinVM.Logger.Info("RowNum: " + TranscribeVM.CurrRowNum.ToString());
                                    MainWinVM.Logger.Info("Place: " + TranscribeVM.CurrPlace.ToString());
                                    MainWinVM.Logger.Info("NumItems in Arr: " + TranscribeVM.TranscriptionArr.Count.ToString());

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
