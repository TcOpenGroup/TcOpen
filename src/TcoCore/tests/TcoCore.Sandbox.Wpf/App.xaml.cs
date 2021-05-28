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
                .SetUpLogger(new TcOpen.Inxton.Logging.SerilogAdapter(new Serilog.LoggerConfiguration()
                                                                            .WriteTo.Console()));
                    

            TcoCore.Threading.Dispatcher.SetDispatcher(TcoCore.Wpf.Threading.Dispatcher.Get);
            PlcTcoCoreExamples.Connector.BuildAndStart();
        }

        public static TcoCoreExamplesTwinController PlcTcoCoreExamples { get { return Entry.PlcTcoCoreExamples; } }




    }
}
