using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using PlcTemplate;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;

namespace Sandbox.PlcTemplate.Wpf
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel() { }

        public PlcTemplateTwinController PlcTemplatePlc { get; } = Entry.PlcTemplatePlc;
    }
}
