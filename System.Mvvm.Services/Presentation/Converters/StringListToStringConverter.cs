using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace System.Mvvm.Converters
{
    using Applications.Data;

    public class StringListToStringConverter : IValueConverter
    {
        public object Convert(object _value, Type _targetType, object _parameter, CultureInfo _culture)
            => StringListConverter.ToString(((IEnumerable<string>)_value), GetSeparator(_parameter));

        public object ConvertBack(object _value, Type _targetType, object _parameter, CultureInfo _culture)
            => StringListConverter.FromString((string)_value, GetSeparator(_parameter));
        

        private static string GetSeparator(object _commandParameter)
            => ConverterHelper.IsParameterSet("ListSeparator", _commandParameter) ? null : Environment.NewLine;
    }
}
