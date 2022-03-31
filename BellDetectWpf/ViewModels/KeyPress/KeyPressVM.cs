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
    public partial class KeyPressVM : INotifyPropertyChanged
    {
        // Events
        public event PropertyChangedEventHandler PropertyChanged;

        // Observable properties
        public string StartStopTxt
        {
            get
            {
                return Repo.StartStopTxt;
            }

            set
            {
                if (Repo.StartStopTxt != value)
                {
                    Repo.StartStopTxt = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string KeyPressStatus
        {
            get
            {
                return Repo.KeyPressStatus;
            }

            set
            {
                if (Repo.KeyPressStatus != value)
                {
                    Repo.KeyPressStatus = value;
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
