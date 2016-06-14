using System.ComponentModel;

namespace System.Mvvm.Base
{

    /// <summary> Extends the <see cref="IDataErrorInfo"/> interface with new Validation methods. </summary>
    public static class DataErrorInfoExtensions
    {
        /// <summary> Validates the specified object. </summary>
        /// <param name="_instance">The object to validate.</param>
        /// <returns>An error message indicating what is wrong with this object. The default is an empty string ("").</returns>
        /// <exception cref="ArgumentNullException">The argument instance must not be null.</exception>
        public static string Validate(this IDataErrorInfo _instance)
        {
            if (null == _instance) throw new ArgumentNullException(nameof(_instance));

            return _instance.Error ?? string.Empty;
        }

        /// <summary> Validates the specified member of the object. </summary>
        /// <param name="_instance">The object to validate.</param>
        /// <param name="_memberName">The name of the member to validate.</param>
        /// <returns>The error message for the member. The default is an empty string ("").</returns>
        /// <exception cref="ArgumentNullException">The argument instance must not be null.</exception>
        public static string Validate(this IDataErrorInfo _instance, string _memberName)
        {
            if (null == _instance) throw new ArgumentNullException(nameof(_instance));

            return _instance[_memberName] ?? string.Empty;
        }
    }
}
