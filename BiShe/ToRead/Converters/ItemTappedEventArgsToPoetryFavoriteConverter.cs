using ToRead.Misc;
using ToRead.ViewModels;
using System.Globalization;

namespace ToRead.Converters;

public class ItemTappedEventArgsToPoetryFavoriteConverter : IValueConverter
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