using System.IO;
using System.Threading.Tasks;
using BellDetectWpf.Repository;
using Microsoft.Win32;

namespace BellDetectWpf.ViewModels
{
    public partial class LoadWavVM
    {
        public async Task LoadWav()
        {
            string riff;
            uint fileSize;
            string wave;
            string fmt;
            uint formatParametersSize;
            ushort wavType;
            string data;

            OpenFileDialog openDlg = new OpenFileDialog
            {
                Filter = string.Empty,
                InitialDirectory = @"C:\ProgramData\BellDetect"
            };

            if (openDlg.ShowDialog() == true)
            {
                WavFilePathName = openDlg.FileName;
                LoadWavStatus = "Loading waveform...";
                await Task.Delay(25);

                // Read .wav file
                using FileStream f = new FileStream(WavFilePathName, FileMode.Open);
                using BinaryReader br = new BinaryReader(f);

                // Read 'RIFF' (4 bytes)
                riff = System.Text.Encoding.UTF8.GetString(br.ReadBytes(4));

                // Read fileSize (uint)
                fileSize = br.ReadUInt32();

                // Read 'WAVE' (4 bytes)
                wave = System.Text.Encoding.UTF8.GetString(br.ReadBytes(4));

                // Read 'fmt ' (4 bytes)
                fmt = System.Text.Encoding.UTF8.GetString(br.ReadBytes(4));

                // Read formatParametersSize (uint)
                formatParametersSize = br.ReadUInt32();

                // Read wavType (ushort)
                wavType = br.ReadUInt16();

                // Read numChannels (ushort)
                NumChannels = br.ReadUInt16();

                // Read sampleFrequency (uint)
                SampleFrequency = br.ReadUInt32();

                // Read dataRate (uint)
                DataRate = br.ReadUInt32();

                // Read blockAlignment (ushort)
                BlockAlignment = br.ReadUInt16();

                // Read bitsPerSamplePerChannel (ushort)
                SampleDepth = br.ReadUInt16();

                // Read 'data' (4 bytes)
                data = System.Text.Encoding.UTF8.GetString(br.ReadBytes(4));

                // Read dataSize (uint)
                DataSize = br.ReadUInt32();

                // Calculate sample length in seconds
                LengthSeconds = (double)DataSize / (BlockAlignment * SampleFrequency);

                // Note: Audacity may add meta data about the .wav file to the end of the file, such that the
                // fileSize will be greater than LengthBytes + 36. The test below for fileSize is therefore
                // 'less than' rather than 'not equal to'
                if (riff != "RIFF" ||
                        fileSize < (DataSize + formatParametersSize + 20) ||
                        wave != "WAVE" ||
                        fmt != "fmt " ||
                        formatParametersSize != 16 ||
                        DataRate != NumChannels * SampleFrequency * (SampleDepth / 8) ||
                        BlockAlignment != NumChannels * (SampleDepth / 8) ||
                        (SampleDepth != 8 && SampleDepth != 16 && SampleDepth != 24 && SampleDepth != 32) ||
                        data != "data")
                {
                    LoadWavStatus = "Not a valid .wav file format. File not loaded.";
                }
                else
                { 
                    // Read in .wav file data
                    Repo.NumSamples = (uint)(DataSize / ((SampleDepth / 8) * NumChannels));
                    Repo.Time = new double[Repo.NumSamples];


                    // Handle 8-bit depth
                    if (SampleDepth == 8)
                    {
                        Repo.WavDataInt = new int[NumChannels, Repo.NumSamples];

                        for (int i = 0; i < Repo.NumSamples; i++)
                        {
                            Repo.Time[i] = (double)i / SampleFrequency;

                            for (int j = 0; j < NumChannels; j++)
                            {
                                byte a = br.ReadByte();
                                Repo.WavDataInt[j, i] = a - 128; // 8-bit wav files use offset binary, not two's complement
                            }
                        }
                    }

                    // Handle 16-bit signed depth
                    else if (SampleDepth == 16)
                    {
                        Repo.WavDataInt = new int[NumChannels, Repo.NumSamples];

                        for (int i = 0; i < Repo.NumSamples; i++)
                        {
                            Repo.Time[i] = (double)i / SampleFrequency;

                            for (int j = 0; j < NumChannels; j++)
                            {
                                Repo.WavDataInt[j, i] = br.ReadInt16();
                            }
                        }
                    }

                    // Handle 24-bit signed depth
                    else if (SampleDepth == 24)
                    {
                        Repo.WavDataInt = new int[NumChannels, Repo.NumSamples];

                        for (int i = 0; i < Repo.NumSamples; i++)
                        {
                            Repo.Time[i] = (double)i / SampleFrequency;

                            for (int j = 0; j < NumChannels; j++)
                            {
                                byte a = br.ReadByte();
                                byte b = br.ReadByte();
                                byte c = br.ReadByte();
                                int d = 0;

                                if (c >= 128)
                                {
                                    c -= 128;
                                    d = -8388608;
                                }

                                int e = d + (c * 65536) + (b * 256) + a;

                                Repo.WavDataInt[j, i] = e;
                            }
                        }
                    }

                    // Handle 32-bit signed depth
                    else
                    {
                        Repo.WavDataInt = new int[NumChannels, Repo.NumSamples];

                        for (int i = 0; i < Repo.NumSamples; i++)
                        {
                            Repo.Time[i] = (double)i / SampleFrequency;

                            for (int j = 0; j < NumChannels; j++)
                            {
                                Repo.WavDataInt[j, i] = br.ReadInt32();
                            }
                        }
                    }

                    LoadWavStatus = "Valid .wav file format. File loaded.";
                    await Task.Delay(25);
                }
            }
        }
    }
}
