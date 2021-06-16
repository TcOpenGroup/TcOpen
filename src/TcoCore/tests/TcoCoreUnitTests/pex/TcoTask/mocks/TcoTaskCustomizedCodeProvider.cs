using System;
using System.Linq;
using Vortex.Connector;

namespace TcoCore.PexTests
{
    internal class TcoTaskCustomizedCodeProvider : TcOpen.Inxton.Swift.ICodeProvider
    {
        public IVortexObject Origin { get; }
        public string Code(params object[] args)
        {
            return "customized code;";
        }
    }
}