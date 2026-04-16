using System.Globalization;

namespace PositionSizeCalculator.Helpers.Converters
{
    public class EntryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal decimalValue)
            {
                return decimalValue == 0 ? string.Empty : decimalValue.ToString(CultureInfo.InvariantCulture);
            }
            if (value is double doubleValue)
            {
                return doubleValue == 0 ? string.Empty : doubleValue.ToString(CultureInfo.InvariantCulture);
            }
            if (value is int intValue)
            {
                return intValue == 0 ? string.Empty : intValue.ToString(CultureInfo.InvariantCulture);
            }
            return value?.ToString() ?? string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string text)
            {
                if (string.IsNullOrWhiteSpace(text))
                {
                    return 0;
                }

                string normalizedText = text.Replace(',', '.');

                if (targetType == typeof(int) && int.TryParse(normalizedText, NumberStyles.Any, CultureInfo.InvariantCulture, out var intValue))
                {
                    return intValue;
                }
                if (targetType == typeof(double) && double.TryParse(normalizedText, NumberStyles.Any, CultureInfo.InvariantCulture, out var doubleValue))
                {
                    return doubleValue;
                }
                if (targetType == typeof(decimal) && decimal.TryParse(normalizedText, NumberStyles.Any, CultureInfo.InvariantCulture, out var decimalValue))
                {
                    return decimalValue;
                }
            }
            return 0;
        }
    }
}
