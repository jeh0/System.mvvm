using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace System.Mvvm.Base
{

    /// <summary> This class provides an implementation for the <see cref="IDataErrorInfo"/> interface which uses the
    ///  validation classes found in the <see cref="System.ComponentModel.DataAnnotations"/> namespace.
    /// </summary>
    public sealed class DataErrorInfoSupport : IDataErrorInfo
    {
        private readonly object m_Instance;


        /// <summary> Initializes a new instance of the <see cref="DataErrorInfoSupport"/> class. </summary>
        /// <param name="_instance">The instance.</param>
        /// <exception cref="ArgumentNullException">instance must not be <c>null</c>.</exception>
        public DataErrorInfoSupport(object _instance)
        {
            if (null == _instance)
                throw new ArgumentNullException(nameof(_instance));

            m_Instance = _instance;
        }


        /// <summary> Gets an error message indicating what is wrong with this object. </summary>
        /// <returns>An error message indicating what is wrong with this object. The default is an empty string ("").</returns>
        public string Error => this[string.Empty];

        /// <summary> Gets the error message for the property with the given name. /// </summary>
        /// <param name="_memberName">The name of the property whose error message to get.</param>
        /// <returns>The error message for the property. The default is an empty string ("").</returns>
        public string this[string _memberName]
        {
            get
            {
                var __validationResults = new List<ValidationResult>();

                if (string.IsNullOrEmpty(_memberName))
                    Validator.TryValidateObject(m_Instance, new ValidationContext(m_Instance, null, null), __validationResults, true);

                else
                {
                    PropertyDescriptor property = TypeDescriptor.GetProperties(m_Instance)[_memberName];
                    if (null == property)
                    {
                        throw new ArgumentException(string.Format(CultureInfo.CurrentCulture,
                            "The specified member {0} was not found on the instance {1}", _memberName, m_Instance.GetType()));
                    }

                    Validator.TryValidateProperty(property.GetValue(m_Instance),
                        new ValidationContext(m_Instance, null, null) { MemberName = _memberName }, __validationResults);
                }

                return string.Join(Environment.NewLine, __validationResults.Select(x => x.ErrorMessage));
            }
        }
    }
}
