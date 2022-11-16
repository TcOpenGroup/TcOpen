namespace TcOpen.Inxton.Data.Merge.Base
{
    public abstract class MergeEntityDataBase<T> : IMergeEntityData<T> where T : IBrowsableDataObject
    {

        
        public abstract T GetEntityData(string id, IRepository<T> repository);
        public abstract void UpdateEntityData(T data, IRepository<T> repository);



        T IMergeEntityData<T>.GetData(string id, IRepository<T> repository)
        {
            return GetEntityData(id, repository);
        }

        void IMergeEntityData<T>.UpdateData(T data, IRepository<T> repository)
        {
            UpdateEntityData(data, repository);
        }      
    }
}
