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
        public static void SaveSettings()
        {
            SavedSettings ss = new()
            {
                SavedWavInitialDirectory = Repo.WavInitialDirectory,
                SavedWavSpecInitialDirectory = Repo.WavSpecInitialDirectory,
                SavedFFTInitialDirectory = Repo.FFTInitialDirectory,
                SavedDFTInitialDirectory = Repo.DFTInitialDirectory,
                SavedButterworthInitialDirectory = Repo.ButterworthInitialDirectory,
                SavedEllipticInitialDirectory = Repo.EllipticInitialDirectory,
                SavedFIRInitialDirectory = Repo.FIRInitialDirectory,
                SavedDetectionInitialDirectory = Repo.DetectionInitialDirectory
            };

            JsonSerializerOptions options = new()
            {
                WriteIndented = true
            };

            // Write to json file
            string filePathName = @"C:\ProgramData\BellDetect\Settings\settings.json";

            string jsonStr = JsonSerializer.Serialize(ss, options);

            File.WriteAllText(filePathName, jsonStr);
        }
    }
}
