using System.Globalization;
using System.Windows.Data;

namespace System.Mvvm.Converters
{
    /// <summary> Value converter that converts an object into a formatted string. 
    ///   The format specification is passed via the  ConverterParameter property.
    /// </summary>
    [ValueConversion(typeof(object), typeof(string))]
    public sealed class StringFormatConverter : IValueConverter
    {
        static readonly StringFormatConverter m_DefaultInstance = new StringFormatConverter();

        /// <summary> Gets the default instance of this converter. </summary>
        public static StringFormatConverter Default => m_DefaultInstance;


        /// <summary> Converts an object into a formatted string. </summary>
        /// <param name="_value">The object to convert.</param>
        /// <param name="_targetType">The type of the binding target property. This parameter will be ignored.</param>
        /// <param name="_parameter">The format specification used to format the object.</param>
        /// <param name="_culture">The culture to use in the converter.</param>
        /// <returns>The formatted string.</returns>
        public object Convert(object _value, Type _targetType, object _parameter, CultureInfo _culture) => string.Format(_culture, _parameter as string ?? "{0}", _value);

        /// <summary> This method is not supported and throws an exception when it is called. </summary>
        /// <param name="_value">The value that is produced by the binding target.</param>
        /// <param name="_targetType">The type to convert to.</param>
        /// <param name="_parameter">The converter parameter to use.</param>
        /// <param name="_culture">The culture to use in the converter.</param>
        /// <returns>Nothing because this method throws an exception.</returns>
        /// <exception cref="NotSupportedException">Throws this exception when the method is called.</exception>
        public object ConvertBack(object _value, Type _targetType, object _parameter, CultureInfo _culture) => new NotSupportedException();
    }
}
