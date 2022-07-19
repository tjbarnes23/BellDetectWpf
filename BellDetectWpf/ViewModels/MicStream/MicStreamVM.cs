using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BellDetectWpf.Repository;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace BellDetectWpf.ViewModels
{
    public partial class MicStreamVM : INotifyPropertyChanged
    {
        // Events
        public event PropertyChangedEventHandler PropertyChanged;

        // Observable properties
        public string MicStreamStartStopTxt
        {
            get
            {
                return Repo.MicStreamStartStopTxt;
            }

            set
            {
                if (Repo.MicStreamStartStopTxt != value)
                {
                    Repo.MicStreamStartStopTxt = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string MicStreamStatus
        {
            get
            {
                return Repo.MicStreamStatus;
            }

            set
            {
                if (Repo.MicStreamStatus != value)
                {
                    Repo.MicStreamStatus = value;
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
