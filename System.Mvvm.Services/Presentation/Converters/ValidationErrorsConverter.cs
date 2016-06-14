using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace System.Mvvm.Converters
{
    /// <summary>
    ///   Multi value converter that converts a <see cref="ValidationError"/> collection to a multi-line string error message.
    ///   Use the binding for the second value to update the target when the <see cref="ValidationError"/> collection changes.
    ///   Set the path of the second binding on the Count property: Binding Path="(Validation.Errors).Count".
    /// </summary>
    /// 
    // Sample code (XAML) that shows how to use this converter:
    //
    // <Style.Triggers>
    //     <Trigger Property="Validation.HasError" Value="true">
    //         <Setter Property="ToolTip">
    //             <Setter.Value>
    //                 <MultiBinding Converter="{StaticResource ValidationErrorsConverter}">
    //                     <Binding Path="(Validation.Errors)" RelativeSource="{RelativeSource Self}"/>
    //                     <Binding Path="(Validation.Errors).Count" RelativeSource="{RelativeSource Self}"/>
    //                 </MultiBinding>
    //             </Setter.Value>
    //         </Setter>
    //     </Trigger>
    // </Style.Triggers>
    public sealed class ValidationErrorsConverter : IMultiValueConverter, IValueConverter
    {
        static readonly ValidationErrorsConverter m_DefaultInstance = new ValidationErrorsConverter();

        /// <summary> Gets the default instance of this converter. </summary>
        public static ValidationErrorsConverter Default => m_DefaultInstance; 


        /// <summary> Converts a collection of <see cref="ValidationError"/> objects into a multi-line string of error messages. </summary>
        /// <param name="_values">The first value is the collection of <see cref="ValidationError"/> objects.</param>
        /// <param name="_targetType">The type of the binding target property. This parameter will be ignored.</param>
        /// <param name="_parameter">The converter parameter to use. This parameter will be ignored.</param>
        /// <param name="_culture">The culture to use in the converter.</param>
        /// <returns>
        ///  A multi-line error message or an empty string when the collection contains no errors. If the first value parameter is <c>null</c>
        ///  or not of the type IEnumerable&lt;ValidationError&gt; this method returns <see cref="DependencyProperty.UnsetValue"/>.
        /// </returns>
        public object Convert(object[] _values, Type _targetType, object _parameter, CultureInfo _culture)
        {
            var __validationErrors = null == _values ? null : _values.FirstOrDefault() as IEnumerable<ValidationError>;
            if (null != __validationErrors)
                return string.Join(Environment.NewLine, __validationErrors.Select(x => x.ErrorContent.ToString()));

            return DependencyProperty.UnsetValue;
        }

        /// <summary> This method is not supported and throws an exception when it is called. </summary>
        /// <param name="_value">The value that is produced by the binding target.</param>
        /// <param name="_targetTypes">The array of types to convert to.</param>
        /// <param name="_parameter">The converter parameter to use.</param>
        /// <param name="_culture">The culture to use in the converter.</param>
        /// <returns>Nothing because this method throws an exception.</returns>
        /// <exception cref="NotSupportedException">Throws this exception when the method is called.</exception>
        public object[] ConvertBack(object _value, Type[] _targetTypes, object _parameter, CultureInfo _culture)
        {
            throw new NotSupportedException();
        }

        /// <summary> OBSOLETE: Converts a collection of <see cref="ValidationError"/> objects into a multi-line string of error messages. </summary>
        /// <param name="_value">The collection of <see cref="ValidationError"/> objects.</param>
        /// <param name="_targetType">The type of the binding target property. This parameter will be ignored.</param>
        /// <param name="_parameter">The converter parameter to use. This parameter will be ignored.</param>
        /// <param name="_culture">The culture to use in the converter.</param>
        /// <returns> A multi-line error message or an empty string when the collection contains no errors. If the value parameter is <c>null</c>
        /// or not of the type IEnumerable&lt;ValidationError&gt; this method returns <see cref="DependencyProperty.UnsetValue"/>.
        /// </returns>
        [Obsolete("Please use the Convert method that has as first parameter an array of values.")]
        public object Convert(object _value, Type _targetType, object _parameter, CultureInfo _culture) => Convert(new[] { _value }, _targetType, _parameter, _culture);

        /// <summary> OBSOLETE: This method is not supported and throws an exception when it is called. </summary>
        /// <param name="_value">The value that is produced by the binding target.</param>
        /// <param name="_targetType">The type to convert to.</param>
        /// <param name="_parameter">The converter parameter to use.</param>
        /// <param name="_culture">The culture to use in the converter.</param>
        /// <returns>Nothing because this method throws an exception.</returns>
        /// <exception cref="NotSupportedException">Throws this exception when the method is called.</exception>
        [Obsolete("ConvertBack is not supported.")]
        public object ConvertBack(object _value, Type _targetType, object _parameter, CultureInfo _culture)
        {
            throw new NotSupportedException();
        }
    }
}