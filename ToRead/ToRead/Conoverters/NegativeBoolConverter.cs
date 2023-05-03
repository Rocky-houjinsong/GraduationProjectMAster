using System.Globalization;
using ToRead.Library.Misc;

namespace ToRead.Conoverters;

/// <summary>
/// 导航布尔转换器。
/// </summary>
public class NegativeBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter,
        CultureInfo culture)
    {
        if (value is bool b)
        {
            return !b;
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter,
        CultureInfo culture) =>
        throw new DoNotCallThisException();
}