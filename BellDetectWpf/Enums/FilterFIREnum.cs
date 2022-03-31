using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BellDetectWpf.Enums
{
    public enum FilterFIREnum
    {
        pm_b9_1,
        pm_c15_1,
        pm_c15_2
    }

    public static class FilterFIRExt
    {
        public static Dictionary<FilterFIREnum, string> FilterFIRDict { get; } = new Dictionary<FilterFIREnum, string>()
        {
            {FilterFIREnum.pm_b9_1, "B9; 1048-1098-1335-1385 Hz; 592 order; 44.1 kHz; -30/-5 dB"},
            {FilterFIREnum.pm_c15_1, "C15; 396.75-514-534-651.25 Hz; 275 order; 48 kHz; -30/-5 dB"},
            {FilterFIREnum.pm_c15_2, "C15; 512-520-524-532 Hz; 8072 order; 48 kHz; -50/-3 dB"}
        };
    }

    public class FilterFIRConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            FilterFIREnum k = (FilterFIREnum)value;
            string v = FilterFIRExt.FilterFIRDict[k];
            return v;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
