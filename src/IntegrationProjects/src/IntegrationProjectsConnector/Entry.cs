using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Adapters.Connector.Tc3.Adapter;

namespace IntegrationProjects
{
    public static class Entry
    {
        static readonly string AmsId = Environment.GetEnvironmentVariable("Tc3Target");

        public static IntegrationProjects.IntegrationProjectsTwinController IntegrationProjectsPlc 
            = new IntegrationProjectsTwinController(Tc3ConnectorAdapter.Create(AmsId, 851, true));
    }
}
