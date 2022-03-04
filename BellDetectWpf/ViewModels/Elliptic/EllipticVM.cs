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
    public partial class EllipticVM : INotifyPropertyChanged
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

        public string EllipticFilePathName
        {
            get
            {
                return Repo.EllipticFilePathName;
            }

            set
            {
                if (Repo.EllipticFilePathName != value)
                {
                    Repo.EllipticFilePathName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string EllipticStatus
        {
            get
            {
                return Repo.EllipticStatus;
            }

            set
            {
                if (Repo.EllipticStatus != value)
                {
                    Repo.EllipticStatus = value;
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
