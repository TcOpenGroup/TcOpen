using PlcTemplate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;
using System.Linq;

namespace Sandbox.PlcTemplate.Wpf
{    
    public class MainWindowViewModel 
    {
        public MainWindowViewModel()
        {            
        
        }
        
        public PlcTemplateTwinController PlcTemplatePlc { get; } = Entry.PlcTemplatePlc;       
    }
}
