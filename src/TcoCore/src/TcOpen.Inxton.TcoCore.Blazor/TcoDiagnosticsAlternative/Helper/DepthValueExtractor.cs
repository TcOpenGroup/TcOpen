using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Helper
{
    public static class DepthValueExtractor
    {
        public static int ExtractFromParentSymbol(string parentSymbol)
        {
            if (string.IsNullOrEmpty(parentSymbol))
            {
                return 0;
            }

            var depthArray = parentSymbol.Split('.');
            return depthArray.Length;
        }
    }
}
