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
    public partial class LoadWavSpecVM : INotifyPropertyChanged
    {
        // Events
        public event PropertyChangedEventHandler PropertyChanged;

        // Observable properties
        public string WavSpecFilePathName
        {
            get
            {
                return Repo.WavSpecFilePathName;
            }

            set
            {
                if (Repo.WavSpecFilePathName != value)
                {
                    Repo.WavSpecFilePathName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string LoadWavSpecStatus
        {
            get
            {
                return Repo.LoadWavSpecStatus;
            }

            set
            {
                if (Repo.LoadWavSpecStatus != value)
                {
                    Repo.LoadWavSpecStatus = value;
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
