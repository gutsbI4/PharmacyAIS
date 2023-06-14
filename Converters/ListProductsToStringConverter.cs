using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;
using PharmacyAIS.Models;

namespace UP02.Converters;

public class ListProductsToStringConverter:IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        IList<ProductOrder> products = value as IList<ProductOrder>;
        return products == null ? null : String.Join(", ",products.Select(a=>$"{a.Product.Name} {a.Quantity} шт."));
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}