using System.Diagnostics;
using System.Windows.Input;

namespace System.Mvvm.Base
{
    /// <summary> This RelayCommand is taken from MSDN magazine
    /// http://msdn.microsoft.com/en-us/magazine/dd419663.aspx#id0090030
    /// </summary>

    public class RelayCommand : ICommand
    {
        #region Fields

        readonly Action<object> m_Execute;
        readonly Predicate<object> m_CanExecute;

        #endregion // Fields

        #region Constructors

        public RelayCommand(Action<object> execute) : this(execute, null) { }
        public RelayCommand(Action<object> _execute, Predicate<object> _canExecute)
        {
            if (null == _execute)
                throw new ArgumentNullException(nameof(_execute));

            m_Execute = _execute;
            m_CanExecute = _canExecute;
        }

        #endregion // Constructors

        #region ICommand Members

        [DebuggerStepThrough] public bool CanExecute(object _parameter) => null == m_CanExecute ? true : m_CanExecute(_parameter);

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object _parameter) => m_Execute(_parameter);

        #endregion // ICommand Members
    }
}
