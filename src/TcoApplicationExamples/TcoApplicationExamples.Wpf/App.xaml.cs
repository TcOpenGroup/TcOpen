using PlcAppExamples;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TcoApplicationExamples.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() : base()
        {
            TcoCore.Threading.Dispatcher.SetDispatcher(TcoCore.Wpf.Threading.Dispatcher.Get);
            PlcAppExamples.Connector.BuildAndStart().ReadWriteCycleDelay = 100;
        }

        public static PlcAppExamplesTwinController PlcAppExamples { get { return Entry.PlcAppExamples; } }
    }
}
