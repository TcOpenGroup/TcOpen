using System;
using TcOpen.Inxton.Abstractions.Logging;

namespace TcOpen.Inxton.Logging
{
    /// <summary>
    /// Default logger implementation with no real logging capability. 
    /// Provides an empty implementation of logging for the framework when no other logger created.    
    /// </summary>
    public class DummyLogger : ITcoLogger
    {        
        public DummyLogger()
        {
     
        }

        public void Debug<T>(string stringTemplate, T payload = default)
        {
            
        }

        public void Error<T>(string stringTemplate, T payload = default)
        {
            
        }

        public void Fatal<T>(string stringTemplate, T payload = default)
        {
            
        }

        public void Information<T>(string stringTemplate, T payload = default)
        {
            
        }

        public void Verbose<T>(string stringTemplate, T payload = default)
        {
            
        }

        public void Warning<T>(string stringTemplate, T payload = default)
        {
            
        }
    }
}
