using System.Globalization;
using ToRead.Library.Misc;

namespace ToRead.Conoverters;

/// <summary>
/// 推荐博客源布尔转换器
/// </summary>
/// <remarks>无需修改.</remarks>
class RecommendBlogSourceToBoolIConverter : IValueConverter
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