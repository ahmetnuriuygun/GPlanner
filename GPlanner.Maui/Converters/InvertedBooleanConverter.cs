using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace GPlanner.Maui.Converters
{
    /// <summary>
    /// Converts a boolean value to its inverse. 
    /// Can also convert boolean to two specified strings (or image sources) based on the input.
    /// </summary>
    /// <remarks>
    /// Usage Examples:
    /// 1. Invert boolean: IsEnabled="{Binding IsBusy, Converter={StaticResource InvertedBooleanConverter}}"
    /// 2. Swap strings: Text="{Binding IsDone, Converter={StaticResource InvertedBooleanConverter}, ConverterParameter='Task Not Done|Task Complete'}"
    /// </remarks>
    public class InvertedBooleanConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not bool boolValue)
            {
                return value;
            }

            // If a parameter is provided, use it to swap strings/paths/values
            if (parameter is string paramString && paramString.Contains('|'))
            {
                // Format: 'ValueIfTrue|ValueIfFalse'
                string[] parts = paramString.Split('|');

                if (parts.Length == 2)
                {
                    // The standard binding provides ValueIfTrue, ValueIfFalse.
                    // We return ValueIfTrue when the source boolean is TRUE (i.e., not inverted).
                    return boolValue ? parts[0] : parts[1];
                }
            }

            // Default behavior: invert the boolean value
            return !boolValue;
        }

        // FIX: Change 'object parameter' to 'object? parameter' to resolve CS8767
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            // Only handle simple boolean conversion back
            if (value is bool boolValue)
            {
                return !boolValue;
            }

            // For string/value swap, conversion back is not supported or necessary here
            return value;
        }
    }
}