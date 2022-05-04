using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BellDetectWpf.Models;
using BellDetectWpf.Repository;

namespace BellDetectWpf.Utilities
{
    public static partial class Utils
    {
        public static void LoadSettings()
        {
            SavedSettings ss;

            // Read from json file
            string filePathName = @"C:\ProgramData\BellDetect\Settings\settings.json";

            string jsonStr = File.ReadAllText(filePathName);

            ss = JsonSerializer.Deserialize<SavedSettings>(jsonStr);

            Repo.WavInitialDirectory = ss.SavedWavInitialDirectory;
            Repo.WavSpecInitialDirectory = ss.SavedWavSpecInitialDirectory;
            Repo.FFTInitialDirectory = ss.SavedFFTInitialDirectory;
            Repo.DFTInitialDirectory = ss.SavedDFTInitialDirectory;
            Repo.ButterworthInitialDirectory = ss.SavedButterworthInitialDirectory;
            Repo.EllipticInitialDirectory = ss.SavedEllipticInitialDirectory;
            Repo.FIRInitialDirectory = ss.SavedFIRInitialDirectory;
            Repo.DetectionInitialDirectory = ss.SavedDetectionInitialDirectory;
        }
    }
}
