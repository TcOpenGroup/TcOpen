using TcoLoggers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;
using System.Linq;

namespace Sandbox.TcoLoggers.Wpf
{    
    public class MainWindowViewModel 
    {
        public MainWindowViewModel()
        {            
        
        }
        
        public TcoLoggersTwinController TcoLoggersPlc { get; } = Entry.TcoLoggersPlc;       
    }
}
