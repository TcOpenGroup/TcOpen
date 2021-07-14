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
    public class TcoMomentaryTaskDefaultCodeProvider : ICodeProvider
    {
        public TcoMomentaryTaskDefaultCodeProvider(IVortexObject origin)
        {
            Origin = origin;
        }

        public IVortexObject Origin { get; }

        public string Code(params object[] args)
        {            
            if(args.Length > 0)
            {
                bool result;
                if (bool.TryParse(args[0].ToString(), out result))
                {
                    return result ? $"{Origin.Symbol}.On()" : $"{Origin.Symbol}.Off()";
                }                                    
            }

            return "// Wrong type of number of arguments.";
        }
    }
}
