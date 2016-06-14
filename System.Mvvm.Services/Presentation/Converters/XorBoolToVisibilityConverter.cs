using System.Globalization;
using System.Windows.Data;

namespace System.Mvvm.Converters
{
    public class XorBoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            var __bool = (bool)_value;

            return ! __bool;
        }

        public object ConvertBack(object _value, Type _targetType, object _parameter, CultureInfo _culture) { throw new NotSupportedException(); }
    }
}
