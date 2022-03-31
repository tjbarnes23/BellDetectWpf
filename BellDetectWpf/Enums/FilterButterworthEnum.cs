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
        TestButterworth1,
        TestButterworth2
    }

    public static class FilterButterworthExt
    {
        public static Dictionary<FilterButterworthEnum, string> FilterButterworthDict { get; } = new Dictionary<FilterButterworthEnum, string>()
        {
            {FilterButterworthEnum.TestButterworth1, "Test Butterworth 1"},
            {FilterButterworthEnum.TestButterworth2, "Test Butterworth 2"}
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
