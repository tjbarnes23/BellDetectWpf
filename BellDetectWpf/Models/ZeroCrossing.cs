using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Enums;

namespace BellDetectWpf.Models
{
    public class ZeroCrossing
    {
        public int SampleNum { get; set; }

        public CrossingTypeEnum CrossingType { get; set; }

        public int SampleInterval { get; set; }
    }
}
