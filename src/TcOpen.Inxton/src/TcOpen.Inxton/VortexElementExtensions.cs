using System;
using Vortex.Connector;

namespace TcOpen.Inxton.VortexElementExtensions
{
    public static class VortexElementExtensions
    {
        public static string GetNameOrSymbol(this IVortexElement element)
        {
            return string.IsNullOrEmpty(element.AttributeName) ? element.GetSymbolTail() : element.AttributeName;
        }

    }
}
