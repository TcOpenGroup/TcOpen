using System;

namespace TcoAbbRobotics
{
    public class Entry
    {
        static string amsId = Environment.GetEnvironmentVariable("Tc3Target");
        const int amsPort = 851;

        public static TcoAbbRobotics.TcoAbbRoboticsTwinController TcoAbbRobotics { get; }
            = new TcoAbbRobotics.TcoAbbRoboticsTwinController(Vortex.Adapters.Connector.Tc3.Adapter.Tc3ConnectorAdapter.Create(amsId, amsPort, true));
    }
}
