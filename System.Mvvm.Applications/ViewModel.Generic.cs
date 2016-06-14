namespace System.Mvvm.Applications
{
    /// <summary> Abstract base class for a ViewModel implementation. </summary>
    /// <typeparam name="TView"> The type of the view. Do provide an interface as type and not the concrete type itself. </typeparam>
    public abstract class ViewModel<TView> : ViewModel where TView : IView
    {
        readonly TView m_View;

        /// <summary> Initializes a new instance of the <see cref="ViewModel&lt;TView&gt;"/> class and attaches itself as <c>DataContext</c> to the view. </summary>
        /// <param name="_view"> The view.</param>
        protected ViewModel(TView _view) : base(_view)
        {
            m_View = _view;
        }

        /// <summary> Gets the associated view as specified view type. </summary>
        /// <remarks> Use this property in a ViewModel class to avoid casting. </remarks>
        protected TView ViewCore => m_View;
    }
}