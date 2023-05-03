using System.Globalization;
using ToRead.Misc;
using ToRead.Models;

namespace ToRead.Converters;

public class PoetryToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter,
        CultureInfo culture) =>
        !(value is Poetry poetry)
            ? null
            : $"{poetry.Dynasty} · {poetry.Author}    {poetry.Snippet}";

    public object ConvertBack(object value, Type targetType, object parameter,
        CultureInfo culture)
    {
        throw new DoNotCallThisException();
    }
}