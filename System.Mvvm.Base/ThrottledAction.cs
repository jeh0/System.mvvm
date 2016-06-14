using System.Threading;
using System.Threading.Tasks;

namespace System.Mvvm.Base
{
    /// <summary> Defines the throttling mode. </summary>
    public enum ThrottledActionMode
    {
        /// <summary> Invokes the method at maximum once for the delay time. </summary>
        InvokeMaxEveryDelayTime,

        /// <summary> Invokes the method only if no invoke request was called for duration of the delay time. </summary>
        InvokeOnlyIfIdleForDelayTime
    }


    /// <summary> This class supports throttling of multiple method calls to improve the responsiveness of an application.
    /// It delays a method call and skips all additional calls of this method during the delay.
    /// The call of the action is synchronized. It uses the current synchronization context that was active during creating this class.
    /// </summary>
    /// <remarks>This class is thread-safe.</remarks>
    public class ThrottledAction
    {
        private readonly TaskScheduler m_TaskScheduler;
        private readonly object m_TimerLock = new object();
        private readonly Timer m_Timer;
        private readonly Action m_Action;
        private readonly ThrottledActionMode m_Mode;
        private readonly TimeSpan m_DelayTime;
        private volatile bool m_IsRunning;


        /// <summary> Initializes a new instance of the <see cref="ThrottledAction"/> class. </summary>
        /// <param name="action">The action that should be throttled.</param>
        /// <exception cref="ArgumentNullException">The argument action must not be null.</exception>
        public ThrottledAction(Action action)
            : this(action, ThrottledActionMode.InvokeMaxEveryDelayTime, TimeSpan.FromMilliseconds(10))
        {
        }

        /// <summary> Initializes a new instance of the <see cref="ThrottledAction"/> class. </summary>
        /// <param name="_action">The action that should be throttled.</param>
        /// <param name="_mode">Defines the throttling mode.</param>
        /// <param name="_delayTime">The delay time.</param>
        /// <exception cref="ArgumentNullException">The argument action must not be null.</exception>
        public ThrottledAction(Action _action, ThrottledActionMode _mode, TimeSpan _delayTime)
        {
            if (null == _action)
                throw new ArgumentNullException(nameof(_action));

            m_TaskScheduler = SynchronizationContext.Current != null ? TaskScheduler.FromCurrentSynchronizationContext() : TaskScheduler.Default;
            m_Timer = new Timer(TimerCallback);
            m_Action = _action;
            m_Mode = _mode;
            m_DelayTime = _delayTime;
        }


        /// <summary> Indicates that an execution of the action delegate is requested. </summary>
        public bool IsRunning => m_IsRunning;


        /// <summary> Requests the execution of the action delegate. </summary>
        public void InvokeAccumulated()
        {
            lock (m_TimerLock)
            {
                if (m_Mode == ThrottledActionMode.InvokeOnlyIfIdleForDelayTime || !m_IsRunning)
                {
                    m_IsRunning = true;
                    m_Timer.Change(m_DelayTime, Timeout.InfiniteTimeSpan);
                }
            }
        }

        /// <summary> Cancel the execution of the action delegate that was requested. </summary>
        public void Cancel()
        {
            lock (m_TimerLock)
            {
                m_IsRunning = false;
                m_Timer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
            }
        }

        private void TimerCallback(object state)
        {
            lock (m_TimerLock)
            {
                m_IsRunning = false;
            }

            Task.Factory.StartNew(m_Action, CancellationToken.None, TaskCreationOptions.DenyChildAttach, m_TaskScheduler);
        }
    }
}
