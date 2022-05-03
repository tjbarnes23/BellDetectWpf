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
