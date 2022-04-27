using TcoIoTests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;
using System.Linq;

namespace Sandbox.TcoIo.Wpf
{    
    public class MainWindowViewModel 
    {
        public MainWindowViewModel()
        {            
        
        }
        public TcoIoTestsTwinController TcoIoTests { get; } = Entry.TcoIoTests;       
    }
}
