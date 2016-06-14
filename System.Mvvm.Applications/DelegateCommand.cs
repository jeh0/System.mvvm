using System.Windows.Input;

namespace System.Mvvm.Applications
{
    /// <summary> Provides an <see cref="ICommand"/> implementation which relays the <see cref="Execute"/> and <see cref="CanExecute"/>  
    ///     method to the specified delegates.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> m_Execute;
        private readonly Func<object, bool> m_CanExecute;


        /// <summary> Initializes a new instance of the <see cref="DelegateCommand"/> class. </summary>
        /// <param name="_execute">Delegate to execute when Execute is called on the command.</param>
        /// <exception cref="ArgumentNullException">The execute argument must not be null.</exception>
        public DelegateCommand(Action _execute) : this(_execute, null) { }

        /// <summary> Initializes a new instance of the <see cref="DelegateCommand"/> class. </summary>
        /// <param name="_execute">Delegate to execute when Execute is called on the command.</param>
        /// <exception cref="ArgumentNullException">The execute argument must not be null.</exception>
        public DelegateCommand(Action<object> _execute) : this(_execute, null) { }

        /// <summary> Initializes a new instance of the <see cref="DelegateCommand"/> class. </summary>
        /// <param name="_execute">Delegate to execute when Execute is called on the command.</param>
        /// <param name="canExecute">Delegate to execute when CanExecute is called on the command.</param>
        /// <exception cref="ArgumentNullException">The execute argument must not be null.</exception>
        public DelegateCommand(Action _execute, Func<bool> canExecute)
            : this(_execute != null ? p => _execute() : (Action<object>)null, canExecute != null ? p => canExecute() : (Func<object, bool>)null)
        { }

        /// <summary> Initializes a new instance of the <see cref="DelegateCommand"/> class. </summary>
        /// <param name="_execute">Delegate to execute when Execute is called on the command.</param>
        /// <param name="_canExecute">Delegate to execute when CanExecute is called on the command.</param>
        /// <exception cref="ArgumentNullException">The execute argument must not be null.</exception>
        public DelegateCommand(Action<object> _execute, Func<object, bool> _canExecute)
        {
            if (null == _execute)
                throw new ArgumentNullException(nameof(_execute));

            m_Execute = _execute;
            m_CanExecute = _canExecute;
        }

        /// <summary> Occurs when changes occur that affect whether or not the command should execute. </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary> Defines the method that determines whether the command can execute in its current state. </summary>
        /// <param name="_parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns> true if this command can be executed; otherwise, false. </returns>
        public bool CanExecute(object _parameter) => null != m_CanExecute ? m_CanExecute(_parameter) : true;

        /// <summary> Defines the method to be called when the command is invoked. </summary>
        /// <param name="_parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <exception cref="InvalidOperationException">The <see cref="CanExecute"/> method returns <c>false.</c></exception>
        public void Execute(object _parameter)
        {
            if (!CanExecute(_parameter))
            {
                throw new InvalidOperationException("The command cannot be executed because the canExecute action returned false.");
            }

            m_Execute(_parameter);
        }

        /// <summary> Raises the <see cref="E:CanExecuteChanged"/> event. </summary>
        public void RaiseCanExecuteChanged() => OnCanExecuteChanged(EventArgs.Empty);

        /// <summary> Raises the <see cref="E:CanExecuteChanged"/> event. </summary>
        /// <param name="_e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnCanExecuteChanged(EventArgs _e)
        {
            EventHandler __handler = this.CanExecuteChanged;
            if (null != __handler) 
                __handler(this, _e); 
        }
    }
}
