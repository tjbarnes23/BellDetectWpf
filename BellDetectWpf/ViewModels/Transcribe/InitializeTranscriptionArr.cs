using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Models;
using Microsoft.Win32;

namespace BellDetectWpf.ViewModels.Transcribe
{
    public static partial class C_Transcribe
    {
        public static void InitializeTranscriptionArr()
        {
            TranscribeVM.TranscriptionArr = new ObservableCollection<Row>();
        }
    }
}
