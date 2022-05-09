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
        pm_C8,
        pm_B9,
        pm_D14,
        pm_C15
    }

    public static class FilterFIRExt
    {
        public static Dictionary<FilterFIREnum, string> FilterFIRDict { get; } = new Dictionary<FilterFIREnum, string>()
        {
            {FilterFIREnum.pm_C8, "PM C8; 1002/1040/1060/1189 Hz; -40/-3/-40 dB; 1366 order; 48 kHz"},
            {FilterFIREnum.pm_B9, "PM B9; 891/982/1002/1040 Hz; -40/-3/-40 dB; 1366 order; 48 kHz"},
            {FilterFIREnum.pm_D14, "PM D14; 532/581/601/647 Hz; -40/-3/-40 dB; 1129 order; 48 kHz"},
            {FilterFIREnum.pm_C15, "PM C15; 463/512/532/581 Hz; -40/-3/-40 dB; 1060 order; 48 kHz"}
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
