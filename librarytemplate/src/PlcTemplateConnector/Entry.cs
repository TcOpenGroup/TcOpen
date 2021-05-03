using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Adapters.Connector.Tc3.Adapter;

namespace PlcTemplate
{
    public static class Entry
    {
        public static PlcTemplate.PlcTemplateTwinController PlcTemplatePlc 
            = new PlcTemplateTwinController(Tc3ConnectorAdapter.Create(851, true));
    }
}
