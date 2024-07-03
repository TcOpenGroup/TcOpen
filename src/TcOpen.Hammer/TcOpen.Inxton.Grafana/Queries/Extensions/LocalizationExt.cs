using PlcHammerConnector.Properties;

namespace Grafana.Backend.Queries
{
    internal static class LocalizationExt
    {
        internal static string Translate(this string @string) =>
            Vortex
                .Localizations.Abstractions.ITranslator.Get(typeof(Localizations))
                .Translate(@string);
    }
}
