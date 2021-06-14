using System;
using System.Linq;

namespace TcoCore.PexTests
{
    internal class TcoToggleTaskCustomizedCodeProvider : TcOpen.Inxton.Swift.ICodeProvider
    {
        public string Code(params object[] args)
        {
            return "customized code;";
        }
    }
}