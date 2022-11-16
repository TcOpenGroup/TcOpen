using System.Linq;

namespace TcOpen.Inxton.Data.Merge.Base
{
    public class MergeEntityData<T> : MergeEntityDataBase<T> where T : IBrowsableDataObject, new()
    {           
        public override T GetEntityData(string id, IRepository<T> repository)
        {
            return  repository.Queryable.FirstOrDefault<T>(p => p._EntityId == id);
        }

        public IQueryable<T> GetEntities(IRepository<T> repository)
        {
            return repository.Queryable.Where(p => true);
        }


        public override void UpdateEntityData(T data, IRepository<T> repository)
        {
            repository.Update(data._EntityId,data);
        }      
    }
}
