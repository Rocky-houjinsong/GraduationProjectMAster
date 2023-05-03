using System.Globalization;
using ToRead.Library.Misc;
using ToRead.Library.Models;

namespace ToRead.Conoverters;

/// <summary>
/// 点击项时引发的事件参数转换器.
/// </summary>
public class ItemTappedEventArgsToTRConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter,
        CultureInfo culture) =>
        (value as ItemTappedEventArgs)?.Item as ToReadItem;

    public object ConvertBack(object value, Type targetType, object parameter,
        CultureInfo culture)
    {
        throw new DoNotCallThisException();
    }
}