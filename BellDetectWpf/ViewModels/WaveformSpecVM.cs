using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Models;
using Microsoft.Win32;

namespace BellDetectWpf.ViewModels
{
    public static class WaveformSpecVM
    {
        private static string filePathName;
        private static string message;

        public static event EventHandler FilePathNameChanged;
        public static event EventHandler MessageChanged;

        public static ObservableCollection<WaveSpec> WaveformSpecArr { get; set; }

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

        public static string Message
        {
            get
            {
                return message;
            }
            set
            {
                if (message != value)
                {
                    message = value;
                    MessageChanged?.Invoke(null, EventArgs.Empty);
                }
            }
        }
    }
}
