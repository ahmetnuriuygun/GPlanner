using System.Globalization;

namespace GPlanner.Maui.Converters
{
    public class BoolToTextConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isEdit && parameter is string param)
            {
                var options = param.Split('|');
                return isEdit ? options[0] : options[1]; // "Save" | "Create"
            }
            return "Save";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
