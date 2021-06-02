using IntegrationProjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;
using System.Linq;

namespace Sandbox.IntegrationProjects.Wpf
{    
    public class MainWindowViewModel 
    {
        public MainWindowViewModel()
        {            
        
        }
        
        public IntegrationProjectsTwinController IntegrationProjectsPlc { get; } = Entry.IntegrationProjectsPlc;       
    }
}
