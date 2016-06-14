using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Mvvm.Base;

namespace System.Mvvm.Applications
{
    /// <summary> Obsolete: Abstract base class for a DataModel implementation. </summary>
    [Serializable, Obsolete("Do not inherit from this class. Use the PropertyChangedEventManager or the CollectionChangedEventManager instead of the Add/RemoveWeakEventListener method.")]
    public abstract class DataModel : Model
    {
        [NonSerialized] readonly List<PropertyChangedEventListener> m_PropertyChangedListeners = new List<PropertyChangedEventListener>();
        [NonSerialized] readonly List<CollectionChangedEventListener> m_CollectionChangedListeners = new List<CollectionChangedEventListener>();
        
        /// <summary> Adds a weak event listener for a PropertyChanged event. </summary>
        /// <param name="_source">The source of the event.</param>
        /// <param name="_handler">The event handler.</param>
        /// <exception cref="ArgumentNullException">source must not be <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">handler must not be <c>null</c>.</exception>
        [Obsolete("Please use the PropertyChangedEventManager.AddHandler instead.")]
        protected void AddWeakEventListener(INotifyPropertyChanged _source, PropertyChangedEventHandler _handler)
        {
            if (null == _source) throw new ArgumentNullException(nameof(_source));
            if (null == _handler) throw new ArgumentNullException(nameof(_handler));

            var __listener = new PropertyChangedEventListener(_source, _handler);

            m_PropertyChangedListeners.Add(__listener);

            PropertyChangedEventManager.AddListener(_source, __listener, "");
        }

        /// <summary> Removes the weak event listener for a PropertyChanged event. </summary>
        /// <param name="_source">The source of the event.</param>
        /// <param name="_handler">The event handler.</param>
        /// <exception cref="ArgumentNullException">source must not be <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">handler must not be <c>null</c>.</exception>
        [Obsolete("Please use the PropertyChangedEventManager.RemoveHandler instead.")]
        protected void RemoveWeakEventListener(INotifyPropertyChanged _source, PropertyChangedEventHandler _handler)
        {
            if (null == _source) throw new ArgumentNullException(nameof(_source));
            if (null == _handler) throw new ArgumentNullException(nameof(_handler));

            PropertyChangedEventListener __listener = m_PropertyChangedListeners.LastOrDefault(l => l.Source == _source && l.Handler == _handler);

            if (null != __listener)
            {
                m_PropertyChangedListeners.Remove(__listener);
                PropertyChangedEventManager.RemoveListener(_source, __listener, string.Empty);
            }
        }

        /// <summary> Adds a weak event listener for a CollectionChanged event. </summary>
        /// <param name="_source">The source of the event.</param>
        /// <param name="_handler">The event handler.</param>
        /// <exception cref="ArgumentNullException">source must not be <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">handler must not be <c>null</c>.</exception>
        [Obsolete("Please use the CollectionChangedEventManager.AddHandler instead.")]
        protected void AddWeakEventListener(INotifyCollectionChanged _source, NotifyCollectionChangedEventHandler _handler)
        {
            if (null == _source) throw new ArgumentNullException(nameof(_source));
            if (null == _handler) throw new ArgumentNullException(nameof(_handler));

            var __listener = new CollectionChangedEventListener(_source, _handler);

            m_CollectionChangedListeners.Add(__listener);

            CollectionChangedEventManager.AddListener(_source, __listener);
        }

        /// <summary> Removes the weak event listener for a CollectionChanged event. </summary>
        /// <param name="_source">The source of the event.</param>
        /// <param name="_handler">The event handler.</param>
        /// <exception cref="ArgumentNullException">source must not be <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">handler must not be <c>null</c>.</exception>
        [Obsolete("Please use the CollectionChangedChangedEventManager.RemoveHandler instead.")]
        protected void RemoveWeakEventListener(INotifyCollectionChanged _source, NotifyCollectionChangedEventHandler _handler)
        {
            if (null == _source) throw new ArgumentNullException(nameof(_source));
            if (null == _handler) throw new ArgumentNullException(nameof(_handler));

            CollectionChangedEventListener __listener = m_CollectionChangedListeners.LastOrDefault(l => l.Source == _source && l.Handler == _handler);

            if (null != __listener)
            {
                m_CollectionChangedListeners.Remove(__listener);
                CollectionChangedEventManager.RemoveListener(_source, __listener);
            }
        }
    }
}