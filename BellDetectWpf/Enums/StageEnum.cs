using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BellDetectWpf.Enums
{
    public enum StageEnum
    {
        Six,
        Eight,
        Ten,
        Twelve
    }

    public static class StageEnumExt
    {
        public static Dictionary<StageEnum, string> StageEnumDict { get; } = new Dictionary<StageEnum, string>()
        {
            {StageEnum.Six, "6"},
            {StageEnum.Eight, "8"},
            {StageEnum.Ten, "10"},
            {StageEnum.Twelve, "12"}
        };

        public static int StageBells(StageEnum stage)
        {
            return stage switch
            {
                StageEnum.Six => 6,
                StageEnum.Eight => 8,
                StageEnum.Ten => 10,
                StageEnum.Twelve => 12,
                _ => 6
            };
        }
    }

    public class BellSetEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            StageEnum k = (StageEnum)value;
            string v = StageEnumExt.StageEnumDict[k];
            return v;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
