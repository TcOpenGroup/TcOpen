using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Connector;

namespace TcOpen.Inxton.App.Logging
{
    internal static class LoggingHelpers
    {
        internal static void EditValueChange(IValueTag valueTag, dynamic original, dynamic newValue)
        {
            TcoAppDomain.Current.Logger.Information($"'{valueTag.Symbol}' value changed from '{original}' to '{newValue}' {{@payload}}",
                new { Path = valueTag.HumanReadable, Symbol = valueTag.Symbol });
        }
    }
}
