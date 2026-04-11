using System.Globalization;

namespace PositionSizeCalculator.Helpers.Converters
{
    public class ZeroToEmptyStringConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int intValue && intValue == 0)
            {
                return string.Empty;
            }  
            if (value is double doubleValue && doubleValue == 0.0)
            {
                return string.Empty;
            }
            if (value is decimal decimalValue && decimalValue == 0.0m)
            {
                return string.Empty;
            }
            return value?.ToString() ?? string.Empty;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(value as string))
            {
                return 0;
            }  
            if (targetType == typeof(int) && int.TryParse(value as string, out var intValue))
            {
                return intValue;
            }
            if (targetType == typeof(double) && double.TryParse(value as string, out var doubleValue))
            {
                return doubleValue;
            }
            if (targetType == typeof(decimal) && decimal.TryParse(value as string, out var decimalValue))
            {
                return decimalValue;
            }
            return 0;
        }
    }
}
