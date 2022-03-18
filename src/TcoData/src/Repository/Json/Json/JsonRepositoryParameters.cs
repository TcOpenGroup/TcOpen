using TcOpen.Inxton.Data;

namespace TcOpen.Inxton.Data.Json
{
    /// <summary>
    /// Provides <see cref="JsonRepository{T}"/> parameter
    /// </summary>
    /// <typeparam name="T">POCO twin type</typeparam>
    public class JsonRepositorySettings<T> : RepositorySettings
    {
        /// <summary>
        /// Creates new instance of <see cref="JsonRepositorySettings{T}"/>
        /// </summary>
        /// <param name="repositoryLocation">Location for the Json files</param>
        public JsonRepositorySettings(string repositoryLocation)
        {
            this.Location = repositoryLocation;
        }

        /// <summary>
        /// Gets location of Json file of the respective repository.
        /// </summary>
        public string Location { get; private set; }
    }
}
