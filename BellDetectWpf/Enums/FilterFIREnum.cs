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
        pm_C8_1,
        pm_C8_2,
        pm_C8_3,
        pm_B9_1,
        pm_B9_2,
        pm_B9_3,
        pm_D14_1,
        pm_D14_2,
        pm_D14_3,
        pm_C15_1,
        pm_C15_2,
        pm_C15_3
    }

    public static class FilterFIRExt
    {
        public static Dictionary<FilterFIREnum, string> FilterFIRDict { get; } = new Dictionary<FilterFIREnum, string>()
        {
            {FilterFIREnum.pm_C8_1, "PM C8; 923/1040/1060/1177 Hz; -30/-5/-30 dB; 276 order; 48 kHz"},
            {FilterFIREnum.pm_C8_2, "PM C8; 1002/1040/1060/1189 Hz; -40/-3/-40 dB; 1366 order; 48 kHz"},
            {FilterFIREnum.pm_C8_3, "PM C8; 923/1040/1060/1177 Hz; -60/-3/-60 dB; 661 order; 48 kHz"},
            {FilterFIREnum.pm_B9_1, "PM B9; 865/982/1002/1119 Hz; -30/-5/-30 dB; 276 order; 48 kHz"},
            {FilterFIREnum.pm_B9_2, "PM B9; 891/982/1002/1040 Hz; -40/-3/-40 dB; 1366 order; 48 kHz"},
            {FilterFIREnum.pm_B9_3, "PM B9; 865/982/1002/1119 Hz; -60/-3/-60 dB; 661 order; 48 kHz"},
            {FilterFIREnum.pm_D14_1, "PM D14; 464/581/601/718 Hz; -30/-5/-30 dB; 276 order; 48 kHz"},
            {FilterFIREnum.pm_D14_2, "PM D14; 532/581/601/647 Hz; -40/-3/-40 dB; 1129 order; 48 kHz"},
            {FilterFIREnum.pm_D14_3, "PM D14; 464/581/601/718 Hz; -60/-5/-60 dB; 661 order; 48 kHz"},
            {FilterFIREnum.pm_C15_1, "PM C15; 395/512/532/650 Hz; -30/-5/-30 dB; 276 order; 48 kHz"},
            {FilterFIREnum.pm_C15_2, "PM C15; 463/512/532/581 Hz; -40/-3/-40 dB; 1060 order; 48 kHz"},
            {FilterFIREnum.pm_C15_3, "PM C15; 395/512/532/650 Hz; -60/-5/-60 dB; 661 order; 48 kHz"}
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
