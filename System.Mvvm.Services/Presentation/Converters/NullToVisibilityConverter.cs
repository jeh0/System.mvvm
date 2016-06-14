using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace System.Mvvm.Converters
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            bool __invert = ConverterHelper.IsParameterSet(nameof(__invert), _parameter);

            if (__invert)
            {
                return _value == null ? Visibility.Visible : Visibility.Collapsed;
            }

            return _value != null ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            throw new NotSupportedException();
        }
    }
}
