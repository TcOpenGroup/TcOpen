using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcoCore;
using TcOpen.Inxton.Swift;
using Vortex.Connector;

namespace TcoCore.Swift
{
    public class TcoToggleTaskDefaultCodeProvider : ICodeProvider
    {
        public TcoToggleTaskDefaultCodeProvider(IVortexObject origin)
        {
            Origin = origin;
        }

        public IVortexObject Origin { get; }

        public string Code(params object[] args)
        {                                  
            return $"{Origin.Symbol}.Toggle()";                       
        }
    }
}
