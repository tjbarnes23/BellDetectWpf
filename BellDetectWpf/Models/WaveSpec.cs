using System.ComponentModel;

namespace BellDetectWpf.Models
{
    public class WaveSpec : INotifyPropertyChanged
    {
        private int frequency;
        private int amplitude;
        private double timeToPeak;
        private double timeToDecayTo50pc;

        public event PropertyChangedEventHandler PropertyChanged;

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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Frequency)));
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Amplitude)));
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TimeToPeak)));
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TimeToDecayTo50pc)));
                }
            }
        }
    }
}
