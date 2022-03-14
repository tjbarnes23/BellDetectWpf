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
    public partial class LoadWavVM : INotifyPropertyChanged
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

        public string LoadWavStatus
        {
            get
            {
                return Repo.LoadWavStatus;
            }

            set
            {
                if (Repo.LoadWavStatus != value)
                {
                    Repo.LoadWavStatus = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public uint SampleFrequency
        {
            get
            {
                return Repo.SampleFrequency;
            }
            set
            {
                if (Repo.SampleFrequency != value)
                {
                    Repo.SampleFrequency = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ushort SampleDepth
        {
            get
            {
                return Repo.SampleDepth;
            }
            set
            {
                if (Repo.SampleDepth != value)
                {
                    Repo.SampleDepth = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ushort WavNumChannels
        {
            get
            {
                return Repo.WavNumChannels;
            }
            set
            {
                if (Repo.WavNumChannels != value)
                {
                    Repo.WavNumChannels = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ushort BlockAlignment
        {
            get
            {
                return Repo.BlockAlignment;
            }
            set
            {
                if (Repo.BlockAlignment != value)
                {
                    Repo.BlockAlignment = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public uint DataRate
        {
            get
            {
                return Repo.DataRate;
            }
            set
            {
                if (Repo.DataRate != value)
                {
                    Repo.DataRate = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public uint DataSize
        {
            get
            {
                return Repo.DataSize;
            }
            set
            {
                if (Repo.DataSize != value)
                {
                    Repo.DataSize = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double LengthSeconds
        {
            get
            {
                return Repo.LengthSeconds;
            }
            set
            {
                if (Repo.LengthSeconds != value)
                {
                    Repo.LengthSeconds = value;
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
