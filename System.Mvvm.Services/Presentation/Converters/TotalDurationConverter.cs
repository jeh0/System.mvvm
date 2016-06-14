using System.Globalization;
using System.Windows.Data;

namespace System.Mvvm.Converters
{
    public class TotalDurationConverter : IMultiValueConverter
    {
        public object Convert(object[] _values, Type _targetType, object _parameter, CultureInfo _culture)
        {
            bool __isTotalDurationEstimated = (bool)_values[0];

            string __totalDuration = (string)new DurationConverter().Convert(_values[1], null, null, null);

            if (__isTotalDurationEstimated)
            {
                __totalDuration = string.Format(CultureInfo.CurrentCulture, "about {0}", __totalDuration);
            }

            return __totalDuration;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
