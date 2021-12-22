using TcoUtilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;
using System.Linq;

namespace Sandbox.TcoUtilities.Wpf
{    
    public class MainWindowViewModel 
    {
        public MainWindowViewModel()
        {            
        
        }
        
        public TcoUtilitiesTwinController TcoUtilitiesPlc { get; } = Entry.TcoUtilitiesPlc;       
    }
}
