using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace System.Mvvm.Converters
{
    public sealed class ErrorMessagesConverter : IMultiValueConverter
    {
        public object Convert(object[] _values, Type _targetType, object _parameter, CultureInfo _culture)
        {
            var __errorMessages = _values?.FirstOrDefault() as IEnumerable<Tuple<Exception, string>>;
            if (null != __errorMessages)
            {
                string __message = __errorMessages.Any() ? __errorMessages.Last().Item2 : null;
                return $"Error: {__errorMessages.Count()} {__message}";

            }

            return DependencyProperty.UnsetValue;
        }
        
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
