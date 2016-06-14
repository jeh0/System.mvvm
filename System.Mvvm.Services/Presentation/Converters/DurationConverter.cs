using System.Globalization;
using System.Windows.Data;

namespace System.Mvvm.Converters
{
    public class DurationConverter : IValueConverter
    {
        public object Convert(object _value, Type _targetType, object _parameter, CultureInfo _culture) => ((TimeSpan)_value).ToShortString();

        public object ConvertBack(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            throw new NotSupportedException();
        }
    }
}
