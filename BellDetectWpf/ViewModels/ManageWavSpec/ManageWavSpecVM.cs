using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Models;
using BellDetectWpf.Repository;

namespace BellDetectWpf.ViewModels
{
    public partial class ManageWavSpecVM : INotifyPropertyChanged
    {
        // Events
        public event PropertyChangedEventHandler PropertyChanged;

        // Observable collections
        public static ObservableCollection<WavSpec> WavSpecs
        {
            get
            {
                return Repo.WavSpecs;
            }

            set
            {
                if (Repo.WavSpecs != value)
                {
                    Repo.WavSpecs = value;
                }
            }
        }

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

        public string ManageWavSpecStatus
        {
            get
            {
                return Repo.ManageWavSpecStatus;
            }

            set
            {
                if (Repo.ManageWavSpecStatus != value)
                {
                    Repo.ManageWavSpecStatus = value;
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
