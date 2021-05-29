using System;
using Vortex.Connector;

namespace TcoCore.Logging
{
    public class LogInfo
    {
        public string Symbol { get; set; }

        public string AttributeName { get; set; }

        public string HumanReadable { get; set; }

        public static LogInfo Create(IVortexElement obj)
        {
            return new LogInfo()
            {
                AttributeName = obj.AttributeName,
                HumanReadable = obj.HumanReadable,
                Symbol = obj.Symbol
            };
        }

        public static string NameOrSymbol(IVortexElement obj)
        {
            return string.IsNullOrEmpty(obj.AttributeName) ? obj.GetSymbolTail() : obj.AttributeName;
        }
    }    
}
