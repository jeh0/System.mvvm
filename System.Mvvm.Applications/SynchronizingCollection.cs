using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Mvvm.Base;

namespace System.Mvvm.Applications
{
    /// <summary> Represents a collection that synchronizes all of it's items with the items of the specified original collection.
    ///  When the original collection notifies a change via the <see cref="INotifyCollectionChanged"/> interface then
    ///  this collection synchronizes it's own items with the original items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <typeparam name="TOriginal">The type of elements in the original collection.</typeparam>
    public class SynchronizingCollection<T, TOriginal> : ReadOnlyObservableList<T>
    {
        private readonly ObservableCollection<T> m_InnerCollection;
        private readonly List<Tuple<TOriginal, T>> m_Mapping;
        private readonly IEnumerable<TOriginal> m_OriginalCollection;
        private readonly Func<TOriginal, T> m_Factory;
        private readonly IEqualityComparer<T> m_ItemComparer;
        private readonly IEqualityComparer<TOriginal> m_OriginalItemComparer;


        /// <summary> Initializes a new instance of the <see cref="SynchronizingCollection&lt;T, TOriginal&gt;"/> class. </summary>
        /// <param name="_originalCollection">The original collection.</param>
        /// <param name="_factory">The factory which is used to create new elements in this collection.</param>
        /// <exception cref="ArgumentNullException">The argument originalCollection must not be null.</exception>
        /// <exception cref="ArgumentNullException">The argument factory must not be null.</exception>
        public SynchronizingCollection(IEnumerable<TOriginal> _originalCollection, Func<TOriginal, T> _factory)
            : base(new ObservableCollection<T>())
        {
            if (null == _originalCollection) throw new ArgumentNullException(nameof(_originalCollection));
            if (null == _factory) throw new ArgumentNullException(nameof(_factory));

            m_Mapping = new List<Tuple<TOriginal, T>>();
            m_OriginalCollection = _originalCollection;
            m_Factory = _factory;
            m_ItemComparer = EqualityComparer<T>.Default;
            m_OriginalItemComparer = EqualityComparer<TOriginal>.Default;

            INotifyCollectionChanged collectionChanged = _originalCollection as INotifyCollectionChanged;
            if (null != collectionChanged)
            {
                CollectionChangedEventManager.AddHandler(collectionChanged, OriginalCollectionChanged);
            }

            m_InnerCollection = (ObservableCollection<T>)this.Items;
            foreach (TOriginal _item in _originalCollection)
            {
                m_InnerCollection.Add(CreateItem(_item));
            }
        }


        private T CreateItem(TOriginal _oldItem)
        {
            T __newItem = m_Factory(_oldItem);
            m_Mapping.Add(new Tuple<TOriginal, T>(_oldItem, __newItem));
            return __newItem;
        }

        private bool RemoveCore(TOriginal _oldItem)
        {
            Tuple<TOriginal, T> tuple = m_Mapping.First(t => m_OriginalItemComparer.Equals(t.Item1, _oldItem));
            m_Mapping.Remove(tuple);
            return m_InnerCollection.Remove(tuple.Item2);
        }

        private void RemoveAtCore(int _index)
        {
            T __newItem = this[_index];
            Tuple<TOriginal, T> __tuple = m_Mapping.First(t => m_ItemComparer.Equals(t.Item2, __newItem));
            m_Mapping.Remove(__tuple);
            m_InnerCollection.RemoveAt(_index);
        }

        private void ClearCore()
        {
            m_InnerCollection.Clear();
            m_Mapping.Clear();
        }

        private void OriginalCollectionChanged(object _sender, NotifyCollectionChangedEventArgs _e)
        {
            if (_e.Action == NotifyCollectionChangedAction.Add)
            {
                if (_e.NewStartingIndex >= 0)
                {
                    int i = _e.NewStartingIndex;
                    foreach (TOriginal item in _e.NewItems)
                    {
                        m_InnerCollection.Insert(i, CreateItem(item));
                        i++;
                    }
                }
                else
                {
                    foreach (TOriginal item in _e.NewItems)
                    {
                        m_InnerCollection.Add(CreateItem(item));
                    }
                }
            }
            else if (_e.Action == NotifyCollectionChangedAction.Remove)
            {
                if (_e.OldStartingIndex >= 0)
                {
                    for (int i = 0; i < _e.OldItems.Count; i++)
                    {
                        RemoveAtCore(_e.OldStartingIndex);
                    }
                }
                else
                {
                    foreach (TOriginal item in _e.OldItems)
                    {
                        RemoveCore(item);
                    }
                }
            }
            else if (_e.Action == NotifyCollectionChangedAction.Replace)
            {
                if (_e.NewStartingIndex >= 0)
                {
                    for (int i = 0; i < _e.NewItems.Count; i++)
                    {
                        m_InnerCollection[i + _e.NewStartingIndex] = CreateItem((TOriginal)_e.NewItems[i]);
                    }
                }
                else
                {
                    foreach (TOriginal item in _e.OldItems) { RemoveCore(item); }
                    foreach (TOriginal item in _e.NewItems) { m_InnerCollection.Add(CreateItem(item)); }
                }
            }
            else if (_e.Action == NotifyCollectionChangedAction.Move)
            {
                for (int i = 0; i < _e.NewItems.Count; i++)
                {
                    m_InnerCollection.Move(_e.OldStartingIndex + i, _e.NewStartingIndex + i);
                }
            }
            else // Reset
            {
                ClearCore();
                foreach (TOriginal item in m_OriginalCollection)
                {
                    m_InnerCollection.Add(CreateItem(item));
                }
            }
        }
    }
}
