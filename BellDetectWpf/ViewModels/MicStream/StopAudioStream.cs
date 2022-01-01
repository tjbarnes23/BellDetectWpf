using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf.ViewModels.MicStream
{
    public static partial class C_MicStream
    {
        public static void StopAudioStream()
        {
            StringBuilder sb;
            byte[] row;
            int count;
            double time;
            double freq;
            string micFFT;

            micFFT = @"C:\ProgramData\BellDetect\micFFTresults.txt";

            // Stop recording if WaveIn exists
            if (MicStreamVM.waveIn != null)
            {
                MicStreamVM.waveIn.StopRecording();
            }

            // Delete file if it already exists
            if (File.Exists(micFFT))
            {
                File.Delete(micFFT);
            }

            // Create a new file     
            using FileStream fs = File.Create(micFFT);

            // Write header row
            sb = new StringBuilder();
            sb.Append("Hz \\ sec\t");

            count = 0;
            
            foreach (double[] fft in MicStreamVM.ResultArr)
            {
                time = Math.Round(((double)FFTVM.N / 20480) * count, 3);
                sb.Append(time);
                sb.Append('\t');
                count++;
            }

            sb.Append('\n');
            row = new UTF8Encoding(true).GetBytes(sb.ToString());
            fs.Write(row, 0, row.Length);

            // Write data rows
            count = 0;

            for (int i = 0; i < FFTVM.N / 2; i++)
            {
                sb.Clear();

                freq = Math.Round(((double)20480 / FFTVM.N) * i, 1);
                sb.Append(freq);
                sb.Append('\t');

                foreach (double[] fft in MicStreamVM.ResultArr)
                {
                    sb.Append(fft[i]);
                    sb.Append('\t');
                }

                sb.Append('\n');
                row = new UTF8Encoding(true).GetBytes(sb.ToString());
                fs.Write(row, 0, row.Length);
            }
        }
    }
}
