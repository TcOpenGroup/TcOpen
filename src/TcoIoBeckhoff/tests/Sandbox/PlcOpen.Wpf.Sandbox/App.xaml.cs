using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using TcoPneumatics;

namespace TcoPneumatics.Wpf.Sandbox
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {          
            Entry.TcoPneumaticsPlc.Connector.BuildAndStart().ReadWriteCycleDelay = 100;

            Entry.TcoPneumaticsPlc.MAIN._wpfCyclinder.TimeToReachPosition = 100;
            Entry.TcoPneumaticsPlc.MAIN._wpfCyclinder_1.TimeToReachPosition = 5000;
            Entry.TcoPneumaticsPlc.MAIN._wpfCyclinder_2.TimeToReachPosition = 250;
            Entry.TcoPneumaticsPlc.MAIN._wpfCyclinder_3.TimeToReachPosition = 50;           
        }
    }
}
