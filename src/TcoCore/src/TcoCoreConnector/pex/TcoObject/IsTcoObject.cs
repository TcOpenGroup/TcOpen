using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Connector;

namespace TcoCore
{
    public interface IsTcoObject : IVortexObject
    {
        IEnumerable<PlainTcoMessage> GetActiveMessages();
    }
}
