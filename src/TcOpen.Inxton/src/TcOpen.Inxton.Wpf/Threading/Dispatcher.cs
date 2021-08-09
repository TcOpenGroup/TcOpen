using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TcOpen.Inxton.Threading;

namespace TcoCore.Wpf.Threading
{
    /// <summary>
    /// Provides access to the <see cref="System.Windows.Threading.Dispatcher"/> of currently running WPF application.
    /// </summary>
    public class Dispatcher : IDispatcher
    {
        private System.Windows.Threading.Dispatcher dispatcher { get { return Application.Current.Dispatcher; } }

        /// <summary>
        /// Invokes action on the thread of the current WPF application dispatcher.
        /// </summary>
        /// <param name="action">Action to execute</param>
        public void Invoke(Action action)
        {
            dispatcher.Invoke(action);
        }

        /// <summary>
        /// Invokes action on the thread of the current WPF application dispatcher asynchronously.
        /// </summary>
        /// <param name="action">Action to execute</param>
        public async Task InvokeAsync(Action action)
        {
            await dispatcher.InvokeAsync(action);
        }

        private volatile static object mutex = new object();
        private static Dispatcher _instance;

        /// <summary>
        /// Gets instance of WPF mediator dispatcher.
        /// </summary>
        public static Dispatcher Get 
        { 
            get
            {
                lock(mutex)
                {                   
                    if(_instance == null)
                    {
                        _instance = new Dispatcher();
                    }

                    return _instance;
                }
            }
        }
    }
}
