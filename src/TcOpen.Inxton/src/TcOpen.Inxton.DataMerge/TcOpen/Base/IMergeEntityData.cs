namespace TcOpen.Inxton.Data.Merge.Base
{
    public  interface IMergeEntityData<T> where T : IBrowsableDataObject
    {


        T GetData(string id, IRepository<T> repository);

        void UpdateData(T data , IRepository<T> repository);


    }
}
