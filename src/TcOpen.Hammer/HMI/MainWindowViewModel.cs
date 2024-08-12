using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HMI;
using PlcHammerConnector;
using TcOpen.Inxton.Local.Security.Wpf;
using Vortex.Connector;
using Vortex.Presentation;

namespace PlcHammer.Hmi
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            SwiftRecorderViewModel = new TcOpen.Inxton.Swift.Wpf.SwiftRecorderViewModel(
                PlcHammer.TECH_MAIN._app._station001
            );
            Users = new UserManagementViewModel();
        }

        private readonly bool IsDesignTime =
            System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject());

        public PlcHammer.PlcHammerTwinController PlcHammer
        {
            get { return IsDesignTime ? Entry.PlcHammerDesign : Entry.PlcHammer; }
        }

        public TcOpen.Inxton.Swift.Wpf.SwiftRecorderViewModel SwiftRecorderViewModel { get; }

        public UserManagementViewModel Users { get; }

        public OpenLoginWindowCommand OpenLoginWindowCommand { get; } =
            new OpenLoginWindowCommand();

        public object LogBox
        {
            get { return App.LogTextBox; }
        }
    }
}
