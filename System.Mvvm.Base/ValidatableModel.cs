using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using static System.String;

namespace System.Mvvm.Base
{
    /// <summary> Defines a base class for a model that supports validation. </summary>
    [Serializable]
    public abstract class ValidatableModel : Model, INotifyDataErrorInfo
    {
        static readonly ValidationResult[] m_NoErrors = new ValidationResult[0];

        [NonSerialized, NotMapped] bool m_HasErrors;
        [NonSerialized, NotMapped] public readonly Dictionary<string, List<ValidationResult>> Errors;

        /// <summary> Initializes a new instance of the <see cref="ValidatableModel"/> class. </summary>
        protected ValidatableModel()
        {
            Errors = new Dictionary<string, List<ValidationResult>>();
        }

        /// <summary> Gets a value that indicates whether the entity has validation errors. </summary>
        [NotMapped] public bool HasErrors 
        { 
            get { return m_HasErrors; }
            private set { SetProperty(ref m_HasErrors, value); }
        }


        /// <summary> Occurs when the validation errors have changed for a property or for the entire entity. </summary>
        [field:NonSerialized] public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;


        /// <summary> Gets the validation errors for the entire entity. </summary>
        /// <returns>The validation errors for the entity.</returns>
        public IEnumerable<ValidationResult> GetErrors() => GetErrors(null);

        /// <summary> Gets the validation errors for a specified property or for the entire entity. </summary>
        /// <param name="_propertyName">The name of the property to retrieve validation errors for; 
        /// or null or String.Empty, to retrieve entity-level errors.</param>
        /// <returns>The validation errors for the property or entity.</returns>
        public IEnumerable<ValidationResult> GetErrors(string _propertyName)
        {
            if (!string.IsNullOrEmpty(_propertyName))
            {
                List<ValidationResult> __result;
                if (Errors.TryGetValue(_propertyName, out __result))
                {
                    return __result;
                }

                return m_NoErrors;
            }
            else
            {
                return Errors.Values.SelectMany(x => x).Distinct().ToArray();
            }
        }
        
        IEnumerable INotifyDataErrorInfo.GetErrors(string _propertyName) => GetErrors(_propertyName);

        /// <summary> Validates the object and all its properties. The validation results are stored and can be retrieved by the 
        /// GetErrors method. If the validation results are changing then the ErrorsChanged event will be raised.
        /// </summary>
        /// <returns>True if the object is valid, otherwise false.</returns>
        public bool Validate()
        {
            var __validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(this, new ValidationContext(this), __validationResults, true);
            if (__validationResults.Any())
            {
                Errors.Clear();
                foreach (var _validationResult in __validationResults)
                {
                    var __propertyNames = _validationResult.MemberNames.Any() ? _validationResult.MemberNames : new string[] { "" };
                    foreach (string _propertyName in __propertyNames)
                    {
                        if (!Errors.ContainsKey(_propertyName))
                            Errors.Add(_propertyName, new List<ValidationResult>() { _validationResult });

                        else
                            Errors[_propertyName].Add(_validationResult);
                    }
                }
                RaiseErrorsChanged();
                return false;
            }
            else
            {
                if (Errors.Any())
                {
                    Errors.Clear();
                    RaiseErrorsChanged();
                }
            }
            return true;
        }

        /// <summary> Set the property with the specified value and validate the property. If the value is not equal with the field then the field is
        ///   set, a PropertyChanged event is raised, the property is validated and it returns true.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="_field">Reference to the backing field of the property.</param>
        /// <param name="_value">The new value for the property.</param>
        /// <param name="_propertyName">The property name. This optional parameter can be skipped
        /// because the compiler is able to create it automatically.</param>
        /// <returns>True if the value has changed, false if the old and new value were equal.</returns>
        /// <exception cref="ArgumentException">The argument propertyName must not be null or empty.</exception>
        protected bool SetPropertyAndValidate<T>(ref T _field, T _value, [CallerMemberName] string _propertyName = null)
        {
            if (IsNullOrEmpty(_propertyName)) throw new ArgumentException($"The argument {nameof(_propertyName)} must not be null or empty.");
            
            if (SetProperty(ref _field, _value, _propertyName))
            {
                ValidateProperty(_value, _propertyName);
                return true;
            }
            return false;
        }
         
        /// <summary> Validates the property with the specified value. The validation results are stored and can be retrieved by the 
        /// GetErrors method. If the validation results are changing then the ErrorsChanged event will be raised.
        /// </summary>
        /// <param name="_value">The value of the property.</param>
        /// <param name="_propertyName">The property name. This optional parameter can be skipped
        /// because the compiler is able to create it automatically.</param>
        /// <returns>True if the property value is valid, otherwise false.</returns>
        /// <exception cref="ArgumentException">The argument propertyName must not be null or empty.</exception>
        protected bool ValidateProperty(object _value, [CallerMemberName] string _propertyName = null)
        {
            if (IsNullOrEmpty(_propertyName))
                throw new ArgumentException($"The argument {nameof(_propertyName)} must not be null or empty.");
            
            var __validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(_value, new ValidationContext(this) { MemberName = _propertyName }, __validationResults);
            if (__validationResults.Any())
            {
                Errors[_propertyName] = __validationResults;
                RaiseErrorsChanged(_propertyName);
                return false;
            }
            else
            {
                if (Errors.Remove(_propertyName))
                    RaiseErrorsChanged(_propertyName);
            }
            return true;
        }

        /// <summary> Raises the <see cref="E:ErrorsChanged"/> event. </summary>
        /// <param name="e">The <see cref="System.ComponentModel.DataErrorsChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnErrorsChanged(DataErrorsChangedEventArgs e)
        {
            EventHandler<DataErrorsChangedEventArgs> __handler = ErrorsChanged;
            if (null != __handler)
            {
                __handler(this, e);
            }
        }
        void RaiseErrorsChanged(string _propertyName = null)
        {
            HasErrors = Errors.Any();
            OnErrorsChanged(new DataErrorsChangedEventArgs(_propertyName));
        }
    }
}