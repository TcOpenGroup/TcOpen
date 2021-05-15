using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcoCore.Threading
{
    /// <summary>
    /// Provides access to UI dispatcher of currently running application.
    /// </summary>
    public class Dispatcher : IDispatcher
    {
        private Dispatcher()
        {
           
        }

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
        /// The appropriate dispatcher must be set at the start of the application. To set dispacher use <see cref="SetDispatcher(IDispatcher)"/> method.
        /// </summary>
        /// <param name="action">Action to run.</param>
        public void Invoke(Action action)
        {
            _dispatcher?.Invoke(action);
        }

        private volatile static object mutex = new object();
        private static Dispatcher _instance;

        /// <summary>
        /// Gets dispatcher mediator of currently running application.
        /// </summary>
        public static Dispatcher Get
        {
            get
            {
                lock (mutex)
                {
                    return _instance ?? new Dispatcher();
                }
            }
        }
    }
}
