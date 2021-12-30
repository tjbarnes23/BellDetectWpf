using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.ViewModels.FFT;
using BellDetectWpf.ViewModels.KeyPress;
using BellDetectWpf.ViewModels.Shared;

namespace BellDetectWpf.ViewModels.MicStream
{
    public static partial class C_MicStream
    {
        public const byte JKey = 0x24;
        public const byte FKey = 0x21;

        public static void RunMicFFT(byte[] buffer, int bytesRecorded)
        {
            int bytePos;
            short amp;
            double amplitude;
            double[] fftResult;

            TimeSpan ts3;
            TimeSpan ts4;
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

            fftResult = new double[FFTVM.N / 2];

            // Add to col 2 of Results array
            // Double the amplitudes to adjust the 2-sided frequency plot, then divide by the number of samples
            for (int i = 0; i < (FFTVM.N / 2); i++)
            {
                amplitude = Math.Sqrt(Math.Pow(FFTVM.XRe[i], 2) + Math.Pow(FFTVM.XIm[i], 2));
                amplitude = (amplitude * 2) / FFTVM.N;
                MicStreamVM.DetectionArr[i, 2] = Math.Round(amplitude, 0);
                fftResult[i] = Math.Round(amplitude, 0);
            }

            // Add fftResult to the list
            MicStreamVM.ResultArr.Add(fftResult);

            // For testing: write 3rd and 4th frequency buckets to logger
            // MainWinVM.Logger.Info("1680hz: " + MicStreamVM.DetectionArr[84, 2].ToString());
            // MainWinVM.Logger.Info("1500hz: " + MicStreamVM.DetectionArr[75, 2].ToString());

            // Detect whether bells 3 or 4 have struck

            // 3rd is at 1680Hz, bucket index 84
            if (MicStreamVM.DetectionArr[84, 2] > 2000 &&
                    ((MicStreamVM.DetectionArr[84, 1] < 1500) || (MicStreamVM.DetectionArr[84, 0] < 1500)))
            {
                ts3 = MicStreamVM.SW.Elapsed;
                interval = ts3 - MicStreamVM.TS3;

                if (interval.TotalMilliseconds > 1250)
                {
                    MainWinVM.Logger.Info("3rd detected");
                    C_KeyPress.Press(JKey);
                    MicStreamVM.TS3 = ts3;
                }
            }

            // 4th is at 1500Hz, bucket index 75
            if (MicStreamVM.DetectionArr[75, 2] > 850 &&
                    ((MicStreamVM.DetectionArr[75, 1] < 800) || (MicStreamVM.DetectionArr[75, 0] < 800)))
            {
                ts4 = MicStreamVM.SW.Elapsed;
                interval = ts4 - MicStreamVM.TS4;

                if (interval.TotalMilliseconds > 1250)
                {
                    MainWinVM.Logger.Info("4th detected");
                    C_KeyPress.Press(FKey);
                    MicStreamVM.TS4 = ts4;
                }
            }
        }
    }
}
