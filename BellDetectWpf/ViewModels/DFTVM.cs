using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.ViewModels.DFT;

namespace BellDetectWpf.ViewModels
{
    public static class DFTVM
    {
        private static double startTime;
        private static string filePathName;

        public static event EventHandler StartTimeChanged;
        public static event EventHandler FilePathNameChanged;

        public static double[] CosDFT { get; set; }
        public static double[] SinDFT { get; set; }
        public static double[] Results { get; set; }

        public static double StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                if (startTime != value)
                {
                    startTime = value;
                    StartTimeChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static string FilePathName
        {
            get
            {
                return filePathName;
            }

            set
            {
                if (filePathName != value)
                {
                    filePathName = value;
                    FilePathNameChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }

        public static void RunDFT()
        {
            C_DFT.RunDFT();
            C_DFT.SaveDFT();
        }
    }
}
