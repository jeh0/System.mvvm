using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace System.Mvvm.Presentation
{
    // This class stores the ValidationErrors of an unloaded Control. 
    // When the Control is loaded again then it restores the ValidationErrors.
    internal class ValidationReloadedTracker
    {
        readonly ValidationTracker m_ValidationTracker;
        readonly IEnumerable<ValidationError> m_Errors;


        public ValidationReloadedTracker(ValidationTracker _validationTracker, object _validationSource, IEnumerable<ValidationError> _errors)
        {
            m_ValidationTracker = _validationTracker;
            m_Errors = _errors;

            if (_validationSource is FrameworkElement)
                ((FrameworkElement)_validationSource).Loaded += ValidationSourceLoaded;
            else
                ((FrameworkContentElement)_validationSource).Loaded += ValidationSourceLoaded;
        }


        void ValidationSourceLoaded(object _sender, RoutedEventArgs _e)
        {
            if (_sender is FrameworkElement)
                ((FrameworkElement)_sender).Loaded -= ValidationSourceLoaded;
            else
                ((FrameworkContentElement)_sender).Loaded -= ValidationSourceLoaded;

            m_ValidationTracker.AddErrors(_sender, m_Errors);
        }
    }
}