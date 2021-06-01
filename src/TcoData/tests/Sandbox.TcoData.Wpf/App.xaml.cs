using TcoData;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Sandbox.TcoData.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //TcoCore.Threading.Dispatcher.SetDispatcher(TcoCore.Wpf.Threading.Dispatcher.Get);
            Entry.TcoDataPlc.Connector.BuildAndStart().ReadWriteCycleDelay = 75;
        }
    }
}
