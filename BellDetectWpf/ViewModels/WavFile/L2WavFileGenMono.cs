using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.ViewModels;

namespace BellDetectWpf.ViewModels.WavFile
{
    public static class L2WavFileGenMono
    {
        public static void WavFileGenMono()
        {
            // Declare variables
            uint formatParametersSize; // bytes
            ushort wavType; // number
            ushort numChannels; // number
            uint sampleFrequency; // kHz

            ushort bitsPerSamplePerChannel; // bits
            uint dataRate; // bytes per second
            ushort blockAlignment; // bytes

            uint totalSampleTime; // seconds
            uint numSamples; // number

            uint dataSize; // bytes
            uint fileSize; // bytes

            string fileName;
            StringBuilder sb;

            // Set variables
            formatParametersSize = 16; // bytes
            wavType = 1; // number
            numChannels = 1; // number
            sampleFrequency = 44100; // kHz

            bitsPerSamplePerChannel = 16; // bits
            dataRate = (sampleFrequency * bitsPerSamplePerChannel * numChannels) / 8; // bytes per second
            blockAlignment = (ushort)((bitsPerSamplePerChannel * numChannels) / 8); // bytes
            
            totalSampleTime = 2; // seconds
            numSamples = (sampleFrequency * totalSampleTime) + 1; // number

            dataSize = (uint)((bitsPerSamplePerChannel * numChannels * numSamples) / 8); // bytes
            fileSize = dataSize + 36; // bytes

            fileName = @"C:\temp\waveform.wav";

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            // Create .wav file
            using FileStream f = new FileStream(fileName, FileMode.Create);
            using BinaryWriter wr = new BinaryWriter(f);

            wr.Write(Encoding.ASCII.GetBytes("RIFF"));
            wr.Write(fileSize);
            wr.Write(Encoding.ASCII.GetBytes("WAVE")); // 
            wr.Write(Encoding.ASCII.GetBytes("fmt "));
            wr.Write(formatParametersSize); // 
            wr.Write(wavType);
            wr.Write(numChannels);
            wr.Write(sampleFrequency);
            wr.Write(dataRate);
            wr.Write(blockAlignment);
            wr.Write(bitsPerSamplePerChannel);
            wr.Write(Encoding.ASCII.GetBytes("data"));
            wr.Write(dataSize);
            
            // Create waveform data
            int numWaves = 2;
            double[,] waveSpec = new double[numWaves, 2];
            double scaleFactor = 1;

            // Frequency and amplitude
            waveSpec[0, 0] = 4400.0;     waveSpec[0, 1] = 16383.0;
            waveSpec[1, 0] = 8800.0;     waveSpec[1, 1] = 16383.0;

            double[] time = new double[numSamples];
            double[,] waves = new double[numWaves, numSamples];
            WaveformVM.Waveform = new short[numSamples];

            double x;
            double w;
            double wSum;

            // Create time array
            for (int i = 0; i < numSamples; i++)
            {
                time[i] = (double)i / sampleFrequency;
            }

            // Create waves 1 (zero based)
            for (int i = 0; i < numSamples; i++)
            {
                wSum = 0.0;

                for (int j = 0; j < numWaves; j++)
                {
                    if (waveSpec[j, 0] != 0.0)
                    {
                        x = time[i] * waveSpec[j, 0] * 2.0 * Math.PI;
                        w = Math.Sin(x) * waveSpec[j, 1] * scaleFactor;
                        waves[j, i] = w;
                        wSum += w;
                    }
                }

                // Prevent any clipping
                if (wSum > 32767)
                {
                    wSum = 32767;
                }
                else if (wSum < -32767)
                {
                    wSum = -32767;
                }

                WaveformVM.Waveform[i] = (short)Math.Round(wSum);
            }

            // Write the summed waveforms to the binary write
            for (int i = 0; i < numSamples; i++)
            {
                wr.Write(WaveformVM.Waveform[i]);
            }
            
            // Write out waveform to a text file (first 256 samples)
            
            fileName = @"C:\temp\waveform.txt";

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            sb = new StringBuilder();

            using FileStream fs = File.Create(fileName);

            for (int i = 0; i < 256; i++)
            {
                sb.Clear();
                sb.Append(WaveformVM.Waveform[i]);
                sb.Append('\n');

                Byte[] row = new UTF8Encoding(true).GetBytes(sb.ToString());
                fs.Write(row, 0, row.Length);
            }
        }
    }
}
