using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcoCore.Threading
{
    /// <summary>
    /// Provides access to the UI dispatcher of running application.
    /// </summary>
    public interface IDispatcher
    {
        /// <summary>
        /// Invokes an action on the dispatcher of the currently running application.
        /// </summary>
        /// <param name="action">Action to run</param>
        void Invoke(Action action);
    }
}
