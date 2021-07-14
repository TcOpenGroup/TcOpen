using System;
using Vortex.Connector;

namespace TcoCore.Logging
{
    public class LogInfo
    {
        private LogInfo()
        {

        }

        public string Symbol { get; private set; }

        public string AttributeName { get; private set; }

        public string HumanReadable { get; private set; }

        public object Details { get; private set; }
                
        public static LogInfo Create(IVortexElement obj)
        {
            return new LogInfo()
            {
                AttributeName = obj.AttributeName,
                HumanReadable = obj.HumanReadable,
                Symbol = obj.Symbol,
                Details = obj is IDecorateLog ? AcquirePayload(((IDecorateLog)obj).LogPayloadDecoration) : null
            };
        }

        private static object AcquirePayload(Func<object> acquirePayload)
        {
           try 
           { 
                return acquirePayload?.Invoke(); 
           } 
           catch 
           {            
                /*Swallow*/ 
           }

            return null;
        }

        public static string NameOrSymbol(IVortexElement obj)
        {
            return string.IsNullOrEmpty(obj.AttributeName) ? obj.GetSymbolTail() : obj.AttributeName;
        }
    }    
}
