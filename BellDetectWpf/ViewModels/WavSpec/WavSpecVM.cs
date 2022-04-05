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
    public partial class WavSpecVM : INotifyPropertyChanged
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

        // Notification method
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
