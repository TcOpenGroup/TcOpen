using Serilog;
using System;
using TcOpen.Abstractions.Logging;
using Serilog.Formatting.Compact;
namespace TcoOpen.Logging
{
    public class SerilogLogger : ITcoLogger
    {
        public SerilogLogger(LoggerConfiguration configuration)
        {
            logger = configuration.CreateLogger();
        }

        public SerilogLogger()
        {
            logger = new LoggerConfiguration()
                                    .WriteTo
                                    .Console()                                                                        
                                    .CreateLogger();
        }

        private readonly Serilog.ILogger logger;

        public void Debug<T>(string stringTemplate, T payload = default)
        {
            logger.Debug<T>(stringTemplate, payload);
        }

        public void Verbose<T>(string stringTemplate, T payload = default)
        {
            logger.Verbose<T>(stringTemplate, payload);
        }

        public void Information<T>(string stringTemplate, T payload = default)
        {
            logger.Information<T>(stringTemplate, payload);
        }

        public void Warning<T>(string stringTemplate, T payload = default)
        {
            logger.Warning<T>(stringTemplate, payload);
        }

        public void Error<T>(string stringTemplate, T payload = default)
        {
            logger.Error<T>(stringTemplate, payload);
        }

        public void Fatal<T>(string stringTemplate, T payload = default)
        {
            logger.Fatal<T>(stringTemplate, payload);
        }
    }
}
