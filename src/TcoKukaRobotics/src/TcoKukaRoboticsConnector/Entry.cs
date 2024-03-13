using System;

namespace TcoKukaRobotics
{
    public class Entry
    {
        static string amsId = Environment.GetEnvironmentVariable("Tc3Target");
        const int amsPort = 851;

        public static TcoKukaRobotics.TcoKukaRoboticsTwinController TcoKukaRobotics { get; }
            = new TcoKukaRobotics.TcoKukaRoboticsTwinController(Vortex.Adapters.Connector.Tc3.Adapter.Tc3ConnectorAdapter.Create(amsId, amsPort, true));
    }
}
