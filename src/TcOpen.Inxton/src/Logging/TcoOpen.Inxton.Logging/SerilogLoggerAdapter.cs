namespace TcOpen.Inxton.Logging
{
    using Serilog;
    using TcOpen.Inxton.Abstractions.Logging;
    public class SerilogAdapter : ITcoLogger
    {
        /// <summary>
        /// Creates new instance of Serilog logger.
        /// </summary>
        /// <param name="configuration">Serilog logger configuration</param>
        public SerilogAdapter(LoggerConfiguration configuration)
        {
            _logger = configuration.CreateLogger();
        }


        /// <summary>
        /// Creates new instance of Serilog logger with default configuration:
        /// Write logs to Console (ConsoleSink)
        /// </summary>
        public SerilogAdapter()
        {
            _logger = new LoggerConfiguration()
                                    .WriteTo
                                    .Console()                                                                        
                                    .CreateLogger();
        }

        private readonly Serilog.ILogger _logger;
              

        public void Debug<T>(string stringTemplate, T payload = default)
        {
            _logger.Debug<T>(stringTemplate, payload);
        }

        public void Verbose<T>(string stringTemplate, T payload = default)
        {
            _logger.Verbose<T>(stringTemplate, payload);
        }

        public void Information<T>(string stringTemplate, T payload = default)
        {
            _logger.Information<T>(stringTemplate, payload);
        }

        public void Warning<T>(string stringTemplate, T payload = default)
        {
            _logger.Warning<T>(stringTemplate, payload);
        }

        public void Error<T>(string stringTemplate, T payload = default)
        {
            _logger.Error<T>(stringTemplate, payload);
        }

        public void Fatal<T>(string stringTemplate, T payload = default)
        {
            _logger.Fatal<T>(stringTemplate, payload);
        }
    }    
}
