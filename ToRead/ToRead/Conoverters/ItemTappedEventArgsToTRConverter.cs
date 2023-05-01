using System.Globalization;
using ToRead.Library.Misc;

namespace ToRead.Conoverters;

public class ItemTappedEventArgsToTRConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter,
        CultureInfo culture) =>
        (value as ItemTappedEventArgs)?.Item as Poetry;

    public object ConvertBack(object value, Type targetType, object parameter,
        CultureInfo culture)
    {
        throw new DoNotCallThisException();
    }
}