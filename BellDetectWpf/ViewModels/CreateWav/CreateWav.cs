using System;
using System.IO;
using System.Threading.Tasks;
using BellDetectWpf.Repository;
using Microsoft.Win32;

namespace BellDetectWpf.ViewModels
{
    public partial class CreateWavVM
    {
        public async Task CreateWav()
        {
            double time;
            int freq;
            int amp;
            double peak;
            double decay;

            double x;
            double w;
            double scale;
            double wSum;

            /**************************************************
            * Update status
            **************************************************/

            CreateWavStatus = "Creating .wav file...";
            await Task.Delay(25);


            /**************************************************
            * Initialize variables
            **************************************************/

            Repo.SampleFrequency = 48000;
            Repo.SampleDepth = 16;
            Repo.WavNumChannels = 1;

            Repo.LengthSeconds = 5.0;
            
            Repo.NumSamples = (uint)(Repo.SampleFrequency * Repo.LengthSeconds);

            Repo.WavDataInt = new int[Repo.WavNumChannels, Repo.NumSamples];
            

            /**************************************************
            * Create waves (zero based)
            **************************************************/

            for (int i = 0; i < Repo.NumSamples; i++)
            {
                time = (double)i / Repo.SampleFrequency;
                wSum = 0.0;

                for (int j = 0; j < Repo.WavSpecs.Count; j++)
                {
                    freq = Repo.WavSpecs[j].Frequency;
                    amp = Repo.WavSpecs[j].Amplitude;
                    peak = Repo.WavSpecs[j].TimeToPeak;
                    decay = Repo.WavSpecs[j].TimeToDecayTo50pc;

                    if (freq != 0)
                    {
                        x = time * freq * 2.0 * Math.PI;
                        w = Math.Sin(x) * amp;

                        // At this point, w is the max amplitude of the wave at all points.
                        // Now scale w by the bell envelope function
                        if (i == 0)
                        {
                            scale = 0;
                        }
                        else
                        {
                            if (time <= peak)
                            {
                                scale = (peak / time) * Math.Pow(Math.E, 1 - (peak / time));
                            }
                            else
                            {
                                scale = Math.Pow(0.5, (time - peak) / decay);
                            }
                        }

                        w *= scale;

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

                Repo.WavDataInt[0, i] = (int)Math.Round(wSum);
            }

            /**************************************************
            * Update status
            **************************************************/

            CreateWavStatus = ".wav file created";
            await Task.Delay(25);
        }
    }
}
