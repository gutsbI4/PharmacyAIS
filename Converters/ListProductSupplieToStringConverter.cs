using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;
using PharmacyAIS.Models;

namespace PharmacyAIS.Converters;

public class ListProductSupplieToStringConverter:IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        IList<SupplieProduct> products = value as IList<SupplieProduct>;
        return products == null ? null : String.Join(", ", products.Select(a => $"{a.Product.Name} {a.Quantity} шт. за {a.Price} руб."));
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}