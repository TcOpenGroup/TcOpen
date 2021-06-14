using System;
using System.Linq;

namespace TcoCore.PexTests
{
    internal class TcoMomentaryTaskCustomizedCodeProvider : TcOpen.Inxton.Swift.ICodeProvider
    {
        public string Code(params object[] args)
        {
            return "customized code;";
        }
    }
}