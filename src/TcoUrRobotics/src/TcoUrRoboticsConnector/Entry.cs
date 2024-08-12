using System;

namespace TcoUrRobotics
{
    public class Entry
    {
        static string amsId = Environment.GetEnvironmentVariable("Tc3Target");
        const int amsPort = 851;

        public static TcoUrRobotics.TcoUrRoboticsTwinController TcoUrRobotics { get; } =
            new TcoUrRobotics.TcoUrRoboticsTwinController(
                Vortex.Adapters.Connector.Tc3.Adapter.Tc3ConnectorAdapter.Create(
                    amsId,
                    amsPort,
                    true
                )
            );
    }
}
