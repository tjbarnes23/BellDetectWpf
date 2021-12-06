using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf.ViewModels.WavFile
{
    public static class L2WavRead
    {
        public static void WavRead()
        {
            uint fileSize;
            uint formatParametersSize;
            ushort wavType;
            ushort numChannels;
            uint sampleFrequency;
            uint dataRate;
            ushort blockAlignment;
            ushort bitsPerSamplePerChannel;
            uint dataSizeBytes;
            uint dataSizeShorts;

            short[] data;

            string fileName;

            // Set filename
            fileName = @"C:\Users\Tim\source\repos\tjbarnes23\BellDetectWpf\BellDetectWpf\BellSamples\8th handstroke mono 96 kHz signed 16-bit.wav";

            // Read .wav file
            using FileStream f = new FileStream(fileName, FileMode.Open);
            using BinaryReader br = new BinaryReader(f);

            // Read 'RIFF'
            for (int i = 0; i < 4; i++)
            {
                _ = br.ReadByte();
            }

            // Read fileSize (uint)
            fileSize = br.ReadUInt32();

            // Read 'WAVE' and 'fmt '
            for (int i = 0; i < 8; i++)
            {
                _ = br.ReadByte();
            }

            // Read formatParametersSize (uint)
            formatParametersSize = br.ReadUInt32();

            // Read wavType (ushort)
            wavType = br.ReadUInt16();

            // Read numChannels (ushort)
            numChannels = br.ReadUInt16();

            // Read sampleFrequency (uint)
            sampleFrequency = br.ReadUInt32();

            // Read dataRate (uint)
            dataRate = br.ReadUInt32();

            // Read blockAlignment (ushort)
            blockAlignment = br.ReadUInt16();

            // Read bitsPerSamplePerChannel (ushort)
            bitsPerSamplePerChannel = br.ReadUInt16();

            // Read dataSize (uint)
            dataSizeBytes = br.ReadUInt32();
            dataSizeShorts = dataSizeBytes / 2;

            data = new short[dataSizeShorts];

            for (int i = 0; i < dataSizeShorts; i++)
            {
                data[i] = br.ReadInt16();
            }

            Debug.Print(dataSizeShorts.ToString());
        }
    }
}
