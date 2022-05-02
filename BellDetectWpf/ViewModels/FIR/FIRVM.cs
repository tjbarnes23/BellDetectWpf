﻿using System;
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
    public partial class FIRVM : INotifyPropertyChanged
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

        public FilterFIREnum FilterFIRLeft
        {
            get
            {
                return Repo.FilterFIRLeft;
            }
            set
            {
                if (Repo.FilterFIRLeft != value)
                {
                    Repo.FilterFIRLeft = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public FilterFIREnum FilterFIRRight
        {
            get
            {
                return Repo.FilterFIRRight;
            }
            set
            {
                if (Repo.FilterFIRRight != value)
                {
                    Repo.FilterFIRRight = value;
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

        public int AmplitudeCutoff
        {
            get
            {
                return Repo.AmplitudeCutoff;
            }
            set
            {
                if (Repo.AmplitudeCutoff != value)
                {
                    Repo.AmplitudeCutoff = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double Gain
        {
            get
            {
                return Repo.Gain;
            }
            set
            {
                if (Repo.Gain != value)
                {
                    Repo.Gain = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string FIRFilePathName
        {
            get
            {
                return Repo.FIRFilePathName;
            }

            set
            {
                if (Repo.FIRFilePathName != value)
                {
                    Repo.FIRFilePathName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string FIRStatus
        {
            get
            {
                return Repo.FIRStatus;
            }

            set
            {
                if (Repo.FIRStatus != value)
                {
                    Repo.FIRStatus = value;
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
