using System.Globalization;
using ToRead.Library.Misc;
using ToRead.Library.Models;

namespace ToRead.Conoverters;

internal class ToReadtoStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter,
        CultureInfo culture) =>
        !(value is ToReadItem poetry)
            ? null
            : $"{poetry.Dynasty} · {poetry.Author}    {poetry.Snippet}";

    public object ConvertBack(object value, Type targetType, object parameter,
        CultureInfo culture)
    {
        throw new DoNotCallThisException();
    }
}