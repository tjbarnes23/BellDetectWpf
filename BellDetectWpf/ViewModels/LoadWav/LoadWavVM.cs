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
        public string FilePathName
        {
            get
            {
                return Repo.FilePathName;
            }

            set
            {
                if (Repo.FilePathName != value)
                {
                    Repo.FilePathName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Status
        {
            get
            {
                return Repo.Status;
            }

            set
            {
                if (Repo.Status != value)
                {
                    Repo.Status = value;
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

        public ushort NumChannels
        {
            get
            {
                return Repo.NumChannels;
            }
            set
            {
                if (Repo.NumChannels != value)
                {
                    Repo.NumChannels = value;
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
