using Avalonia.Data;
using Avalonia.Data.Converters;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAIS.Converters
{
    public class StringToDecimalConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            decimal result = 0m;
            value =value.ToString().Replace(',', '.');
            if (value is decimal decimalValue)
            {
                result = decimalValue;
            }
            else if (value is string stringValue)
            {
                decimal.TryParse(stringValue, NumberStyles.Any, culture, out result);
            }

            return result.ToString();
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            decimal result = 0m;
            value = value.ToString().Replace(',', '.');
            if (value is string stringValue)
            {
                decimal.TryParse(stringValue, NumberStyles.Any, culture, out result);
            }

            return result;
        }
    }
}
