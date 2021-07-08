using PlcHammerConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcOpen.Inxton.Security.Wpf;
using TcOpen.Inxton.Security.Wpf.Login;
using Vortex.Presentation;

namespace PlcHammer.Hmi
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            SwiftRecorderViewModel = new TcOpen.Inxton.Swift.Wpf.SwiftRecorderViewModel(PlcHammer.TECH_MAIN._app._station001);
            Users = new UserManagementViewModel();
        }
        public PlcHammer.PlcHammerTwinController PlcHammer { get { return Entry.PlcHammer; } }     
        
        public TcOpen.Inxton.Swift.Wpf.SwiftRecorderViewModel SwiftRecorderViewModel { get; }

        public UserManagementViewModel Users { get; }

        public OpenLoginWindowCommand OpenLoginWindowCommand { get; } = new OpenLoginWindowCommand();


    }
}
