using System.Threading;
using System.Windows.Threading;

namespace System.Mvvm.Applications
{
    /// <summary> Provides helper methods that perform common tasks involving a view. </summary>
    public static class ViewHelper
    {
        /// <summary> Gets the ViewModel which is associated with the specified view. </summary>
        /// <param name="_view">The view.</param>
        /// <returns>The associated ViewModel, or <c>null</c> when no ViewModel was found.</returns>
        /// <exception cref="ArgumentNullException">view must not be <c>null</c>.</exception>
        public static ViewModel GetViewModel(this IView _view)
        {
            if (null == _view)
                throw new ArgumentNullException(nameof(_view));

            object __dataContext = _view.DataContext;

            // When the DataContext is null then it might be that the ViewModel hasn't set it yet.
            // Enforce it by executing the event queue of the Dispatcher.
            if (null == __dataContext && SynchronizationContext.Current is DispatcherSynchronizationContext)
            {
                DispatcherHelper.DoEvents();
                __dataContext = _view.DataContext;
            }

            return __dataContext as ViewModel;
        }
        
        /// <summary> Gets the ViewModel which is associated with the specified view. </summary>
        /// <typeparam name="T"> The type of the ViewModel </typeparam>
        /// <param name="_view"> The view. </param>
        /// <returns> The associated ViewModel, or <c>null</c> when no ViewModel was found. </returns>
        /// <exception cref="ArgumentNullException"> view must not be <c>null</c>. </exception>
        public static T GetViewModel<T>(this IView _view) where T : ViewModel => GetViewModel(_view) as T;
    }
}
