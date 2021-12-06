using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf.ViewModels.WavFile
{
    public static class L2WavFileGenStereo
    {
        public static void WavFileGenStereo()
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
            numChannels = 2; // number
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
            int numWaves = 1;
            double[] time = new double[numSamples];
            double[,] left = new double[numWaves, numSamples];
            double[,] right = new double[numWaves, numSamples];
            short[] leftSum = new short[numSamples];
            short[] rightSum = new short[numSamples];

            double x;
            double frl = 440.0;
            double frr = 660.0;
            double w;

            // Create time array
            for (int i = 0; i < numSamples; i++)
            {
                time[i] = (double)i / sampleFrequency;
            }

            // Create left wave 1 (zero based)
            for (int i = 0; i < numSamples; i++)
            {
                x = time[i] * frl * 2.0 * Math.PI;
                left[0, i] = Math.Sin(x) * 32767.0;
            }

            // Create right wave 1 (zero based)
            for (int i = 0; i < numSamples; i++)
            {
                x = time[i] * frr * 2.0 * Math.PI;
                right[0, i] = Math.Sin(x) * 32767.0;
            }

            // Create additional waves

            // Sum the left waves and convert to short format
            for (int i = 0; i < numSamples; i++)
            {
                w = 0;

                for (int j = 0; j < numWaves; j++)
                {
                    w += left[j, i];
                }

                leftSum[i] = (short)Math.Round(w);

                // Debug.Print(leftSum[i].ToString());
            }

            // Sum the right waves and convert to short format
            for (int i = 0; i < numSamples; i++)
            {
                w = 0;

                for (int j = 0; j < numWaves; j++)
                {
                    w += right[j, i];
                }

                rightSum[i] = (short)Math.Round(w);

                // Debug.Print(rightSum[i].ToString());
            }

            // Write the summed waveforms to the binary write
            for (int i = 0; i < numSamples; i++)
            {
                wr.Write(leftSum[i]);
                wr.Write(rightSum[i]);
            }
            
            wr.Close();
            f.Close();
        }
    }
}
