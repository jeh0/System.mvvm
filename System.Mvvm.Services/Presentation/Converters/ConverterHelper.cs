using static System.String;

namespace System.Mvvm.Converters
{
    internal static class ConverterHelper
    {
        public static bool IsParameterSet(string _expectedParameter, object _actualParameter)
        {
            string __parameter = _actualParameter as string;

            return !IsNullOrEmpty(__parameter) && string.Equals(__parameter, _expectedParameter, StringComparison.OrdinalIgnoreCase);
        }
    }
}
