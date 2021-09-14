using System;
using System.Globalization;
using System.Windows.Data;

namespace Ninance_v2.Core
{
    public class UnixToDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            if (value is long miilis)
                dateTime = dateTime.AddMilliseconds(miilis).ToLocalTime();

            return dateTime;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
                return new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();

            return null;
        }
    }
}
