using System;
using System.Threading.Tasks;

namespace TcOpen.Inxton.Threading
{
    /// <summary>
    /// Provides access to UI dispatcher of currently running application.
    /// </summary>
    public class Dispatcher : IDispatcher
    {
        private Dispatcher() { }

        private static IDispatcher _dispatcher;

        /// <summary>
        /// Sets the dispatcher for running application.
        /// </summary>
        /// <param name="dispatcher"></param>
        public static void SetDispatcher(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// Invokes an action on the UI dispatcher of the currently running application.
        /// The appropriate dispatcher must be set at the start of the application. To set dispatcher use <see cref="SetDispatcher(IDispatcher)"/> method.
        /// </summary>
        /// <param name="action">Action to run.</param>
        public void Invoke(Action action)
        {
            _dispatcher?.Invoke(action);
        }

        // <summary>
        /// Invokes an action on the UI dispatcher of the currently running application asynchronously
        /// The appropriate dispatcher must be set at the start of the application. To set dispatcher use <see cref="SetDispatcher(IDispatcher)"/> method.
        /// </summary>
        /// <param name="action">Action to run.</param>
        public Task InvokeAsync(Action action)
        {
            return _dispatcher?.InvokeAsync(action);
        }

        private static volatile object mutex = new object();

        /// <summary>
        /// Gets dispatcher mediator of currently running application.
        /// </summary>
        public static IDispatcher Get
        {
            get
            {
                lock (mutex)
                {
                    return _dispatcher ?? new Dispatcher();
                }
            }
        }
    }
}
