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
    public partial class ButterworthVM : INotifyPropertyChanged
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

        public FilterButterworthEnum FilterButterworth
        {
            get
            {
                return Repo.FilterButterworth;
            }
            set
            {
                if (Repo.FilterButterworth != value)
                {
                    Repo.FilterButterworth = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string ButterworthFilePathName
        {
            get
            {
                return Repo.ButterworthFilePathName;
            }

            set
            {
                if (Repo.ButterworthFilePathName != value)
                {
                    Repo.ButterworthFilePathName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string ButterworthStatus
        {
            get
            {
                return Repo.ButterworthStatus;
            }

            set
            {
                if (Repo.ButterworthStatus != value)
                {
                    Repo.ButterworthStatus = value;
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
