using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf
{
    public static class WavFileGenTrinity8th
    {
        public static void GenerateTrinity8thWavFile()
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

            // Create .wav file
            FileStream f = new FileStream(@"C:\temp\waveform.wav", FileMode.Create);
            BinaryWriter wr = new BinaryWriter(f);

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
            int numWaves = 11;
            double[,] waveSpec = new double[numWaves, 2];
            double scaleFactor = 2000;

            // Frequency and amplitude
            waveSpec[0, 0] = 221.5;     waveSpec[0, 1] = 2.1756; // hum
            waveSpec[1, 0] = 441.5;     waveSpec[1, 1] = 1.659; // prime
            waveSpec[2, 0] = 524.0;     waveSpec[2, 1] = 1.117; // tierce
            waveSpec[3, 0] = 659.0;     waveSpec[3, 1] = 0.174; // quint
            waveSpec[4, 0] = 883.5;     waveSpec[4, 1] = 3.464; // nominal
            waveSpec[5, 0] = 1188.5;    waveSpec[5, 1] = 0.9506; // unnamed
            waveSpec[6, 0] = 1322.5;    waveSpec[6, 1] = 2.526; // superquint
            waveSpec[7, 0] = 1465.5;    waveSpec[7, 1] = 0.631; // unnamed
            waveSpec[8, 0] = 1832.5;    waveSpec[8, 1] = 0.6237; // octave above nominal
            waveSpec[9, 0] = 2389.0;    waveSpec[9, 1] = 0.6646; // unnamed
            waveSpec[10, 0] = 2994.0;   waveSpec[10, 1] = 0.484; // unnamed

            double[] time = new double[numSamples];
            double[,] waves = new double[numWaves, numSamples];
            short[] wavesSum = new short[numSamples];

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

                wavesSum[i] = (short)Math.Round(wSum);
            }

            // Write the summed waveforms to the binary write
            for (int i = 0; i < numSamples; i++)
            {
                wr.Write(wavesSum[i]);
            }
            
            wr.Close();
            f.Close();
        }
    }
}
