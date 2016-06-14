using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace System.Mvvm.Presentation
{
    // This class listens to the Validation.Error event of the owner (Control). 
    // When the Error event is raised then it synchronizes the errors with its internal errors list and updates the ValidationHelper.
    internal sealed class ValidationTracker
    {
        readonly List<Tuple<object, ValidationError>> m_Errors;
        readonly DependencyObject m_Owner;
        
        public ValidationTracker(DependencyObject _owner)
        {
            m_Owner = _owner;
            m_Errors = new List<Tuple<object, ValidationError>>();

            Validation.AddErrorHandler(_owner, ErrorChangedHandler);
        }


        internal void AddErrors(object _validationSource, IEnumerable<ValidationError> _errors)
        {
            foreach (ValidationError _error in _errors)
                AddError(_validationSource, _error);

            ValidationHelper.InternalSetIsValid(m_Owner, !_errors.Any());
        }

        void AddError(object _validationSource, ValidationError _error)
        {
            m_Errors.Add(new Tuple<object, ValidationError>(_validationSource, _error));

            if (_validationSource is FrameworkElement)
                ((FrameworkElement)_validationSource).Unloaded += ValidationSourceUnloaded;

            else if (_validationSource is FrameworkContentElement)
                ((FrameworkContentElement)_validationSource).Unloaded += ValidationSourceUnloaded;
        }

        void ErrorChangedHandler(object _sender, ValidationErrorEventArgs _e)
        {
            if (_e.Action == ValidationErrorEventAction.Added)
                AddError(_e.OriginalSource, _e.Error);
            else
            {
                Tuple<object, ValidationError> __error = m_Errors.FirstOrDefault(err => err.Item1 == _e.OriginalSource && err.Item2 == _e.Error);

                if (null != __error)
                    m_Errors.Remove(__error);
            }

            ValidationHelper.InternalSetIsValid(m_Owner, !m_Errors.Any());
        }

        void ValidationSourceUnloaded(object _sender, RoutedEventArgs _e)
        {
            if (_sender is FrameworkElement)
                ((FrameworkElement)_sender).Unloaded -= ValidationSourceUnloaded;

            else
                ((FrameworkContentElement)_sender).Unloaded -= ValidationSourceUnloaded;

            // An unloaded control might be loaded again. Then we need to restore the validation errors.
            Tuple<object, ValidationError>[] __errorsToRemove = m_Errors.Where(err => err.Item1 == _sender).ToArray();
            if (__errorsToRemove.Any())
            {
                // It keeps alive because it listens to the Loaded event.
                new ValidationReloadedTracker(this, __errorsToRemove.First().Item1, __errorsToRemove.Select(x => x.Item2));

                foreach (Tuple<object, ValidationError> error in __errorsToRemove)
                    m_Errors.Remove(error);
            }

            ValidationHelper.InternalSetIsValid(m_Owner, !m_Errors.Any());
        }
    }
}