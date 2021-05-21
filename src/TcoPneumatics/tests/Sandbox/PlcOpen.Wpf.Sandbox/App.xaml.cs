using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using TcoPneumatics;
using TcoPneumaticsTests;

namespace TcoPneumatics.Wpf.Sandbox
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App():base()
        {
            TcoCore.Threading.Dispatcher.SetDispatcher(TcoCore.Wpf.Threading.Dispatcher.Get);
            Entry.TcoPneumaticsTestsPlc.Connector.BuildAndStart();           
        }
    }
}
