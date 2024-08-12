using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TcoUtilities;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;

namespace Sandbox.TcoUtilities.Wpf
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel() { }

        public TcoUtilitiesTwinController TcoUtilitiesPlc { get; } = Entry.TcoUtilitiesPlc;
    }
}
