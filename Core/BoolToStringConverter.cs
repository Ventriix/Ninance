using System;
using System.Globalization;
using System.Windows.Data;

namespace Ninance_v2.Core
{

    [ValueConversion(typeof(bool), typeof(bool))]
    public class BoolToStringConverter : IValueConverter
    {

        public string ValueIfTrue { get; set; }
        public string ValueIfFalse { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool && (bool) value ? ValueIfTrue : ValueIfFalse;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && value.ToString() == ValueIfTrue;
        }
    }
}
