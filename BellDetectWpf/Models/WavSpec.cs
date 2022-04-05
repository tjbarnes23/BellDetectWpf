using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BellDetectWpf.Models
{
    public class WavSpec : INotifyPropertyChanged
    {
        // Private backing fields
        int frequency;
        int amplitude;
        double timeToPeak;
        double timeToDecayTo50pc;

        // Events
        public event PropertyChangedEventHandler PropertyChanged;

        // Observable properties
        public int Frequency
        {
            get
            {
                return frequency;
            }
            
            set
            {
                if (frequency != value)
                {
                    frequency = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Amplitude
        {
            get
            {
                return amplitude;
            }

            set
            {
                if (amplitude != value)
                {
                    amplitude = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double TimeToPeak
        {
            get
            {
                return timeToPeak;
            }

            set
            {
                if (timeToPeak != value)
                {
                    timeToPeak = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double TimeToDecayTo50pc
        {
            get
            {
                return timeToDecayTo50pc;
            }

            set
            {
                if (timeToDecayTo50pc != value)
                {
                    timeToDecayTo50pc = value;
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
