using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;

namespace TcOpen.Inxton.Data
{
    /// <summary>
    /// Base class for data repositories.
    /// </summary>
    /// <typeparam name="T">Type of data object.</typeparam>
    public abstract class RepositoryBase<T> : IRepository<T>, IRepository where T : IBrowsableDataObject
    {
        #region On CRUD delegates
        /// <summary>
        /// Gets or sets delegate that executes prior to new entry into repository.
        /// </summary>
        public OnCreateDelegate<T> OnCreate
        {
            get => onCreate;
            set
            {
                if (onCreate != null && value != null) throw new DelegateAlreadySetException();
                onCreate = value;
            }
        }
        private OnCreateDelegate<T> onCreate;

        /// <summary>
        /// Gets or sets delegate that executes prior to new reading data from repository
        /// </summary>
        public OnReadDelegate OnRead
        {
            get => onRead;
            set
            {
                if (onRead != null && value != null) throw new DelegateAlreadySetException();
                onRead = value;
            }
        }
        private OnReadDelegate onRead;

        /// <summary>
        /// Gets or sets delegate that executes prior to updating existing record.
        /// </summary>
        public OnUpdateDelegate<T> OnUpdate
        {
            get => onUpdate;
            set
            {
                if (onUpdate != null && value != null) throw new DelegateAlreadySetException();
                onUpdate = value;
            }
        }
        private OnUpdateDelegate<T> onUpdate;

        /// <summary>
        /// Gets or sets delegate that executes prior to deleting existing record.
        /// </summary>
        public OnDeleteDelegate OnDelete
        {
            get => onDelete;
            set
            {
                if (onDelete != null && value != null) throw new DelegateAlreadySetException();
                onDelete = value;
            }
        }
        private OnDeleteDelegate onDelete;
        #endregion

        #region On CRUD Done delegates
        /// <summary>
        /// Gets or sets delegate that executes after a new entry has been added sucesfully.
        /// </summary>
        public OnCreateDoneDelegate<T> OnCreateDone
        {
            get => onCreateDone;
            set
            {
                if (onCreateDone != null && value != null) throw new DelegateAlreadySetException();
                onCreateDone = value;
            }
        }
        private OnCreateDoneDelegate<T> onCreateDone;

        /// <summary>
        /// Gets or sets delegate that executes  after an entry has been read sucesfully.
        /// </summary>
        public OnReadDoneDelegate<T> OnReadDone
        {
            get => onReadDone;
            set
            {
                if (onReadDone != null && value != null) throw new DelegateAlreadySetException();
                onReadDone = value;
            }
        }
        private OnReadDoneDelegate<T> onReadDone;

        /// <summary>
        /// Gets or sets delegate that executes  after an entry has been updated sucesfully.
        /// </summary>
        public OnUpdateDoneDelegate<T> OnUpdateDone
        {
            get => onUpdateDone;
            set
            {
                if (onUpdateDone != null && value != null) throw new DelegateAlreadySetException();
                onUpdateDone = value;
            }
        }
        private OnUpdateDoneDelegate<T> onUpdateDone;

        /// <summary>
        /// Gets or sets delegate that executes  after an entry has been deleted sucesfully.
        /// </summary>
        public OnDeleteDoneDelegate OnDeleteDone
        {
            get => onDeleteDone;
            set
            {
                if (onDeleteDone != null && value != null) throw new DelegateAlreadySetException();
                onDeleteDone = value;
            }
        }
        private OnDeleteDoneDelegate onDeleteDone;
        #endregion

        #region On CRUD Failed delegates
        /// <summary>
        /// Gets or sets delegate that executes after a new entry has NOT been added sucesfully.
        /// </summary>
        public OnCreateFailedDelegate<T> OnCreateFailed
        {
            get => onCreateFailed;
            set
            {
                if (onCreateFailed != null && value != null) throw new DelegateAlreadySetException();
                onCreateFailed = value;
            }
        }
        private OnCreateFailedDelegate<T> onCreateFailed;

        /// <summary>
        /// Gets or sets delegate that executes  after an entry has NOT been read sucesfully.
        /// </summary>
        public OnReadFailedDelegate OnReadFailed
        {
            get => onReadFailed;
            set
            {
                if (onReadFailed != null && value != null) throw new DelegateAlreadySetException();
                onReadFailed = value;
            }
        }
        private OnReadFailedDelegate onReadFailed;

        /// <summary>
        /// Gets or sets delegate that executes  after an entry has NOT been updated sucesfully.
        /// </summary>
        public OnUpdateFailedDelegate<T> OnUpdateFailed
        {
            get => onUpdateFailed;
            set
            {
                if (onUpdateFailed != null && value != null) throw new DelegateAlreadySetException();
                onUpdateFailed = value;
            }
        }
        private OnUpdateFailedDelegate<T> onUpdateFailed;

        /// <summary>
        /// Gets or sets delegate that executes  after an entry has NOT been deleted sucesfully.
        /// </summary>
        public OnDeleteFailedDelegate OnDeleteFailed
        {
            get => onDeleteFailed;
            set
            {
                if (onDeleteFailed != null) throw new DelegateAlreadySetException();
                onDeleteFailed = value;
            }
        }
        private OnDeleteFailedDelegate onDeleteFailed;
        #endregion

