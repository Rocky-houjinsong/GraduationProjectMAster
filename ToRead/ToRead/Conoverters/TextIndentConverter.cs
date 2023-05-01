using System.Globalization;
using ToRead.Library.Misc;

namespace ToRead.Conoverters;

public class TextIndentConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter,
        CultureInfo culture) =>
        (value as string)?.Insert(0, "\u3000\u3000")
        .Replace("\n", "\n\u3000\u3000");

    public object ConvertBack(object value, Type targetType, object parameter,
        CultureInfo culture)
    {
        throw new DoNotCallThisException();
    }
}