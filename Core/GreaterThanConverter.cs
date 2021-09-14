using System;
using System.Globalization;
using System.Windows.Data;

namespace Ninance_v2.Core
{
    public class GreaterThanConverter : IValueConverter
    {
        public int GreaterThanValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((int) value) > GreaterThanValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
