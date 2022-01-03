using System.ComponentModel;

namespace BellDetectWpf.Models
{
    public class DetectionSpec : INotifyPropertyChanged
    {
        private char bell;
        private int frequencyBin;
        private int amplitudeLow;
        private int amplitudeHigh;
        private int minTimeBetweenDetectionsMs;
        private char key;

        public event PropertyChangedEventHandler PropertyChanged;

        public char Bell
        {
            get
            {
                return bell;
            }
            set
            {
                if (bell != value)
                {
                    bell = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Bell)));
                }
            }
        }

        public int FrequencyBin
        {
            get
            {
                return frequencyBin;
            }
            set
            {
                if (frequencyBin != value)
                {
                    frequencyBin = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FrequencyBin)));
                }
            }
        }

        public int AmplitudeLow
        {
            get
            {
                return amplitudeLow;
            }
            set
            {
                if (amplitudeLow != value)
                {
                    amplitudeLow = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AmplitudeLow)));
                }
            }

        }

        public int AmplitudeHigh
        {
            get
            {
                return amplitudeHigh;
            }
            set
            {
                if (amplitudeHigh != value)
                {
                    amplitudeHigh = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AmplitudeHigh)));
                }
            }

        }

        public int MinTimeBetweenDetectionsMs
        {
            get
            {
                return minTimeBetweenDetectionsMs;
            }
            set
            {
                if (minTimeBetweenDetectionsMs != value)
                {
                    minTimeBetweenDetectionsMs = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MinTimeBetweenDetectionsMs)));
                }
            }
        }

        public char Key
        {
            get
            {
                return key;
            }
            set
            {
                if (key != value)
                {
                    key = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Key)));
                }
            }
        }
    }
}
