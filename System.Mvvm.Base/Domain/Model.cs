using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace System.Mvvm.Base
{
    /// <summary> Defines the base class for a model. </summary>
    [Serializable]
    public abstract class Model : INotifyPropertyChanged
    {
        /// <summary> Occurs when a property value changes. </summary>
        [field: NonSerialized] public event PropertyChangedEventHandler PropertyChanged;


        /// <summary> Set the property with the specified value. If the value is not equal with the field then the field is
        /// set, a PropertyChanged event is raised and it returns true.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="_field">Reference to the backing field of the property.</param>
        /// <param name="_value">The new value for the property.</param>
        /// <param name="_propertyName">The property name. This optional parameter can be skipped
        /// because the compiler is able to create it automatically.</param>
        /// <returns> True if the value has changed, false if the old and new value were equal. </returns>
        protected bool SetProperty<T>(ref T _field, T _value, [CallerMemberName] string _propertyName = null)
        {
            if (Equals(_field, _value)) return false;

            _field = _value;
            RaisePropertyChanged(_propertyName);
            return true;
        }

        /// <summary> Raises the <see cref="E:PropertyChanged"/> event. </summary>
        /// <param name="_propertyName">The property name of the property that has changed.
        /// This optional parameter can be skipped because the compiler is able to create it automatically.</param>
        protected void RaisePropertyChanged([CallerMemberName] string _propertyName = null) => OnPropertyChanged(new PropertyChangedEventArgs(_propertyName));

        /// <summary Raises the <see cref="E:PropertyChanged"/> event. </summary>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs _e)
        {
            PropertyChangedEventHandler __handler = PropertyChanged;
            if (null != __handler)
                __handler(this, _e);
        }
    } // --- Model : class ---
}