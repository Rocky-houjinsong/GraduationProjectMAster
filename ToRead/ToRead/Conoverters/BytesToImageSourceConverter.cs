using System.Globalization;
using ToRead.Library.Misc;

namespace ToRead.Conoverters;

/// <summary>
/// 字节转图片值转换器.
/// </summary>
/// <remarks>无需修改.</remarks>
public class BytesToImageSourceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter,
        CultureInfo culture) =>
        value is byte[] bytes
            ? ImageSource.FromStream(() => new MemoryStream(bytes))
            : null;

    public object ConvertBack(object value, Type targetType, object parameter,
        CultureInfo culture) =>
        throw new DoNotCallThisException();
}