using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Enums;
using BellDetectWpf.Repository;

namespace BellDetectWpf.ViewModels
{
    public partial class DetectionVM : INotifyPropertyChanged
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

        public FilterTypeEnum FilterType
        {
            get
            {
                return Repo.FilterType;
            }
            set
            {
                if (Repo.FilterType != value)
                {
                    Repo.FilterType = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double LeftLowLow
        {
            get
            {
                return Repo.LeftLowLow;
            }
            set
            {
                if (Repo.LeftLowLow != value)
                {
                    Repo.LeftLowLow = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double LeftLowHigh
        {
            get
            {
                return Repo.LeftLowHigh;
            }
            set
            {
                if (Repo.LeftLowHigh != value)
                {
                    Repo.LeftLowHigh = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double LeftMid
        {
            get
            {
                return Repo.LeftMid;
            }
            set
            {
                if (Repo.LeftMid != value)
                {
                    Repo.LeftMid = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double LeftHighLow
        {
            get
            {
                return Repo.LeftHighLow;
            }
            set
            {
                if (Repo.LeftHighLow != value)
                {
                    Repo.LeftHighLow = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double LeftHighHigh
        {
            get
            {
                return Repo.LeftHighHigh;
            }
            set
            {
                if (Repo.LeftHighHigh != value)
                {
                    Repo.LeftHighHigh = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double RightLowLow
        {
            get
            {
                return Repo.RightLowLow;
            }
            set
            {
                if (Repo.RightLowLow != value)
                {
                    Repo.RightLowLow = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double RightLowHigh
        {
            get
            {
                return Repo.RightLowHigh;
            }
            set
            {
                if (Repo.RightLowHigh != value)
                {
                    Repo.RightLowHigh = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double RightMid
        {
            get
            {
                return Repo.RightMid;
            }
            set
            {
                if (Repo.RightMid != value)
                {
                    Repo.RightMid = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double RightHighLow
        {
            get
            {
                return Repo.RightHighLow;
            }
            set
            {
                if (Repo.RightHighLow != value)
                {
                    Repo.RightHighLow = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double RightHighHigh
        {
            get
            {
                return Repo.RightHighHigh;
            }
            set
            {
                if (Repo.RightHighHigh != value)
                {
                    Repo.RightHighHigh = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int MinAmplitudeValue
        {
            get
            {
                return Repo.MinAmplitudeValue;
            }
            set
            {
                if (Repo.MinAmplitudeValue != value)
                {
                    Repo.MinAmplitudeValue = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int MinAmplitudeCycles
        {
            get
            {
                return Repo.MinAmplitudeCycles;
            }
            set
            {
                if (Repo.MinAmplitudeCycles != value)
                {
                    Repo.MinAmplitudeCycles = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int MinAmplitudeIncreasePC
        {
            get
            {
                return Repo.MinAmplitudeIncreasePC;
            }
            set
            {
                if (Repo.MinAmplitudeIncreasePC != value)
                {
                    Repo.MinAmplitudeIncreasePC = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int MinAmplitudeIncreaseTS
        {
            get
            {
                return Repo.MinAmplitudeIncreaseTS;
            }
            set
            {
                if (Repo.MinAmplitudeIncreaseTS != value)
                {
                    Repo.MinAmplitudeIncreaseTS = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string DetectionFilePathName
        {
            get
            {
                return Repo.DetectionFilePathName;
            }

            set
            {
                if (Repo.DetectionFilePathName != value)
                {
                    Repo.DetectionFilePathName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string DetectionStatus
        {
            get
            {
                return Repo.DetectionStatus;
            }

            set
            {
                if (Repo.DetectionStatus != value)
                {
                    Repo.DetectionStatus = value;
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
