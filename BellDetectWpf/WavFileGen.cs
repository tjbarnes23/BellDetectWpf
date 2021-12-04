using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf
{
    public static class WavFileGen
    {
        public static void GenerateWavFile()
        {
            uint sampleRate; // kHz
            ushort sampleLength; // seconds
            uint numSamples; // number
            uint sampleSize; // bytes
            ushort numChannels; // number
            uint dataSize; // bytes
            uint dataHeaderSize; // bytes
            uint headerSize; // bytes
            ushort format; // number
            uint dataRate; // bytes per second
            ushort blockAlignment; // bytes
            ushort bitsPerSample; // bits

            sampleRate = 22050; // kHz
            sampleLength = 2; // seconds
            numSamples = sampleRate * sampleLength; // number
            sampleSize = 1; // bytes
            numChannels = 1; // number
            dataSize = numSamples * sampleSize * numChannels; // bytes
            dataHeaderSize = dataSize + 36;
            headerSize = 16;
            format = 1;
            dataRate = sampleRate * sampleSize * numChannels;
            blockAlignment = (ushort)(sampleSize * numChannels);
            bitsPerSample = (ushort)(sampleSize * 8);

            FileStream f = new FileStream("a.wav", FileMode.Create);
            BinaryWriter wr = new BinaryWriter(f);

            wr.Write(Encoding.ASCII.GetBytes("RIFF"));
            wr.Write(dataHeaderSize);
            wr.Write(Encoding.ASCII.GetBytes("WAVE")); // 
            wr.Write(Encoding.ASCII.GetBytes("fmt "));
            wr.Write(headerSize); // 
            wr.Write(format);
            wr.Write(numChannels);
            wr.Write(sampleRate);
            wr.Write(dataRate);
            wr.Write(blockAlignment);
            wr.Write(bitsPerSample);
            wr.Write(Encoding.ASCII.GetBytes("data"));
            wr.Write(dataSize);

            double t = 0.0;
            double x;
            double yd;
            double fr = 440.0;
            byte y;

            for (int i = 0; i < numSamples; i++)
            {
                t += (1.0 / sampleRate);

                x = t * fr * 2.0 * Math.PI;

                yd = Math.Sin(x);
                yd = (yd * 127.0) + 127.0;
                y = (byte)Math.Round(yd);
                
                Debug.Print(y.ToString());

                wr.Write(y);
            }

            wr.Close();
            f.Close();
        }
    }
}
