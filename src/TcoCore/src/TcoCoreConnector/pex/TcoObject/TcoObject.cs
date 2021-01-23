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
    public partial class TcoObject : IVortexIdentity
    {
        public OnlinerULInt Identity => this._Identity;

        partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
        {
            this.Connector.IdentityProvider.AddIdentity(this);
        }
    }
}
