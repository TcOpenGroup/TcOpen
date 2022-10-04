using TcoCognexVision;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;
using System.Linq;

namespace Sandbox.TcoCognexVision.Wpf
{    
    public class MainWindowViewModel 
    {
        public MainWindowViewModel()
        {            
        
        }
        
        public TcoCognexVisionTwinController TcoCognexVisionPlc { get; } = Entry.TcoCognexVisionPlc;       
    }
}
