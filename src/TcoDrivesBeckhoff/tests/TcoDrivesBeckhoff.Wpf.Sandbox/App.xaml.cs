using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TcoDrivesBeckhoff;
using TcoDrivesBeckhoffTests;

namespace TcoDrivesBeckhoff.Wpf.Sandbox
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() : base()
        {
            //Entry.TcoDrivesBeckhoff.Connector.BuildAndStart();
            TcoDrivesBeckhoffTests.Entry.TcoDrivesBeckhoffTestsPlc.Connector.BuildAndStart();
            TcoDrivesBeckhoffTests.Entry.TcoDrivesBeckhoffTestsPlc.MAIN._tcoDrivesBeckhoffContext._callMyPlcInstance.Synchron = false;
            TcoDrivesBeckhoffTests.Entry.TcoDrivesBeckhoffTestsPlc.MAIN._wpfContext._serviceModeActive.Synchron = true;
            this.ShutdownMode = ShutdownMode.OnLastWindowClose;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            
            try
            {
                TcoDrivesBeckhoffTests.Entry.TcoDrivesBeckhoffTestsPlc.MAIN._tcoDrivesBeckhoffContext._callMyPlcInstance.Synchron = false;
                TcoDrivesBeckhoffTests.Entry.TcoDrivesBeckhoffTestsPlc.MAIN._wpfContext._serviceModeActive.Synchron = false;
            }
            catch
            {
            }

            base.OnExit(e);
        }       
    }
}
