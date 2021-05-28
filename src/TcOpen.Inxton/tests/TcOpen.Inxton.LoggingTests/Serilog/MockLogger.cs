using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Linq;

namespace TcOpen.Inxton.Logging.SerilogTests
{
    public static class MockLogger
    {
        public static LoggerConfiguration MockConsole(this LoggerSinkConfiguration sinkConfiguration, LogEventLevel restrictedToMinimumLevel = LogEventLevel.Verbose, string outputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}", IFormatProvider formatProvider = null, LoggingLevelSwitch levelSwitch = null, LogEventLevel? standardErrorFromLevel = null, ConsoleTheme theme = null)
        {
            return sinkConfiguration.Sink(new MockSink(), LogEventLevel.Verbose, new LoggingLevelSwitch(LogEventLevel.Verbose));
        }
    }
}