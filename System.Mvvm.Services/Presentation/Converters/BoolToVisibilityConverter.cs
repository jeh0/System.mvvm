using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace System.Mvvm.Converters
{
    /// <summary> Value converter that converts a boolean value to and from Visibility enumeration values. </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibilityConverter : IValueConverter
    {
        static readonly BoolToVisibilityConverter m_DefaultInstance = new BoolToVisibilityConverter();

        /// <summary> Gets the default instance of this converter. </summary>
        public static BoolToVisibilityConverter Default => m_DefaultInstance;


        /// <summary> Converts a boolean value into a Visibility enumeration value. </summary>
        /// <param name="_value">The boolean value.</param>
        /// <param name="_targetType">The type of the binding target property. This parameter will be ignored.</param>
        /// <param name="_parameter">Use the string 'Invert' to get an inverted result (Visible and Collapsed are exchanged). 
        /// Do not specify this parameter if the default behavior is desired.</param>
        /// <param name="_culture">The culture to use in the converter.</param>
        /// <returns>Visible when the boolean value was true; otherwise Collapsed.</returns>
        public object Convert(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            bool? __flag = (bool?)_value;
            bool __invert = IsInvertParameterSet(_parameter);

            if (__invert)
                return __flag == true ? Visibility.Collapsed : Visibility.Visible;

            return __flag == true ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary> Converts a Visibility enumeration value into a boolean value. </summary>
        /// <param name="_value">The Visibility enumeration value.</param>
        /// <param name="_targetType">The type of the binding target property. This parameter will be ignored.</param>
        /// <param name="_parameter">Use the string 'Invert' to get an inverted result (true and false are exchanged). 
        /// Do not specify this parameter if the default behavior is desired.</param>
        /// <param name="_culture">The culture to use in the converter.</param>
        /// <returns>true when the Visibility enumeration value was Visible; otherwise false.</returns>
        public object ConvertBack(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            var __visibility = (Visibility)_value;
            bool invert = IsInvertParameterSet(_parameter);
            
            if (invert)
                return __visibility != Visibility.Visible;

            return __visibility == Visibility.Visible;
        }

        static bool IsInvertParameterSet(object _parameter)
        {
            var __invertParameter = _parameter as string;
            return !string.IsNullOrEmpty(__invertParameter) && string.Equals(__invertParameter, "invert", StringComparison.OrdinalIgnoreCase);
        }
    }
}
