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
    public partial class TcoContext : IVortexIdentity
    {
        private readonly IList<TcoMessage> _messages = new List<TcoMessage>();
        public IEnumerable<TcoMessage> Messages { get { return _messages; } }

        public OnlinerULInt Identity => this._Identity;

        internal void AddMessage(TcoMessage message)
        {
            _messages.Add(message);
        }

        partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
        {
            this.Connector.IdentityProvider.AddIdentity(this);
        }
    }
}
