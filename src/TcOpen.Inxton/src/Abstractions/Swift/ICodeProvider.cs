using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Connector;

namespace TcOpen.Inxton.Swift
{
    public interface ICodeProvider
    {
        string Code(params object[] args);
        
        IVortexObject Origin { get; }
    }
}
