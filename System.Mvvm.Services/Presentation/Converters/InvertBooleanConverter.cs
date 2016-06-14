using System.Globalization;
using System.Windows.Data;

namespace System.Mvvm.Converters
{
    /// <summary> Value converter that inverts a boolean value. </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public class InvertBooleanConverter : IValueConverter
    {
        static readonly InvertBooleanConverter defaultInstance = new InvertBooleanConverter();

        /// <summary> Gets the default instance of this converter. </summary>
        public static InvertBooleanConverter Default { get { return defaultInstance; } }
        
        /// <summary> Converts a boolean value into the inverted value. </summary>
        /// <param name="_value">The boolean value to invert.</param>
        /// <param name="_targetType">The type of the binding target property. This parameter will be ignored.</param>
        /// <param name="_parameter">The converter parameter to use. This parameter will be ignored.</param>
        /// <param name="_culture">The culture to use in the converter.</param>
        /// <returns>The inverter boolean value.</returns>
        public object Convert(object _value, Type _targetType, object _parameter, CultureInfo _culture) => !(bool)_value;
         
        /// <summary> Converts a boolean value into the inverted value. </summary>
        /// <param name="_value">The boolean value to invert.</param>
        /// <param name="_targetType">The type to convert to. This parameter will be ignored.</param>
        /// <param name="_parameter">The converter parameter to use. This parameter will be ignored.</param>
        /// <param name="_culture">The culture to use in the converter.</param>
        /// <returns>The inverter boolean value.</returns>
        public object ConvertBack(object _value, Type _targetType, object _parameter, CultureInfo _culture) => !(bool)_value;
    }
}
