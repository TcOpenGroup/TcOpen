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
            
        }
        public PlcHammer.PlcHammerTwinController PlcHammer { get { return Entry.PlcHammer; } }        
    }
}
