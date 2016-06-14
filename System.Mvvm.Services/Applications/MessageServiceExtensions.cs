namespace System.Mvvm.Services
{
    /// <summary> Provides method overloads for the <see cref="IMessageService"/> to simplify its usage. </summary>
    public static class MessageServiceExtensions
    {
        /// <summary> Shows the message. </summary>
        /// <param name="_service">The message service.</param>
        /// <param name="_message">The message.</param>
        /// <exception cref="ArgumentNullException">The argument service must not be null.</exception>
        public static void ShowMessage(this IMessageService _service, string _message)
        {
            if (null == _service) { throw new ArgumentNullException(nameof(_service)); }
            _service.ShowMessage(null, _message);
        }

        /// <summary> Shows the message as warning. </summary>
        /// <param name="_service">The message service.</param>
        /// <param name="_message">The message.</param>
        /// <exception cref="ArgumentNullException">The argument service must not be null.</exception>
        public static void ShowWarning(this IMessageService _service, string _message)
        {
            if (null == _service) { throw new ArgumentNullException(nameof(_service)); }
            _service.ShowWarning(null, _message);
        }

        /// <summary> Shows the message as error. </summary>
        /// <param name="_service">The message service.</param>
        /// <param name="_message">The message.</param>
        /// <exception cref="ArgumentNullException">The argument service must not be null.</exception>
        public static void ShowError(this IMessageService _service, string _message)
        {
            if (null == _service) { throw new ArgumentNullException(nameof(_service)); }
            _service.ShowError(null, _message);
        }

        /// <summary> Shows the specified question. </summary>
        /// <param name="_service">The message service.</param>
        /// <param name="_message">The question.</param>
        /// <returns><c>true</c> for yes, <c>false</c> for no and <c>null</c> for cancel.</returns>
        /// <exception cref="ArgumentNullException">The argument service must not be null.</exception>
        public static bool? ShowQuestion(this IMessageService _service, string _message)
        {
            if (null == _service) { throw new ArgumentNullException(nameof(_service)); }
            return _service.ShowQuestion(null, _message);
        }

        /// <summary> Shows the specified yes/no question. </summary>
        /// <param name="_service">The message service.</param>
        /// <param name="_message">The question.</param>
        /// <returns><c>true</c> for yes and <c>false</c> for no.</returns>
        /// <exception cref="ArgumentNullException">The argument service must not be null.</exception>
        public static bool ShowYesNoQuestion(this IMessageService _service, string _message)
        {
            if (null == _service) { throw new ArgumentNullException(nameof(_service)); }
            return _service.ShowYesNoQuestion(null, _message);
        }
    }
}
