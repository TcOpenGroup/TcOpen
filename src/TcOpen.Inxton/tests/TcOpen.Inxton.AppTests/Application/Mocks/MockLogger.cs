using System;
using System.Linq;
using TcOpen.Inxton.Abstractions.Logging;

namespace TcOpen.Inxton.AppTests
{
    public class MockLogger : ITcoLogger
    {
        public string LastLog { get; private set; }
        public void Debug<T>(string stringTemplate, T payload = default)
        {
            LastLog = $"DEBUG:{stringTemplate}[{payload.ToString()}]";
        }

        public void Error<T>(string stringTemplate, T payload = default)
        {
            LastLog = $"ERROR:{stringTemplate}[{payload.ToString()}]";
        }

        public void Fatal<T>(string stringTemplate, T payload = default)
        {
            LastLog = $"FATAL:{stringTemplate}[{payload.ToString()}]";
        }

        public void Information<T>(string stringTemplate, T payload = default)
        {
            LastLog = $"INFO:{stringTemplate}[{payload.ToString()}]";
        }

        public void Verbose<T>(string stringTemplate, T payload = default)
        {
            LastLog = $"VERB:{stringTemplate}[{payload.ToString()}]";
        }

        public void Warning<T>(string stringTemplate, T payload = default)
        {
            LastLog = $"WARN:{stringTemplate}[{payload.ToString()}]";
        }
    }
}