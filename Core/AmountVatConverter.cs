using System;
using System.Globalization;
using System.Windows.Data;

namespace Ninance_v2.Core
{

    [ValueConversion(typeof(double), typeof(double))]
    public class AmountVatConverter : IValueConverter
    {

        public string SubtractOrAdd { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double amount;
            double vat;

            if(double.TryParse(parameter.ToString(), out vat) && double.TryParse(value.ToString(), out amount))
            {
                if(SubtractOrAdd.Equals("Subtract"))
                    return amount - (amount * (vat / 100));
                else
                    return amount + (amount * (vat / 100));
            }

            return 0.00;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0.00;
        }
    }
}
