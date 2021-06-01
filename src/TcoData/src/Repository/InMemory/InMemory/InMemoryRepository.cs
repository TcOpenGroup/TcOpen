using System;
using System.Collections.Generic;
using System.Linq;
using TcOpen.Inxton.Abstractions.Data;
using TcOpen.Inxton.Data;
 
namespace TcoData.Repository.InMemory
{
    public class InMemoryRepository<T> : RepositoryBase<T> where T : IBrowsableDataObject
    {

        public InMemoryRepository(InMemoryRepositorySettings<T> parameters)
        {

        }

        private readonly Dictionary<string, T> _repository = new Dictionary<string, T>();
        internal Dictionary<string, T> Records
        {
            get { return this._repository; }
        }
      
        protected override void CreateNvi(string identifier, T data) 
        {
            try
            {                
                if (_repository.Any(p => p.Value.Equals(data)))
                {
                    throw new SameObjectReferenceException($"InMemory repository cannot contain two object with the same reference. You must create as new instance of '{nameof(T)}'");
                }
                                
                _repository.Add(identifier, data);                                                   
            }
            catch (ArgumentException argumentException)
            {
                throw new DuplicateIdException($"Record with ID {identifier} already exists in this collection.", argumentException);
            }
                                              
        }
        protected override T ReadNvi(string identifier)
        {
            try
            {
                return this._repository[identifier];
            }
            catch (Exception ex)
            {

                throw new UnableToLocateRecordId($"Unable to locate record with ID: {identifier} in {this.GetType()}.", ex);
            }
            
        }
        
        protected override void UpdateNvi(string identifier, T data)
        {
            try
            {
                if(data == null)
                {
                    throw new Exception("Data object cannot be 'null'");
                }

                var record = _repository.First(p => p.Key == identifier);
                this._repository[identifier] = data;
            }
            catch (Exception ex)
            {

                throw new UnableToUpdateRecord($"Unable to update record ID:{identifier} in {this.GetType()}.", ex);
            }
           
        }
        protected override void DeleteNvi(string identifier)
        {
            this._repository.Remove(identifier);
        }

        protected override long CountNvi
        {
            get { return this._repository.Count; }
        }

        protected override IEnumerable<T> GetRecordsNvi(string identifier, int limit, int skip)
        {
            if(identifier == "*")
            {
                return this.Records.Select(p => p.Value);
            }

            return this.Records.Where(p => p.Key.Contains(identifier)).Select(p => p.Value);
        }

        protected override long FilteredCountNvi(string id)
        {
            if (id == "*")
            {
                return this.Records.Select(p => true).Count();
            }

            return this.Records.Where(p => p.Key.Contains(id)).LongCount();
        }

        public override IQueryable<T> Queryable { get { return this._repository.AsQueryable().Select(p => p.Value); } }
    }    
}
