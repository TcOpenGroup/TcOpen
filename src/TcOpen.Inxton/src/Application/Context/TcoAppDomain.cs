using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Logging;
using TcOpen.Inxton.Logging;
using TcOpen.Inxton.Threading;

namespace TcOpen.Inxton
{
    /// <summary>
    /// Provides basic entry point for various application functions.
    /// </summary>
    public class TcoAppDomain
    {
        /// <summary>
        /// Prevents creating of the instance from outside of this class.
        /// </summary>
        private TcoAppDomain()
        {
            
        }

        private static volatile object mutex = new object();
        private static TcoAppDomain _current;

        /// <summary>
        /// Gets current application domain.
        /// </summary>
        public static TcoAppDomain Current
        {
            get
            {
                if (_current == null)
                {
                    lock (mutex)
                    {
                        if (_current == null)
                        {
                            _current = new TcoAppDomain();
                            _current.Builder = new TcoAppBuilder(_current);
                        }
                    }
                }

                return _current;
            }
        }

        /// <summary>
        /// Gets logger for this application.
        /// </summary>
        public ITcoLogger Logger { get; internal set; } = new DummyLoggerAdapter();

        /// <summary>
        /// Gets dispatcher for this application.
        /// </summary>
        public IDispatcher Dispatcher { get { return Threading.Dispatcher.Get; } } 


        /// <summary>
        /// Gets application builder.
        /// </summary>
        public TcoAppBuilder Builder { get; private set; }
    }
}
