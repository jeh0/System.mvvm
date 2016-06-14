using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace System.Mvvm.Converters
{
    public class PercentConverter : IValueConverter
    {
        public object Convert(object _value, Type _targetType, object _parameter, CultureInfo _culture) => string.Format(_culture, "{0:P0}", _value);

        public object ConvertBack(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            _culture = _culture ?? CultureInfo.CurrentCulture;

            double _d;
            if (double.TryParse(((string)_value).Replace(_culture.NumberFormat.PercentSymbol, string.Empty)
                    , NumberStyles.Float | NumberStyles.AllowThousands, _culture, out _d))
                return _d / 100d;

            return DependencyProperty.UnsetValue;
        }
    }
}
