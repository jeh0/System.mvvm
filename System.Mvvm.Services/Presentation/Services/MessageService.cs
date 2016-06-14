using System.ComponentModel.Composition;
using System.Globalization;
using System.Windows;
using static System.Windows.MessageBoxImage;
using static System.Windows.MessageBoxButton;
//using static System.Windows.MessageBoxOptions;

namespace System.Mvvm.Presentation
{
    using Services;

    /// <summary> This is the default implementation of the <see cref="IMessageService"/> interface. 
    ///  It shows messages via the MessageBox to the user.
    /// </summary>
    /// <remarks> If the default implementation of this service doesn't serve your need then you can provide your own implementation. </remarks>
    [Export(typeof(IMessageService))]
    public class MessageService : IMessageService
    {
        static MessageBoxResult MessageBoxResult => MessageBoxResult.None;
        static MessageBoxOptions MessageBoxOptions => CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft 
                    ? MessageBoxOptions.RtlReading : MessageBoxOptions.None;

        /// <summary> Shows the message. </summary>
        /// <param name="_owner">The window that owns this Message Window.</param>
        /// <param name="_message">The message.</param>
        public void ShowMessage(object _owner, string _message) => ShowBase(_owner, _message, None);

        /// <summary> Shows the message as warning. </summary>
        /// <param name="_owner">The window that owns this Message Window.</param>
        /// <param name="_message">The message.</param>
        public void ShowWarning(object _owner, string _message) => ShowBase(_owner, _message, Warning);

        /// <summary> Shows the message as error. </summary>
        /// <param name="_owner">The window that owns this Message Window.</param>
        /// <param name="_message">The message.</param>
        public void ShowError(object _owner, string _message) => ShowBase(_owner, _message, Error);

        /// <summary> Shows the specified question. </summary>
        /// <param name="_owner">The window that owns this Message Window.</param>
        /// <param name="_message">The question.</param>
        /// <returns><c>true</c> for yes, <c>false</c> for no and <c>null</c> for cancel.</returns>
        public bool? ShowQuestion(object _owner, string _message)
        {
            MessageBoxResult __result = ShowBase(_owner, _message, Question, YesNoCancel, MessageBoxResult.Cancel);

            if (MessageBoxResult.Yes == __result) return true;
            else if (MessageBoxResult.No == __result) return false;
            return null;
        }

        /// <summary> Shows the specified yes/no question. </summary>
        /// <param name="_owner">The window that owns this Message Window.</param>
        /// <param name="_message">The question.</param>
        /// <returns><c>true</c> for yes and <c>false</c> for no.</returns>
        public bool ShowYesNoQuestion(object _owner, string _message)
            => ShowBase(_owner, _message, Question, YesNo, MessageBoxResult.No) == MessageBoxResult.Yes;

        public MessageBoxResult ShowBase(object _owner, string _message
            , MessageBoxImage _messageBoxImage, MessageBoxButton _messageBoxButton = OK
            , MessageBoxResult _messageBoxResult = MessageBoxResult.None)
        {
            MessageBoxResult __result;

            var __ownerWindow = _owner as Window;
            if (null != __ownerWindow)
                __result = MessageBox.Show(__ownerWindow, _message, ApplicationInfo.ProductName, _messageBoxButton, _messageBoxImage, _messageBoxResult, MessageBoxOptions);
            else
                __result = MessageBox.Show(_message, ApplicationInfo.ProductName, _messageBoxButton, _messageBoxImage, _messageBoxResult, MessageBoxOptions);

            return __result;
        }
    } // --- MessageService : class ---
}