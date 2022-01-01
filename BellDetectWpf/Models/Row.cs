using System.ComponentModel;

namespace BellDetectWpf.Models
{
    public class Row : INotifyPropertyChanged
    {
        private char p1;
        private char p2;
        private char p3;
        private char p4;
        private char p5;
        private char p6;
        private char p7;
        private char p8;
        private char p9;
        private char p0;
        private char pE;
        private char pT;

        public event PropertyChangedEventHandler PropertyChanged;

        public char P1
        {
            get
            {
                return p1;
            }
            set
            {
                if (p1 != value)
                {
                    p1 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P1)));
                }
            }
        }

        public char P2
        {
            get
            {
                return p2;
            }
            set
            {
                if (p2 != value)
                {
                    p2 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P2)));
                }
            }
        }

        public char P3
        {
            get
            {
                return p3;
            }
            set
            {
                if (p3 != value)
                {
                    p3 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P3)));
                }
            }
        }

        public char P4
        {
            get
            {
                return p4;
            }
            set
            {
                if (p4 != value)
                {
                    p4 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P4)));
                }
            }
        }

        public char P5
        {
            get
            {
                return p5;
            }
            set
            {
                if (p5 != value)
                {
                    p5 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P5)));
                }
            }
        }

        public char P6
        {
            get
            {
                return p6;
            }
            set
            {
                if (p6 != value)
                {
                    p6 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P6)));
                }
            }
        }

        public char P7
        {
            get
            {
                return p7;
            }
            set
            {
                if (p7 != value)
                {
                    p7 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P7)));
                }
            }
        }

        public char P8
        {
            get
            {
                return p8;
            }
            set
            {
                if (p8 != value)
                {
                    p8 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P8)));
                }
            }
        }

        public char P9
        {
            get
            {
                return p9;
            }
            set
            {
                if (p9 != value)
                {
                    p9 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P9)));
                }
            }
        }

        public char P0
        {
            get
            {
                return p0;
            }
            set
            {
                if (p0 != value)
                {
                    p0 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(P0)));
                }
            }
        }

        public char PE
        {
            get
            {
                return pE;
            }
            set
            {
                if (pE != value)
                {
                    pE = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PE)));
                }
            }
        }

        public char PT
        {
            get
            {
                return pT;
            }
            set
            {
                if (pT != value)
                {
                    pT = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PT)));
                }
            }
        }
    }
}
