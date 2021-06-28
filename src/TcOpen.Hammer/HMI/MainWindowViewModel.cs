using PlcHammerConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Presentation;

namespace PlcHammer.Hmi
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            SwiftRecorderViewModel = new TcOpen.Inxton.Swift.Wpf.SwiftRecorderViewModel(PlcHammer.TECH_MAIN._app._station001);
        }
        public PlcHammer.PlcHammerTwinController PlcHammer { get { return Entry.PlcHammer; } }     
        
        public TcOpen.Inxton.Swift.Wpf.SwiftRecorderViewModel SwiftRecorderViewModel { get; }
    }
}
