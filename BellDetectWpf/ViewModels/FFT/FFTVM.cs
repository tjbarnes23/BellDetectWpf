using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Repository;

namespace BellDetectWpf.ViewModels
{
    public partial class FFTVM : INotifyPropertyChanged
    {
        // Events
        public event PropertyChangedEventHandler PropertyChanged;

        // Observable properties
        public string WavFilePathName
        {
            get
            {
                return Repo.WavFilePathName;
            }

            set
            {
                if (Repo.WavFilePathName != value)
                {
                    Repo.WavFilePathName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public uint N // number of bins
        {
            get
            {
                return Repo.N;
            }
            set
            {
                if (Repo.N != value)
                {
                    Repo.N = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string FFTFilePathName
        {
            get
            {
                return Repo.FFTFilePathName;
            }

            set
            {
                if (Repo.FFTFilePathName != value)
                {
                    Repo.FFTFilePathName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string FFTStatus
        {
            get
            {
                return Repo.FFTStatus;
            }

            set
            {
                if (Repo.FFTStatus != value)
                {
                    Repo.FFTStatus = value;
                    NotifyPropertyChanged();
                }
            }
        }

        // Notification method
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
