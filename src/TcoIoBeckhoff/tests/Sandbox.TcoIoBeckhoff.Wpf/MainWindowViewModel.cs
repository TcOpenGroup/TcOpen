using TcoIoBeckhoff;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;
using System.Linq;

namespace Sandbox.TcoIoBeckhoff.Wpf
{    
    public class MainWindowViewModel 
    {
        public MainWindowViewModel()
        {            
        
        }
        
        public TcoIoBeckhoffTwinController TcoIoBeckhoffPlc { get; } = Entry.TcoIoBeckhoffPlc;       
    }
}
