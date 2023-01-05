using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using Serilog;
using System;

using System.Windows;
using System.Windows.Media;
using TcoCoreExamples;
using TcOpen.Inxton;
using TcOpen.Inxton.TcoCore.Wpf;
using Vortex.Adapters.Connector.Tc3.Adapter;

namespace TcoCore.Sandbox.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
         
            Color primaryColor = SwatchHelper.Lookup[MaterialDesignColor.DeepPurple];
            Color accentColor = SwatchHelper.Lookup[MaterialDesignColor.Lime];
            ITheme theme = Theme.Create(new MaterialDesignLightTheme(), primaryColor, accentColor);
            Resources.SetTheme(theme);




            base.OnStartup(e);
        }
        public App() : base()
        {
           
            PlcTcoCoreExamples.Connector.ReadWriteCycleDelay = 250;
            PlcTcoCoreExamples.Connector.BuildAndStart();

            TcOpen.Inxton.TcoAppDomain.Current.Builder
                .SetUpLogger(new TcOpen.Inxton.Logging.SerilogAdapter(new LoggerConfiguration()
                                                        .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose)
                                                        .WriteTo.Notepad()
                                                        .MinimumLevel.Verbose()))
                .SetDispatcher(TcoCore.Wpf.Threading.Dispatcher.Get)
                .SetPlcDialogs(DialogProxyServiceWpf.Create(new[] { PlcTcoCoreExamples.EXAMPLES_PRG._diaglogsContext }));

            
            PlcTcoCoreExamples.MANIPULATOR._context._logger.StartLoggingMessages(eMessageCategory.All);
            PlcTcoCoreExamples.EXAMPLES_PRG._context._logger.StartLoggingMessages(eMessageCategory.All);
            PlcTcoCoreExamples.MAIN._station001._logger.StartLoggingMessages(eMessageCategory.All);
            PlcTcoCoreExamples.EXAMPLES_PRG._loggerContext._loggerUsage._logger.StartLoggingMessages(eMessageCategory.All);
        }

        
        private static string AMS_ID = Environment.GetEnvironmentVariable("Tc3Target");
        private static volatile object mutex = new object();

        public static TcoCoreExamplesTwinController _plc;        
        
        /// <summary>
        /// Gets Plc twin for this application.
        /// </summary>
        public static TcoCoreExamplesTwinController PlcTcoCoreExamples
        {
            get
            {
                if (_plc == null)
                {
                    lock (mutex)
                    {
                        if (_plc == null)
                        {
                            _plc = CreateTwin();
                        }
                    }
                }

                return _plc;
            }
        }

        /// <summary>
        /// Creates twin connector for runtime or design mode.
        /// </summary>
        /// <returns>Plc twin</returns>
        private static TcoCoreExamplesTwinController CreateTwin()
        {
            if (!IsInDesign)
            {
                return new TcoCoreExamplesTwinController(Tc3ConnectorAdapter.Create(AMS_ID, 853, true));
            }
            else
            {
                return new TcoCoreExamplesTwinController(new Vortex.Connector.ConnectorAdapter(typeof(Vortex.Connector.DummyConnectorFactory)), new object[] { string.Empty });
            }
        }

        /// <summary>
        /// Gets true when running in design mode
        /// </summary>
        private static bool IsInDesign
        {
            get
            {
                return System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject());
            }
        }
    }
}
