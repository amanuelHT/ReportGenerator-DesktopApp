using System.Globalization;
using System.Windows.Data;

public class DateOnlyToStringConverter : IValueConverter
{
    private const string DateFormat = "yyyy-MM-dd";

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateOnly dateOnly)
        {
            return dateOnly.ToString(DateFormat);
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string dateStr && DateOnly.TryParseExact(dateStr, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly result))
        {
            return result;
        }

        return DateOnly.MinValue;
    }
}
