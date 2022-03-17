using TcoIoBeckhoffTests;
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
        public TcoIoBeckhoffTestsTwinController TcoIoBeckhoffTests { get; } = Entry.TcoIoBeckhoffTests;       
    }
}
