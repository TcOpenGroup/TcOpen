using Serilog.Core;
using Serilog.Events;
using System;
using System.Linq;

namespace TcOpen.Inxton.Logging.SerilogTests
{
    public class MockSink : ILogEventSink
    {
        public static string LastLogEntry { get; private set; }
        public void Emit(LogEvent logEvent)
        {
            LastLogEntry = $"{logEvent.Level}:{logEvent.RenderMessage()}";
        }
    }
}