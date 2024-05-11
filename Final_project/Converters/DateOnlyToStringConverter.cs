using System.Globalization;
using System.Windows.Data;

public class DateOnlyToStringConverter : IValueConverter
{
    private const string DateFormat = "yyyy-MM-dd";

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime dateOnly)
        {
            return dateOnly.ToString(DateFormat);
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string dateStr && DateTime.TryParseExact(dateStr, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
        {
            return result;
        }

        return DateTime.MinValue;
    }
}
