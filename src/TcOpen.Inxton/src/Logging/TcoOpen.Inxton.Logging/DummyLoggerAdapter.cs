using System;
using TcOpen.Inxton.Abstractions.Logging;

namespace TcOpen.Inxton.Logging
{
    /// <summary>
    /// Default logger implementation with no real logging capability. 
    /// Provides an empty implementation of logging for the framework when no other logger created.    
    /// </summary>
    public class DummyLoggerAdapter : ITcoLogger
    {
        public (string message, object payload, string serverity) LastMessage { get; private set; }

        public DummyLoggerAdapter()
        {
            
        }
        
        public void ClearLastMessage()
        {
            LastMessage = (string.Empty, null, string.Empty);
        }

        public bool IsLastMessageEmpty()
        {
            return LastMessage.message == string.Empty && LastMessage.payload == null && LastMessage.serverity == string.Empty;
        }

        public void Debug<T>(string stringTemplate, T payload = default)
        {
            LastMessage = (stringTemplate, payload, "Debug");
        }

        public void Error<T>(string stringTemplate, T payload = default)
        {
            LastMessage = (stringTemplate, payload, "Error");
        }

        public void Fatal<T>(string stringTemplate, T payload = default)
        {
            LastMessage = (stringTemplate, payload, "Fatal");
        }

        public void Information<T>(string stringTemplate, T payload = default)
        {
            LastMessage = (stringTemplate, payload, "Information");
        }

        public void Verbose<T>(string stringTemplate, T payload = default)
        {
            LastMessage = (stringTemplate, payload, "Verbose");
        }

        public void Warning<T>(string stringTemplate, T payload = default)
        {
            LastMessage = (stringTemplate, payload, "Warning");
        }
    }
}
