using Raven.Client.Documents;
using TcOpen.Inxton.Data;

namespace TcOpen.Inxton.RavenDb
{
    public class RavenDbRepositorySettingsBase<T> : RepositorySettings where T : IBrowsableDataObject
    {
        public IDocumentStore Store { get; set; }
    }
}
