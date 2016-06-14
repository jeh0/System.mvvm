﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace System.Mvvm.Presentation
{
    /// <summary> Provides methods and attached properties that support data validation tracking. 
    ///   This works only if NotifyOnValidationError=true is set by every Binding that participates in data validation.
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary> Identifies the IsEnabled attached property. This property contains the value 
        ///   that indicates whether the ValidationHelper is enabled.
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty
            = DependencyProperty.RegisterAttached("IsEnabled"
                , typeof(bool)
                , typeof(ValidationHelper)
                , new FrameworkPropertyMetadata(false, IsEnabledChangedCallback))
            ;

        /// <summary> Identifies the IsValid attached property. 
        ///   This property contains the value that indicates whether the associated object is valid.
        /// </summary>
        /// <remarks> This is a readonly property. However, it is allowed to define a Binding with 
        ///   the Mode=OneWayToSource for this property.
        /// </remarks>
        public static readonly DependencyProperty IsValidProperty
            = DependencyProperty.RegisterAttached("IsValid"
                , typeof(bool)
                , typeof(ValidationHelper)
                , new FrameworkPropertyMetadata(true, IsValidChangedCallback))
            ;


        /// <summary> Gets a value that indicates whether the ValidationHelper is enabled. </summary>
        /// <param name="_obj"> The object to read the value from. </param>
        /// <returns> true, if the ValidationHelper is enabled. </returns>
        public static bool GetIsEnabled(DependencyObject _obj) => (bool)_obj.GetValue(IsEnabledProperty);

        /// <summary> Sets the value that indicates whether the ValidationHelper is enabled.
        ///   The default value is 'false'. It is not allowed to call this method with 'false'.
        /// </summary>
        /// <param name="_obj"> The object to set the value to. </param>
        /// <param name="value"> if set to <c>true</c> then the ValidationHelper is enabled for the specified object. </param>
        /// <exception cref="ArgumentException"> The value must not be set to 'false'. </exception>
        public static void SetIsEnabled(DependencyObject _obj, bool value) => _obj.SetValue(IsEnabledProperty, value);

        /// <summary> Gets a value that indicates whether the object is valid. </summary>
        /// <param name="_obj"> The object to read the value from. </param>
        /// <returns> true, if the object is valid. </returns>
        public static bool GetIsValid(DependencyObject _obj) => (bool)_obj.GetValue(IsValidProperty);

        /// <summary> Do not call this method. It is for internal use only. </summary>
        /// <param name="_obj">The object to set the value to.</param>
        /// <param name="_value">if set to <c>true</c> then the object is valid.</param>
        /// <exception cref="InvalidOperationException">Thrown when this method is called.</exception>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void SetIsValid(DependencyObject _obj, bool _value)
        {
            throw new InvalidOperationException("The IsValid attached dependency property is readonly and must not be modified!");
        }

        internal static void InternalSetIsValid(DependencyObject _obj, bool _value)
        {
            if (GetIsValid(_obj) != _value)
                // Only change the value when it is different; otherwise unnecessary binding updates are raised.
                _obj.SetCurrentValue(IsValidProperty, _value);
        }

        static void IsEnabledChangedCallback(DependencyObject _obj, DependencyPropertyChangedEventArgs _e)
        {
            if (!GetIsEnabled(_obj))
                throw new ArgumentException("The IsEnabled attached dependency property must not be set to 'false'.");

            // We only create an instance of the tracker. It keeps alive because it listens to the Error event.
            new ValidationTracker(_obj);
        }

        static void IsValidChangedCallback(DependencyObject _obj, DependencyPropertyChangedEventArgs _e)
        {
            if (!GetIsEnabled(_obj))
                throw new InvalidOperationException("The IsValid attached property can only be used when the IsEnabled attached property is set to true.");

            Binding __binding = BindingOperations.GetBinding(_obj, IsValidProperty);
            if (null != __binding)
            {
                CheckBindingMode(__binding.Mode);
                return;
            }

            MultiBinding __multiBinding = BindingOperations.GetMultiBinding(_obj, IsValidProperty);
            if (null != __multiBinding)
            {
                CheckBindingMode(__multiBinding.Mode);
                return;
            }

            PriorityBinding __priorityBinding = BindingOperations.GetPriorityBinding(_obj, IsValidProperty);
            if (null != __priorityBinding)
                throw new InvalidOperationException("PriorityBinding is not supported for the IsValid attached dependency property!");
        }

        static void CheckBindingMode(BindingMode _bindingMode)
        {
            if (_bindingMode != BindingMode.OneWayToSource)
                throw new InvalidOperationException("The binding mode of the IsValid attached dependency property must be 'Mode=OneWayToSource'!");
        }
    }
}
