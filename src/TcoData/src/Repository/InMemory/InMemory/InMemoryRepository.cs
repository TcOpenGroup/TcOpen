using System;
using System.Collections.Generic;
using System.Linq;

namespace TcOpen.Inxton.Data.InMemory
{
    /// <summary>
    /// Provides in memory data repository.
    /// <note type="important">
    /// The data in this repository persist only during the run of the application.
    /// </note>
    /// </summary>
    /// <typeparam name="T">POCO twin type</typeparam>
    public class InMemoryRepository<T> : RepositoryBase<T> where T : IBrowsableDataObject
    {
        /// <summary>
        /// Creates new instance of <see cref="InMemoryRepository{T}"/>
        /// </summary>
        /// <param name="parameters">Repository settings</param>
        public InMemoryRepository(InMemoryRepositorySettings<T> parameters)
        {

        }

        /// <summary>
        /// Creates new instance of <see cref="InMemoryRepository{T}"/>
        /// </summary>
        public InMemoryRepository()
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

        protected override IEnumerable<T> GetRecordsNvi(string identifier, int limit, int skip, eSearchMode searchMode)
        {
            if(identifier == "*")
            {
                return this.Records.Select(p => p.Value);
            }
            
            switch (searchMode)
            {
                case eSearchMode.StartsWith:
                    return this.Records.Where(p => p.Key.StartsWith(identifier)).Select(p => p.Value);                    
                case eSearchMode.Contains:
                    return this.Records.Where(p => p.Key.Contains(identifier)).Select(p => p.Value);
                case eSearchMode.Exact:
                default:
                    return this.Records.Where(p => p.Key == identifier).Select(p => p.Value);                    
            }            
        }

        protected override long FilteredCountNvi(string id)
        {
            if (id == "*")
            {
                return this.Records.Select(p => true).Count();
            }

            return this.Records.Where(p => p.Key.Contains(id)).LongCount();
        }

        protected override bool ExistsNvi(string identifier)
        {
            return this.Records.Any(p => p.Key == identifier);
        }
        
        public override IQueryable<T> Queryable { get { return this._repository.AsQueryable().Select(p => p.Value); } }
    }    
}
