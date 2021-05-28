using System;
using Vortex.Connector;

namespace TcoCore.Logging
{
    public class LogInfo
    {
        public string Symbol { get; set; }

        public string AttributeName { get; set; }

        public string HumanReadable { get; set; }

        public object Payload { get; set; }
        
        public static LogInfo Create(IVortexElement obj)
        {
            return new LogInfo()
            {
                AttributeName = obj.AttributeName,
                HumanReadable = obj.HumanReadable,
                Symbol = obj.Symbol,
                Payload = obj is IDecorateLog ? AcquirePayload(((IDecorateLog)obj).LogPayloadDecoration) : null
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
