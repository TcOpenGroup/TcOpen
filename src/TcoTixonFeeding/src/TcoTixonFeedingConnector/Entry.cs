using System;

namespace TcoTixonFeeding
{
    public class Entry
    {
        static string amsId = Environment.GetEnvironmentVariable("Tc3Target");
        const int amsPort = 851;

        public static TcoTixonFeeding.TcoTixonFeedingTwinController TcoTixonFeeding { get; } =
            new TcoTixonFeeding.TcoTixonFeedingTwinController(
                Vortex.Adapters.Connector.Tc3.Adapter.Tc3ConnectorAdapter.Create(
                    amsId,
                    amsPort,
                    true
                )
            );
    }
}
