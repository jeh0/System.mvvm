using System.Collections.Specialized;
using System.Windows;

namespace System.Mvvm.Applications
{
    internal class CollectionChangedEventListener : IWeakEventListener
    {
        private readonly INotifyCollectionChanged m_Source;
        private readonly NotifyCollectionChangedEventHandler m_Handler;


        public CollectionChangedEventListener(INotifyCollectionChanged _source, NotifyCollectionChangedEventHandler _handler)
        {
            if (null == _source)
                throw new ArgumentNullException(nameof(_source));

            m_Source = _source;

            if (null == _handler)
                throw new ArgumentNullException(nameof(_handler));

            m_Handler = _handler;
        }


        public INotifyCollectionChanged Source => m_Source;

        public NotifyCollectionChangedEventHandler Handler => m_Handler;

        public bool ReceiveWeakEvent(Type _managerType, object _sender, EventArgs _e)
        {
            m_Handler(_sender, (NotifyCollectionChangedEventArgs)_e);
            return true;
        }
    }
}