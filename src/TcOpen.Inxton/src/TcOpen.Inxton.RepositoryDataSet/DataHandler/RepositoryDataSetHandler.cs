using TcOpen.Inxton.Data;

namespace TcOpen.Inxton.RepositoryDataSet
{
    public class RepositoryDataSetHandler<T>
        where T : class, new()
    {
        /// <summary>
        /// Creates new instance of <see cref="RepositorySetDataHandler"/>
        /// </summary>
        /// <param name="repository"></param>
        private RepositoryDataSetHandler(IRepository<EntitySet<T>> repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Creates new instance of <see cref="RepositorySetDataHandler"/>
        /// </summary>
        /// <param name="repository">Instructions repository</param>
        /// <returns>New instnace of <see cref="RepositorySetDataHandler"/></returns>
        public static RepositoryDataSetHandler<T> CreateSet(IRepository<EntitySet<T>> repository)
        {
            return new RepositoryDataSetHandler<T>(repository);
        }

        /// <summary>
        /// Gets the repository of instructor data sets.
        /// </summary>
        public IRepository<EntitySet<T>> Repository { get; }

        /// <summary>
        /// Loads instruction set from the repository to this <see cref="ProductionSet"/> of this <see cref="RepositorySetDataHandler"/>.
        /// </summary>
        /// <param name="itemSetId">Instruction set identifier.</param>
        /// <returns>Instruction set.</returns>s
        public EntitySet<T> Read(string itemSetId)
        {
            return Repository.Read(itemSetId);
        }

        /// <summary>
        /// Creates new set.
        /// </summary>
        /// <param name="itemSetId">Instruction set id</param>
        public void Create(string itemSetId, EntitySet<T> itemsSet)
        {
            Repository.Create(itemSetId, itemsSet);
        }

        /// <summary>
        /// Updates the set.
        /// </summary>
        /// <param name="itemSetId">Instruction set id.</param>
        /// <param name="itemsSet">Instruction set.</param>
        public void Update(string itemSetId, EntitySet<T> itemsSet)
        {
            Repository.Update(itemSetId, itemsSet);
        }
    }
}
