using System.ComponentModel;
using System.Windows;

namespace System.Mvvm.Applications
{
    internal class PropertyChangedEventListener : IWeakEventListener
    {
        readonly INotifyPropertyChanged m_Source;
        readonly PropertyChangedEventHandler m_Handler;

        public PropertyChangedEventListener(INotifyPropertyChanged _source, PropertyChangedEventHandler _handler)
        {
            if (null == _source)
                throw new ArgumentNullException(nameof(_source));
            m_Source = _source;

            if (null == _handler)
                throw new ArgumentNullException(nameof(_handler));
            m_Handler = _handler;
        }


        public INotifyPropertyChanged Source => m_Source;
        public PropertyChangedEventHandler Handler => m_Handler;
        
        public bool ReceiveWeakEvent(Type _managerType, object _sender, EventArgs _e)
        {
            m_Handler(_sender, (PropertyChangedEventArgs)_e);
            return true;
        }
    }
}
