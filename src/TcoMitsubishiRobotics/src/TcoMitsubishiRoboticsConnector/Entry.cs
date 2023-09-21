using System;

namespace TcoMitsubishiRobotics
{
    public class Entry
    {
        static string amsId = Environment.GetEnvironmentVariable("Tc3Target");
        const int amsPort = 851;

        public static TcoMitsubishiRobotics.TcoMitsubishiRoboticsTwinController TcoMitsubishiRobotics { get; }
            = new TcoMitsubishiRobotics.TcoMitsubishiRoboticsTwinController(Vortex.Adapters.Connector.Tc3.Adapter.Tc3ConnectorAdapter.Create(amsId, amsPort, true));
    }
}
