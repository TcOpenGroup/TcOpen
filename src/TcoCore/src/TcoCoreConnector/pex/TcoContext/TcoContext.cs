using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Connector;
using Vortex.Connector.Identity;
using Vortex.Connector.ValueTypes;

namespace TcoCore
{
    public partial class TcoContext : IVortexIdentity, IsTcoContext, IsTcoObject
    {
        private readonly IList<TcoMessage> _messages = new List<TcoMessage>();

        /// <summary>
        /// Gets the messages that belong to this Context.
        /// </summary>
        public IEnumerable<TcoMessage> Messages { get { return _messages; } }
        
        /// <summary>
        /// Gets active messages of this context.
        /// <note type="important">
        /// Depending on the depth and size of the context this might be performance demanding operation.
        /// </note>        
        /// </summary>
        public IEnumerable<PlainTcoMessage> ActiveMessages { get { return GetActiveMessages(); } }

        /// <summary>
        /// Get the identity of this Context.
        /// </summary>
        public OnlinerULInt Identity => this._Identity;

        /// <summary>
        /// Gets start cycle counter of this <see cref="TcoContext"/>.
        /// </summary>
        public OnlinerULInt StartCycleCount => this._startCycleCount;

        /// <summary>
        /// Adds message to the queue of the messages of this Context.
        /// </summary>
        /// <param name="message">Message to add.</param>
        public void AddMessage(TcoMessage message)
        {
            _messages.Add(message);
        }
        
        partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
        {
            this.Connector.IdentityProvider.AddIdentity(this);
            this.MessageHandler = new TcoObjectMessageHandler(this, this);
        }
     
        /// <summary>
        /// Gets last know value of start cycle counter of this context.
        /// </summary>
        public ulong LastStartCycleCount => this._startCycleCount.LastValue;

        /// <summary>
        /// Gets active messages of this context.
        /// <note type="important">
        /// Depending on the depth and size of the context this might be performance demanding operation.
        /// </note>        
        /// </summary>        
        public IEnumerable<PlainTcoMessage> GetActiveMessages()
        {
            return MessageHandler.GetActiveMessages();
        }

        /// <summary>
        /// Gets 'Message Handler' for this Context.
        /// </summary>
        public TcoObjectMessageHandler MessageHandler { get; private set; }
    }
}
