using TcoInspectors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;
using System.Linq;
using TcoInspectorsTests;

namespace Sandbox.TcoInspectors.Wpf
{    
    public class MainWindowViewModel 
    {
        public MainWindowViewModel()
        {            
        
        }
        
        public TcoInspectorsTestsTwinController TcoInspectorsPlc { get; } = App.Plc;       
    }
}
