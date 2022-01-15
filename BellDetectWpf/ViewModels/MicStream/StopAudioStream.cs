using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Enums;

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
            string audioFFT;

            DateTime currTime = DateTime.Now;
            sb = new StringBuilder();
            sb.Append(@"C:\ProgramData\BellDetect\audioFFTresults");
            sb.Append('-');
            sb.Append(currTime.Year);
            sb.Append('-');

            if (currTime.Month < 10)
            {
                sb.Append('0');
                sb.Append(currTime.Month);
            }
            else
            {
                sb.Append(currTime.Month);
            }

            sb.Append('-');

            if (currTime.Day < 10)
            {
                sb.Append('0');
                sb.Append(currTime.Day);
            }
            else
            {
                sb.Append(currTime.Day);
            }

            sb.Append('-');

            if (currTime.Hour < 10)
            {
                sb.Append('0');
                sb.Append(currTime.Hour);
            }
            else
            {
                sb.Append(currTime.Hour);
            }

            sb.Append('h');

            if (currTime.Minute < 10)
            {
                sb.Append('0');
                sb.Append(currTime.Minute);
            }
            else
            {
                sb.Append(currTime.Minute);
            }

            sb.Append('m');

            if (currTime.Second < 10)
            {
                sb.Append('0');
                sb.Append(currTime.Second);
            }
            else
            {
                sb.Append(currTime.Second);
            }

            sb.Append('s');
            sb.Append(".txt");

            audioFFT = sb.ToString();

            // Stop recording if WaveIn exists
            if (MicStreamVM.Output == OutputEnum.KeyPress)
            {
                if (waveIn != null)
                {
                    waveIn.StopRecording();
                    waveIn.Dispose();
                }
            }
            else
            {
                if (capture != null)
                {
                    capture.StopRecording();
                    capture.Dispose();
                }
            }

            // Delete file if it already exists
            if (File.Exists(audioFFT))
            {
                File.Delete(audioFFT);
            }

            // Create a new file     
            using FileStream fs = File.Create(audioFFT);

            // Write header row
            sb = new StringBuilder();
            sb.Append("Bin #");
            sb.Append('\t');
            
            sb.Append("Hz | sec");
            sb.Append('\t');

            count = 0;
            
            foreach (double[] fft in MicStreamVM.ResultArr)
            {
                time = Math.Round(((double)FFTVM.N / MicStreamVM.SampleFrequency) * count, 3);
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

                sb.Append(i);
                sb.Append('\t');

                freq = Math.Round(((double)MicStreamVM.SampleFrequency / FFTVM.N) * i, 1);
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
