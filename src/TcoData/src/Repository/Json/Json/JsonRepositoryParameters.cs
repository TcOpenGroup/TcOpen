using TcOpen.Inxton.Data;

namespace TcoData.Repository.Json
{
    public class JsonRepositorySettings<T> : RepositorySettings
    {

        public JsonRepositorySettings(string repositoryLocation)
        {
            this.Location = repositoryLocation;
        }

        public string Location { get; private set; }
    }
}
