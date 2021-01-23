using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TcoCoreTests;

namespace TcoCore.Sandbox.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() : base()
        {
            PlcTcoCoreTests.Connector.BuildAndStart();
        }

        public static TcoCoreTestsTwinController PlcTcoCoreTests { get { return Entry.PlcTcoCoreTests; } }




    }
}
