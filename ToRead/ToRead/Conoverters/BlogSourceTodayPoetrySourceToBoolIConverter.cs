using System.Globalization;
using ToRead.Library.Misc;

namespace ToRead.Conoverters;

class TodayBlogSourceToBoolIConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter,
        CultureInfo culture)
    {
        return value is string source && parameter is string expectedSource &&
               source == expectedSource;
    }

    public object ConvertBack(object value, Type targetType, object parameter,
        CultureInfo culture) =>
        throw new DoNotCallThisException();
}