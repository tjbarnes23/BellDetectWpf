using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellDetectWpf.Models
{
    public class SavedSettings
    {
        public string SavedWavInitialDirectory { get; set; }

        public string SavedWavSpecInitialDirectory { get; set; }

        public string SavedFFTInitialDirectory { get; set; }

        public string SavedDFTInitialDirectory { get; set; }

        public string SavedButterworthInitialDirectory { get; set; }

        public string SavedEllipticInitialDirectory { get; set; }

        public string SavedFIRInitialDirectory { get; set; }
    }
}
