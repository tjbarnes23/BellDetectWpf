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
