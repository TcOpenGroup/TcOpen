using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcoCore.Logging
{
    public interface IDecorateLog
    {
        /// <summary>
        /// Gets or sets log payload decoration function. 
        /// The return object will can be added to provide additional information about this task execution.
        /// <note:important>
        /// There must be an implementation that calls and adds the result object into the log message payload.
        /// an example of the implementation can be found here <see cref="LogInfo.Create(IVortexElement)"/> (TcoCore.Logging.LogInfo.Create).
        /// How to create a payload decorator see in <see cref="TcoSequencer.PexConstructor(IVortexObject, string, string)"/>
        /// </important>
        /// </summary>
        Func<object> LogPayloadDecoration { get; set; }
    }
}
