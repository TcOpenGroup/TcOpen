using System;
using Vortex.Adapters.Connector.Tc3.Adapter;

namespace TcoDrivesBeckhoff
{
    public class Entry
    {
        static string amsId = Environment.GetEnvironmentVariable("Tc3Target");
        const int amsPort = 851;

        public static TcoDrivesBeckhoff.TcoDrivesBeckhoffTwinController TcoDrivesBeckhoff { get; }
            = new TcoDrivesBeckhoff.TcoDrivesBeckhoffTwinController(Vortex.Adapters.Connector.Tc3.Adapter.Tc3ConnectorAdapter.Create(amsId, amsPort, true));
    }
}
