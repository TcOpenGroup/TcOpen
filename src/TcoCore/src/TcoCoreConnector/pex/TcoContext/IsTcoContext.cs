using System;
using System.Collections.Generic;
using System.Linq;
using Vortex.Connector.ValueTypes;

namespace TcoCore
{
    public interface IsTcoContext
    {
        /// <summary>
        /// Adds message to the queue of the messages of this Context.
        /// </summary>
        /// <param name="message">Message to add.</param>
        void AddMessage(TcoMessage message);
       
        /// <summary>
        /// Gets last know value of start cycle counter of this context.
        /// </summary>
        ulong LastStartCycleCount { get; }

        /// <summary>
        /// Gets start cycle counter of this <see cref="TcoContext"/>.
        /// </summary>
        OnlinerULInt StartCycleCount { get; }
    }
}
