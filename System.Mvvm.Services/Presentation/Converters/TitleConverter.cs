using System.Globalization;
using System.Windows;
using System.Windows.Data;
using static System.String;

namespace System.Mvvm.Converters
{
    public class TitleConverter : IMultiValueConverter
    {
        public object Convert(object[] _values, Type _targetType, object _parameter, CultureInfo _culture)
        {
            if (null == _values || 2 != _values.Length || !(_values[0] is string) || !(null == _values[1] || _values[1] is string))
                return DependencyProperty.UnsetValue;

            var __title = (string)_values[0];
            var __documentName = (string)_values[1];

            if (!IsNullOrEmpty(__documentName)) 
                __title = $"{__documentName} - {__title}";

            return __title;
        }

        public object[] ConvertBack(object _value, Type[] _targetTypes, object _parameter, CultureInfo _culture)
        {
            throw new NotSupportedException();
        }
    }
}