using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BellDetectWpf.Enums
{
    public enum FilterEllipticEnum
    {
        pm_B9_1,
        tjb_B2_1,
        tjb_As3_1,
        tjb_Gs4_1,
        tjb_Fs5_1,
        tjb_E6_1,
        tjb_Ds7_1,
        tjb_Cs8_1,
        tjb_B9_1
    }

    public static class FilterEllipticExt
    {
        public static Dictionary<FilterEllipticEnum, string> FilterEllipticDict { get; } = new Dictionary<FilterEllipticEnum, string>()
        {
            {FilterEllipticEnum.pm_B9_1, "PM B9: 1096-1136-1178-1218 Hz; 4th order; 44.1 kHz; -30/-5 dB"},
            {FilterEllipticEnum.tjb_B2_1,"TJB B2:1924-1962-2081-2163 Hz; 8th order; 44.1 kHz; -40/-3 dB"},
            {FilterEllipticEnum.tjb_As3_1,"TJB As3: 1749-1817-1924-1962 Hz; 8th order; 44.1 kHz; -40/-3 dB"},
            {FilterEllipticEnum.tjb_Gs4_1,"TJB Gs4: 1558-1619-1749-1817 Hz; 6th order; 44.1 kHz; -40/-3 dB"},
            {FilterEllipticEnum.tjb_Fs5_1,"TJB Fs5: 1388-1442-1558-1619 Hz; 6th order; 44.1 kHz; -40/-3 dB"},
            {FilterEllipticEnum.tjb_E6_1,"TJB E6: 1283-1308-1388-1442 Hz; 8th order; 44.1kHz; -40/-3 dB"},
            {FilterEllipticEnum.tjb_Ds7_1,"TJB Ds7: 1163-1211-1283-1308 Hz; 8th order; 44.1 kHz; -40/-3 dB"},
            {FilterEllipticEnum.tjb_Cs8_1,"TJB Cs8: 1032-1074-1163-1211 Hz; 6th order; 44.1 kHz; -40/-3 dB"},
            {FilterEllipticEnum.tjb_B9_1,"TJB B9: 953-971-1032-1074 Hz; 8th order; 44.1 kHz; -40/-3 dB"}
        };
    }

    public class FilterEllipticConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            FilterEllipticEnum k = (FilterEllipticEnum)value;
            string v = FilterEllipticExt.FilterEllipticDict[k];
            return v;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
