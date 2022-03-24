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
    public partial class DFTVM : INotifyPropertyChanged
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

        public string DFTFilePathName
        {
            get
            {
                return Repo.DFTFilePathName;
            }

            set
            {
                if (Repo.DFTFilePathName != value)
                {
                    Repo.DFTFilePathName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string DFTStatus
        {
            get
            {
                return Repo.DFTStatus;
            }

            set
            {
                if (Repo.DFTStatus != value)
                {
                    Repo.DFTStatus = value;
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
