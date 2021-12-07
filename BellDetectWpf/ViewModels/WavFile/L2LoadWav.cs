using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf.ViewModels.WavFile
{
    public static class L2LoadWav
    {
        public static void LoadWav()
        {
            uint fileSize;
            uint formatParametersSize;
            ushort wavType;
            ushort numChannels;
            uint dataRate;
            ushort blockAlignment;
            ushort bitsPerSamplePerChannel;
            uint dataSizeBytes;

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
            WaveformVM.SampleFrequency = br.ReadUInt32();

            // Read dataRate (uint)
            dataRate = br.ReadUInt32();

            // Read blockAlignment (ushort)
            blockAlignment = br.ReadUInt16();

            // Read bitsPerSamplePerChannel (ushort)
            bitsPerSamplePerChannel = br.ReadUInt16();

            // Read 'data'
            for (int i = 0; i < 4; i++)
            {
                _ = br.ReadByte();
            }

            // Read dataSize (uint)
            dataSizeBytes = br.ReadUInt32();
            WaveformVM.NumSamples = dataSizeBytes / 2;

            WaveformVM.Waveform = new short[WaveformVM.NumSamples];

            for (int i = 0; i < WaveformVM.NumSamples; i++)
            {
                WaveformVM.Waveform[i] = br.ReadInt16();
            }
        }
    }
}
