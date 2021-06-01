using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using TcOpen.Inxton.Abstractions.Data;

namespace TcOpen.Inxton.Data

{
    /// <summary>
    /// Base class for data repositories.
    /// </summary>
    /// <typeparam name="T">Type of data object.</typeparam>
    public abstract class RepositoryBase<T> : IRepository<T>, IRepository where T : IBrowsableDataObject
    {
        /// <summary>
        /// Creates a new record/document in the repository. (Concrete implementation of given repository type)
        /// </summary>
        /// <param name="identifier">Unique identifier of the record/repository</param>
        /// <param name="data">Data object.</param>
        protected abstract void CreateNvi(string identifier, T data);

        /// <summary>
        /// Reads en existing record/document from the repository. (Concrete implementation of given repository type)
        /// </summary>
        /// <param name="identifier">Unique identifier of the record/document to read.</param>
        /// <returns>Retrieved data object.</returns>
        protected abstract T ReadNvi(string identifier);

        /// <summary>
        /// Updates an existing record/document. (Concrete implementation of given repository type)
        /// </summary>
        /// <param name="identifier">Unique identifier of the record/document to update.</param>
        /// <param name="data">Data object with updated content.</param>
        protected abstract void UpdateNvi(string identifier, T data);

        /// <summary>
        /// Deletes an existing record/document. (Concrete implementation of given repository type)
        /// </summary>
        /// <param name="identifier">Unique identifier of the record/document to delete.</param>
        protected abstract void DeleteNvi(string identifier);

        /// <summary>
        /// Counts the records/documents in the repository. (Concrete implementation of given repository type)
        /// </summary>
        protected abstract long CountNvi { get; }

        /// <summary>
        /// Retrieves records/documents that contain given string in the identifier. (Concrete implementation of given repository type)
        /// </summary>
        /// <param name="identifierContent">String required to be contained in the identifier of records/documents.</param>
        /// <param name="limit">Limit of documents to retrieve.</param>
        /// <param name="skip">Number of documents to be skipped.</param>
        /// <returns></returns>
        protected abstract IEnumerable<T> GetRecordsNvi(string identifierContent, int limit, int skip);

        /// <summary>
        /// Counts records that contain given string in the id. (Concrete implementation of given repository type)
        /// </summary>
        /// <param name="identifierContent">String required to be contained in the identifier of the records/documents.</param>
        /// <returns></returns>
        protected abstract long FilteredCountNvi(string identifierContent);

        /// <summary>
        /// Gets the number of records/documents in the repository.
        /// </summary>
        public long Count { get { return this.CountNvi; } }

        /// <summary>
        /// Gets the count of the records/documents that contain given string in the identifier.
        /// </summary>
        /// <param name="identifierContent">String required to be contained in the identifier of the records/documents.</param>
        /// <returns></returns>
        public long FilteredCount(string identifierContent) => FilteredCountNvi(identifierContent);


        private volatile object mutex = new object();

        /// <summary>
        /// Creates a new record/document in the repository.
        /// </summary>
        /// <param name="identifier">Unique identifier of the record/repository</param>
        /// <param name="data">Data object.</param>
        public void Create(string identifier, T data)
        {
            lock (mutex)
            {
                if (data != null)
                {

                    if (string.IsNullOrEmpty(identifier))
                    {
                        identifier = DataHelpers.CreateUid().ToString();
                    }

                    data._Id = identifier;
                    data._Created = DateTime.Now;
                    data._Modified = data._Created;
                }

                CreateNvi(identifier, data);
            }
        }

        /// <summary>
        /// Reads en existing record/document from the repository.
        /// </summary>
        /// <param name="identifier">Unique identifier of the record/document to read.</param>
        /// <returns>Retrieved data object.</returns>
        public T Read(string identifier)
        {
            lock (mutex)
            {
                return ReadNvi(identifier);
            }
        }

        /// <summary>
        /// Updates an existing record/document.
        /// </summary>
        /// <param name="identifier">Unique identifier of the record/document to update.</param>
        /// <param name="data">Data object with updated content.</param>
        public void Update(string identifier, T data)
        {
            lock (mutex)
            {
                if (data != null)
                {
                    if (data._Id != identifier)
                    {
                        throw new IdentifierValueMismatchedException($"Record identifier '[_data._Id]' '{data._Id}' has different value than " +
                            $"requested identifier for update '{identifier}'\n" +
                            $"Value passed as 'identifier' must be the same as the value contained in the '_id' member of the data object.");
                    }

                    data._Modified = DateTimeProviders.DateTimeProvider.Now;
                }

                UpdateNvi(identifier, data);
            }
        }

        /// <summary>
        /// Deletes an existing record/document. (Concrete implementation of given repository type)
        /// </summary>
        /// <param name="identifier">Unique identifier of the record/document to delete.</param>
        public void Delete(string identifier)
        {
            lock (mutex)
            {
                DeleteNvi(identifier);
            }
        }

        void IRepository.Create(string identifier, object data) => Create(identifier, (T)data);

        dynamic IRepository.Read(string identifier) => Read(identifier);

        void IRepository.Update(string identifier, object data) => Update(identifier, (T)data);

        void IRepository.Delete(string identifier) => Delete(identifier);

        long IRepository.Count => this.Count;

        public IEnumerable<T> GetRecords(string identifier, int limit = 10, int skip = 0)
        {
            try
            {
                return GetRecordsNvi(identifier, limit, skip);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Gets <see cref="IQueryable"/> of given repository.
        /// </summary>
        public abstract IQueryable<T> Queryable { get; }
    }

    public class RepositoryNotInitializedException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="RepositoryNotInitializedException" /> class.</summary>
        public RepositoryNotInitializedException()
        {

        }

        /// <summary>Initializes a new instance of the <see cref="RepositoryNotInitializedException" /> class with a specified error message.</summary>
        /// <param name="message">The message that describes the error. </param>
        public RepositoryNotInitializedException(string message) : base(message)
        {

        }

        /// <summary>Initializes a new instance of the <see cref="RepositoryNotInitializedException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
        /// <param name="message">The error message that explains the reason for the exception. </param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified. </param>
        public RepositoryNotInitializedException(string message, Exception innerException) : base(message, innerException)
        {

        }

        /// <summary>Initializes a new instance of the <see cref="RepositoryNotInitializedException" /> class with serialized data.</summary>
        /// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
        /// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
        /// <exception cref="System.ArgumentNullException">The <paramref name="info" /> parameter is <see langword="null" />. </exception>
        /// <exception cref="System.Runtime.Serialization.SerializationException">The class name is <see langword="null" /> or <see cref="System.Exception.HResult" /> is zero (0). </exception>
        [SecuritySafeCritical]
        protected RepositoryNotInitializedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

    }
}
