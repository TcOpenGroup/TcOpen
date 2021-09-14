using System;
using System.Collections.Generic;
using System.Linq;
using Vortex.Connector;
using Vortex.Connector.Identity;
using Vortex.Connector.ValueTypes;

namespace TcoCore
{
    public partial class TcoObject : IVortexIdentity, IsTcoObject
    {        
        private TcoContext _context;
        
        partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
        {
            Init(parent);
        }

        private void Init(IVortexObject parent)
        {
            this.Connector?.IdentityProvider.AddIdentity(this);
            this._context = parent?.GetParent<TcoContext>();
            this.MessageHandler = new TcoObjectMessageHandler(this._context, this);
        }
        
        /// <summary>
        /// Gets identity of this <see cref="TcoObject"/>.
        /// </summary>
        public OnlinerULInt Identity => this._Identity;

        /// <summary>
        /// Gets 'Message Handler' for this object.
        /// </summary>
        public TcoObjectMessageHandler MessageHandler { get; private set; }
    }
}
