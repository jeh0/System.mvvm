using System.Security.Permissions;
using System.Windows.Threading;

namespace System.Mvvm.Applications
{
    internal static class DispatcherHelper
    {
        /// <summary> Execute the event queue of the dispatcher. </summary>
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        internal static void DoEvents()
        {
            var __frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(ExitFrame), __frame);
            Dispatcher.PushFrame(__frame);
        }

        static object ExitFrame(object __frame)
        {
            ((DispatcherFrame)__frame).Continue = false;
            return null;
        }
    }
}
