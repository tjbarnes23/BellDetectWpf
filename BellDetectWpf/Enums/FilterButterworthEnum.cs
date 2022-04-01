using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BellDetectWpf.Enums
{
    public enum FilterButterworthEnum
    {
        pm_C15_1
    }

    public static class FilterButterworthExt
    {
        public static Dictionary<FilterButterworthEnum, string> FilterButterworthDict { get; } = new Dictionary<FilterButterworthEnum, string>()
        {
            {FilterButterworthEnum.pm_C15_1, "PM C15; 495-515-530-550 Hz; 12th order; 48 kHz; -60/-5 dB"}
        };
    }

    public class FilterButterworthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            FilterButterworthEnum k = (FilterButterworthEnum)value;
            string v = FilterButterworthExt.FilterButterworthDict[k];
            return v;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
