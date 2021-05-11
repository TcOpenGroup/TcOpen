using TcoElements;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Sandbox.TcoElements.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            TcoCore.Threading.Dispatcher.SetDispatcher(TcoCore.Wpf.Threading.Dispatcher.Get);
            TcoElementsTests.Entry.TcoElementsTests.Connector.ReadWriteCycleDelay = 75;
            TcoElementsTests.Entry.TcoElementsTests.Connector.BuildAndStart();            
        }
    }
}
