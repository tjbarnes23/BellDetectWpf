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
        TestElliptic1,
        TestElliptic2
    }

    public static class FilterEllipticExt
    {
        public static Dictionary<FilterEllipticEnum, string> FilterEllipticDict { get; } = new Dictionary<FilterEllipticEnum, string>()
        {
            {FilterEllipticEnum.TestElliptic1, "Test Elliptic 1"},
            {FilterEllipticEnum.TestElliptic2, "Test Elliptic 2"}
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
