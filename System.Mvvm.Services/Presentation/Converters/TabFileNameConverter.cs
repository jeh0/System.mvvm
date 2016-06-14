using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;

namespace System.Mvvm.Converters
{
    public class TabFileNameConverter : IMultiValueConverter
    {
        const int c_MaxCharacters = 40;
        
        public object Convert(object[] _values, Type _targetType, object _parameter, CultureInfo _culture)
        {
            if (null == _values || 2 != _values.Length || !(_values[0] is string) || !(_values[1] is bool))
                return DependencyProperty.UnsetValue;
            
            string __fileName = Path.GetFileName((string)_values[0]);

            if (__fileName.Length > c_MaxCharacters)
                __fileName = __fileName.Remove(c_MaxCharacters - 3) + "...";

            return __fileName + ((bool)_values[1] ? "*" : string.Empty);
        }

        public object[] ConvertBack(object _value, Type[] _targetTypes, object _parameter, CultureInfo _culture)
        {
            throw new NotImplementedException();
        }
    }
}
