using System.Threading;
using System.Windows.Threading;

namespace System.Mvvm.Applications
{
    /// <summary> Abstract base class for a ViewModel implementation. </summary>
#pragma warning disable 618
    public abstract class ViewModel : DataModel
#pragma warning restore 618
    {
        private readonly IView m_View;


        /// <summary> Initializes a new instance of the <see cref="ViewModel"/> class and attaches itself as <c>DataContext</c> to the view. </summary>
        /// <param name="_view">The view.</param>
        protected ViewModel(IView _view)
        {
            if (null == _view)
                throw new ArgumentNullException(nameof(_view));

            m_View = _view;
            
            // Check if the code is running within the WPF application model
            if (SynchronizationContext.Current is DispatcherSynchronizationContext)
            {
                // Set DataContext of the view has to be delayed so that the ViewModel can initialize the internal data (e.g. Commands)
                // before the view starts with DataBinding.
                Dispatcher.CurrentDispatcher.BeginInvoke((Action)delegate()
                {
                     m_View.DataContext = this;
                });
            }
            else
            {
                // When the code runs outside of the WPF application model then we set the DataContext immediately.
                _view.DataContext = this;
            }
        }

        /// <summary> Gets the associated view. </summary>
        public object View => m_View;
    }
}