        /// <summary>
        /// Gets or set validation delegate for updating data in this repository.
        /// </summary>
        /// <remarks>			
        ///		<note type = "warning" >
        ///           Validation condition is executed only upon update from the user interface.
        ///           Direct call of <see cref="Update(string, T)"/> does not validate the data.
        ///           The data are also not validate when called from TcoCore.TcoRemoteTask/>
        ///		</note>
        /// </remarks>
        /// <example>      
        ///       repository.OnRecordUpdateValidation = (data) => 
        ///       {
        ///           return new DataValidation[]
        ///               {
        ///                   new DataValidation($"'{nameof(data.sampleData.SampleInt)}' must be greater than 0", data.sampleData.SampleInt <= 0),
        ///                   new DataValidation($"'{nameof(data.sampleData.SampleInt2)}' must be less than 0", data.sampleData.SampleInt2 > 0)
        ///               };
        ///        };
        /// </example>
        public ValidateDataDelegate<T> OnRecordUpdateValidation
        {
            get;
            set;

        } = (data) => new DataItemValidation[] { };

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
        /// Checks that the record with given identifier exists in the repository.
        /// </summary>
        /// <param name="identifier">Entity id to check for existence.</param>
        protected abstract bool ExistsNvi(string identifier);

        /// <summary>
        /// Retrieves records/documents that contain given string in the identifier. (Concrete implementation of given repository type)
        /// </summary>
        /// <param name="identifierContent">String required to be contained in the identifier of records/documents.</param>
        /// <param name="limit">Limit of documents to retrieve.</param>
        /// <param name="skip">Number of documents to be skipped.</param>
        /// <returns></returns>
        protected abstract IEnumerable<T> GetRecordsNvi(string identifierContent, int limit, int skip, eSearchMode searchMode);

        /// <summary>
        /// Counts records that contain given string in the id. (Concrete implementation of given repository type)
        /// </summary>
        /// <param name="identifierContent">String required to be contained in the identifier of the records/documents.</param>
        /// <returns></returns>
        protected abstract long FilteredCountNvi(string identifierContent, eSearchMode searchMode);

        /// <summary>
        /// Gets the number of records/documents in the repository.
        /// </summary>
        public long Count { get { return this.CountNvi; } }

        /// <summary>
        /// Gets the count of the records/documents that contain given string in the identifier.
        /// </summary>
        /// <param name="identifierContent">String required to be contained in the identifier of the records/documents.</param>
        /// <returns></returns>
        public long FilteredCount(string identifierContent, eSearchMode searchMode) => FilteredCountNvi(identifierContent, searchMode);


        private volatile object mutex = new object();

        public bool Exists(string identifier)
        {
            lock (mutex)
            {
                return ExistsNvi(identifier);
            }
        }

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

                    data._EntityId = identifier.Trim();
                    OnCreate?.Invoke(identifier, data);
                }
                try
                {
                    CreateNvi(identifier, data);
                }
                catch (Exception e)
                {
                    OnCreateFailed?.Invoke(identifier, data, e);
                    throw e;
                }
                OnCreateDone?.Invoke(identifier, data);
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
                try
                {
                    OnRead?.Invoke(identifier);
                    var readResult = ReadNvi(identifier);
                    OnReadDone?.Invoke(identifier, readResult);
                    return readResult;
                }
                catch (Exception e)
                {
                    OnReadFailed?.Invoke(identifier, e);
                    throw e;
                }
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
                    OnUpdate?.Invoke(identifier, data);
                    if (data._EntityId != identifier)
                    {
                        var idMismatchEx = new IdentifierValueMismatchedException($"Record identifier '[_data._Id]' '{data._EntityId}' has different value than " +
                            $"requested identifier for update '{identifier}'\n" +
                            $"Value passed as 'identifier' must be the same as the value contained in the '_id' member of the data object.");
                        OnUpdateFailed?.Invoke(identifier, data, idMismatchEx);
                        throw idMismatchEx;
                    }
                }
                try
                {
                    UpdateNvi(identifier, data);
                }
                catch (Exception e)
                {
                    OnUpdateFailed?.Invoke(identifier, data, e);
                    throw e;
                }
                OnUpdateDone?.Invoke(identifier, data);
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
                try
                {
                    OnDelete?.Invoke(identifier);
                    DeleteNvi(identifier);
                    OnDeleteDone?.Invoke(identifier);
                }
                catch (Exception e)
                {
                    OnDeleteFailed?.Invoke(identifier, e);
                    throw e;
                }
            }
        }

        void IRepository.Create(string identifier, object data) => Create(identifier, (T)data);

        dynamic IRepository.Read(string identifier) => Read(identifier);

        void IRepository.Update(string identifier, object data) => Update(identifier, (T)data);

        void IRepository.Delete(string identifier) => Delete(identifier);

        long IRepository.Count => this.Count;

        /// <summary>
        /// Gets <see cref="IEnumerable{T}"/> of repository entries that match the identifier.
        /// </summary>
        public IEnumerable<T> GetRecords(string identifier, int limit = 10, int skip = 0, eSearchMode searchMode = eSearchMode.Exact)
        {
            try
            {
                return GetRecordsNvi(identifier, limit, skip, searchMode);
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

    public class DelegateAlreadySetException : Exception
    {
        /// <summary>
        /// When you try to set a delegate that has already been set elsewhere this exception will occur.
        /// Investigate what was the purpose of the delegate you want to override, and make sure that you don't need
        /// the functionality anymore. 
        /// If you need the functionality, be sure to include it in the new delegate.
        /// </summary>
        public DelegateAlreadySetException() : base(TrimMultiline(
            @"The delegate you're trying can only be set once.
            If you want to re-init the delegate set it to first to null then asign a new delegate.
            *TcoApplications rely on this delegates to update the _Created and _Modified values.*"))
        {
        }

        private static string TrimMultiline(string multiline) => string.Join(
                             "\n",
                             multiline.Split('\n').Select(s => s.Trim()));
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
