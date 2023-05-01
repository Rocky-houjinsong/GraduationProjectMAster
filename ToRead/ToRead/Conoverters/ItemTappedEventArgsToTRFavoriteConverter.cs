﻿using System.Globalization;
using ToRead.Library.Misc;

namespace ToRead.Conoverters;

internal class ItemTappedEventArgsToTRFavoriteConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter,
        CultureInfo culture) =>
        (value as ItemTappedEventArgs)?.Item as PoetryFavorite;

    public object ConvertBack(object value, Type targetType, object parameter,
        CultureInfo culture)
    {
        throw new DoNotCallThisException();
    }
}