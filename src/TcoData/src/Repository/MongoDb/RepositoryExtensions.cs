using System;
using TcOpen.Inxton.Data;

namespace TcOpen.Inxton.Data.MongoDb
{
    public static class Repository
    {
        public static IRepository<T> Factory<T>(MongoDbRepositorySettings<T> parameters)
            where T : IBrowsableDataObject
        {
            try
            {
                return new MongoDbRepository<T>(parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Creation of MongoDb repository failed. Check number, type and value of parameters. For detail see inner exception.",
                    ex
                );
            }
        }
    }
}
