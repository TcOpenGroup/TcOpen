using TcoInspectors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;
using System.Linq;

namespace Sandbox.TcoInspectors.Wpf
{    
    public class MainWindowViewModel 
    {
        public MainWindowViewModel()
        {            
        
        }
        
        public TcoInspectorsTwinController TcoInspectorsPlc { get; } = Entry.TcoInspectorsPlc;       
    }
}
