using System.Globalization;
using System.Windows.Data;

namespace System.Mvvm.Converters
{
    public class RatingToStarsConverter : IValueConverter
    {
        public object Convert(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            int __rating = System.Convert.ToInt32(_value, CultureInfo.InvariantCulture);
            if (__rating >= 99)
            {
                return 5;
            }
            else if (__rating >= 75)
            {
                return 4;
            }
            else if (__rating >= 50)
            {
                return 3;
            }
            else if (__rating >= 25)
            {
                return 2;
            }
            else if (__rating >= 1)
            {
                return 1;
            }
            return 0;
            
        }

        public object ConvertBack(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            int __stars = System.Convert.ToInt32(_value, CultureInfo.InvariantCulture);
            if (__stars == 5)
            {
                return 99;
            }
            else if (__stars == 4)
            {
                return 75;
            }
            else if (__stars == 3)
            {
                return 50;
            }
            else if (__stars == 2)
            {
                return 25;
            }
            else if (__stars == 1)
            {
                return 1;
            }
            return 0;
        }
    }
}
