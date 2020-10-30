using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using TcOpen;

namespace PlcOpen.Wpf.Sandbox
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {          
            Entry.TcOpen.Connector.BuildAndStart().ReadWriteCycleDelay = 100;

            Entry.TcOpen.MAIN._wpfCyclinder.TimeToReachPosition = 100;
            Entry.TcOpen.MAIN._wpfCyclinder_1.TimeToReachPosition = 5000;
            Entry.TcOpen.MAIN._wpfCyclinder_2.TimeToReachPosition = 250;
            Entry.TcOpen.MAIN._wpfCyclinder_3.TimeToReachPosition = 50;           
        }
    }
}
