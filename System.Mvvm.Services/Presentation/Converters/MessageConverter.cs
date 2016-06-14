using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace System.Mvvm.Converters
{
    public sealed class MessageConverter : IMultiValueConverter
    {
        public object Convert(object[] _values, Type _targetType, object _parameter, CultureInfo _culture)
        {
            var __messages = _values?.FirstOrDefault() as IEnumerable<Tuple<string, string>>;
            if (null != __messages)
                return $"{(__messages.Any() ? __messages.Last().Item1 : null)}";

            return DependencyProperty.UnsetValue;
        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
