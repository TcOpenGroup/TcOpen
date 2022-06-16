using TcoElements;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TcOpen.Inxton.Local.Security;

namespace Sandbox.TcoElements.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            TcOpen.Inxton.TcoAppDomain.Current.Builder
                 .SetUpLogger(new TcOpen.Inxton.Logging.SerilogAdapter())
                 .SetDispatcher(TcoCore.Wpf.Threading.Dispatcher.Get)
                 .SetSecurity(TcOpen.Inxton.Local.Security.SecurityManager.CreateDefault());

            if(SecurityManager.Manager.UserRepository.Count == 0)
            {
                SecurityManager.Manager.UserRepository.Create("default", new UserData("default", "", new string[] { "Administrator" }));
            }

            TcoElementsTests.Entry.TcoElementsTests.Connector.ReadWriteCycleDelay = 75;

            System.Threading.Thread.Sleep(1000);

            TcoElementsTests.Entry.TcoElementsTests.Connector.BuildAndStart();            
        }
    }
}
