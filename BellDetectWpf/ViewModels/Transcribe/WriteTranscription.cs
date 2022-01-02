using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Enums;
using BellDetectWpf.Models;
using BellDetectWpf.ViewModels.Shared;

namespace BellDetectWpf.ViewModels.Transcribe
{
    public static partial class C_Transcribe
    { 
        public static async Task WriteTranscription()
        {
            StringBuilder sb;
            int numBells;
            byte[] row;

            await C_Shared.Status("Saving transcription...", "black", 10, false);

            // Check collection isn't null
            if (TranscribeVM.TranscriptionArr == null)
            {
                await C_Shared.Status("Grid is empty. Transcription not saved", "red", 7000, true);
                return;
            }

            if (File.Exists(TranscribeVM.FilePathName))
            {
                File.Delete(TranscribeVM.FilePathName);
            }

            // Create a new file     
            using FileStream fs = File.Create(TranscribeVM.FilePathName);

            sb = new StringBuilder();
            numBells = StageEnumExt.StageBells(TranscribeVM.Stage);

            foreach (Row transcriptionRow in TranscribeVM.TranscriptionArr)
            {
                sb.Clear();

                for (int i = 0; i < numBells; i++)
                {
                    sb.Append(transcriptionRow[i]);
                    sb.Append('\t');
                }

                sb.Append('\n');

                row = new UTF8Encoding(true).GetBytes(sb.ToString());
                fs.Write(row, 0, row.Length);
            }

            await C_Shared.Status("Transcription saved", "black", 3000, true);
        }
    }
}
