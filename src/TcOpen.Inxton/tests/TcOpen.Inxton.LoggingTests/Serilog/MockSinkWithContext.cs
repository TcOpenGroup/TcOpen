using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.IO;
using System.Linq;
using System.Text;
using TcOpen.Inxton.Security;

namespace TcOpen.Inxton.Logging.SerilogTests
{
    public class MockFormatSink
        : ILogEventSink
    {
        public string OutputTemplate { get; set; }

        public MockFormatSink(string outputTemplate)
        {
            OutputTemplate = outputTemplate;
        }

        public static string LastLogEntry { get; private set; }
        public void Emit(LogEvent logEvent)
        {
            ITextFormatter x = new Serilog.Formatting.Display.MessageTemplateTextFormatter(OutputTemplate);
            var stringWrite = new StringWriter();
            x.Format(logEvent, stringWrite);
            LastLogEntry = $"{logEvent.Level}:{stringWrite}".Trim();
        }

    }
}