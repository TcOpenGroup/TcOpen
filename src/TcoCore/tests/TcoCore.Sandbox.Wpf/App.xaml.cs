using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TcoCoreExamples;

namespace TcoCore.Sandbox.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() : base()
        {            
            TcOpen.Inxton.TcoAppDomain.Current.Builder
                .SetUpLogger(new TcOpen.Inxton.Logging.SerilogAdapter(new LoggerConfiguration()
                                                        .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose)
                                                        .WriteTo.Notepad()
                                                        .MinimumLevel.Verbose()))
                .SetDispatcher(TcoCore.Wpf.Threading.Dispatcher.Get);


            PlcTcoCoreExamples.Connector.ReadWriteCycleDelay = 250;
            PlcTcoCoreExamples.Connector.BuildAndStart();

            PlcTcoCoreExamples.MANIPULATOR._context._logger.StartLoggingMessages(eMessageCategory.All);
            PlcTcoCoreExamples.EXAMPLES_PRG._context._logger.StartLoggingMessages(eMessageCategory.All);
            PlcTcoCoreExamples.MAIN._station001._logger.StartLoggingMessages(eMessageCategory.All);
            PlcTcoCoreExamples.EXAMPLES_PRG._loggerContext._loggerUsage._logger.StartLoggingMessages(eMessageCategory.All);
        }

        public static TcoCoreExamplesTwinController PlcTcoCoreExamples { get { return Entry.PlcTcoCoreExamples; } }




    }
}
