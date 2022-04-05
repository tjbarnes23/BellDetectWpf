using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Repository;

namespace BellDetectWpf.ViewModels
{
    public partial class CreateWavVM
    {
        public async Task WriteWav()
        {
            uint fileSize;
            uint dataSize;
            uint formatParametersSize;
            ushort wavType;
            uint dataRate;
            ushort blockAlignment;

            CreateWavStatus = "Saving .wav file...";
            await Task.Delay(25);

            // Set formatParametersSize (uint)
            formatParametersSize = 16; // 16 bytes per the WAV file spec

            // Set wavType (ushort)
            wavType = 1; // 1 = PCM

            // Set dataRate (uint)
            dataRate = (uint)(Repo.SampleFrequency * (Repo.SampleDepth / 8) * Repo.WavNumChannels); // bytes per second

            // Set blockAlignment (ushort)
            blockAlignment = (ushort)((Repo.SampleDepth / 8) * Repo.WavNumChannels); // bytes per sample

            // Set dataSize (uint)
            dataSize = (uint)(Repo.NumSamples * (Repo.SampleDepth / 8) * Repo.WavNumChannels);

            // Set fileSize (uint)
            fileSize = dataSize + formatParametersSize + 20; // bytes

            // Delete file if it already exists
            if (File.Exists(WavFilePathName))
            {
                File.Delete(WavFilePathName);
            }

            // Create .wav file
            using FileStream f = new FileStream(WavFilePathName, FileMode.Create);
            {
                using BinaryWriter wr = new BinaryWriter(f);
                {
                    wr.Write(Encoding.ASCII.GetBytes("RIFF"));
                    wr.Write(fileSize);
                    wr.Write(Encoding.ASCII.GetBytes("WAVE")); // 
                    wr.Write(Encoding.ASCII.GetBytes("fmt "));
                    wr.Write(formatParametersSize); // 
                    wr.Write(wavType);
                    wr.Write(Repo.WavNumChannels);
                    wr.Write(Repo.SampleFrequency);
                    wr.Write(dataRate);
                    wr.Write(blockAlignment);
                    wr.Write(Repo.SampleDepth);
                    wr.Write(Encoding.ASCII.GetBytes("data"));
                    wr.Write(dataSize);

                    // Write .wav file
                    for (int i = 0; i < Repo.NumSamples; i++)
                    {
                        for (int j = 0; j < Repo.WavNumChannels; j++)
                        {
                            wr.Write((short)Repo.WavDataInt[j, i]);
                        }
                    }
                }
            }

            CreateWavStatus = ".wav file saved";
            await Task.Delay(25);
        }
    }
}
