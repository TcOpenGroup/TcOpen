namespace TcOpen.Inxton.Local.Security
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using TcOpen.Inxton.Data;
    using TcOpen.Inxton.Security;

    /// <summary>
    /// Default class for handling user data. It saves each user in a separate json file.
    /// Default folder is 'C:\INXTON\USERS'.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultUserDataRepository<T> : IRepository<T> where T : UserData
    {
        /// <summary>
        /// Creates new instance of of <see cref="DefaultUserDataRepository{T}"/>
        /// </summary>
        /// <param name="usersFolder">Users folder</param>
        public DefaultUserDataRepository(string usersFolder = @"C:\INXTON\USERS\")
        {
            Location = usersFolder;

            if (!Directory.Exists(Location))
            {
                try
                {
                    Directory.CreateDirectory(Location);
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        /// <summary>
        /// User files location.
        /// </summary>
        public string Location { get; private set; }

        /// <summary>
        /// Creates new user file.
        /// </summary>
        /// <param name="identifier">Users' identifier.</param>
        /// <param name="data">User data.</param>
        public void Create(string identifier, T data)
        {
            try
            {
                if (RecordExists(identifier))
                {
                    throw new DuplicateIdException($"Record with ID {identifier} already exists in this collection.", null);
                }

                Save(identifier, data);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Reads users' data from file.
        /// </summary>
        /// <param name="identifier">Users' identifier</param>
        /// <returns></returns>
        public T Read(string identifier)
        {
            try
            {
                if (!RecordExists(identifier))
                {
                    throw new UnableToLocateRecordId($"Unable to locate record with ID: {identifier} in {Location}.", null);
                }

                return this.Load(identifier, typeof(T));

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Updates users' data.
        /// </summary>
        /// <param name="identifier">Users' identifier.</param>
        /// <param name="data">Users' data.</param>
        public void Update(string identifier, T data)
        {
            try
            {
                if (!RecordExists(identifier))
                {
                    throw new UnableToLocateRecordId($"Unable to locate record with ID: {identifier} in {Location}.", null);
                }

                Save(identifier.ToString(), data);
            }
            catch (Exception ex)
            {

                throw new UnableToUpdateRecord($"Unable to update record ID:{identifier} in {Location}.", ex);
            }

        }

        /// <summary>
        /// Deletes users' data file.
        /// </summary>
        /// <param name="identifier">Users identifier</param>
        public void Delete(string identifier)
        {
            if (this.RecordExists(identifier))
            {
                File.Delete(Path.Combine(this.Location, identifier));
            }
        }

        /// <summary>
        /// Gets total count of users.
        /// </summary>
        [Obsolete("Use 'Queryable' instead")]
        public long Count
        {
            get { return Directory.EnumerateFiles(Location).Count(); }
        }

        /// <summary>
        /// Gets users filtered by partial identifier. If empty will return all records from given range.
        /// </summary>
        /// <param name="identifier">Identifier to search.</param>
        /// <param name="skip">Skip records.</param>
        /// <param name="limit">Limit number of records.</param>
        /// <returns></returns>        
        [Obsolete("Use 'Queryable' instead")]
        public IEnumerable<T> GetRecords(string identifier, int skip = 0, int limit = 1000, eSearchMode searchMode = eSearchMode.Exact)
        {
            var filetered = new List<T>();

            if (string.IsNullOrEmpty(identifier) || string.IsNullOrWhiteSpace(identifier) || identifier == "*")
            {
                foreach (var item in Directory.EnumerateFiles(this.Location))
                {
                    filetered.Add(this.Load(new FileInfo(item).Name, typeof(T)));
                }
            }
            else
            {
                var files = Directory.EnumerateFiles(this.Location).Where(p => p.Contains(identifier));

                foreach (var item in files)
                {
                    filetered.Add(this.Load(new FileInfo(item).Name, typeof(T)));
                }
            }

            return filetered;

        }

        /// <summary>
        /// Counts users according to filter string.
        /// </summary>
        /// <param name="id">Filter string</param>
        /// <returns></returns>
        [Obsolete("Use 'Queryable' instead")]
        public long FilteredCount(string id)
        {
            if (id == "*")
            {
                return Directory.EnumerateFiles(this.Location).Count();
            }
            else
            {
                return Directory.EnumerateFiles(this.Location).Where(p => p.Contains(id)).Count();
            }
        }

        /// <summary>
        /// Retruns true if given user exists.
        /// </summary>
        /// <param name="identifier">Users' identifier</param>
        /// <returns></returns>
        private bool RecordExists(string identifier)
        {
            return File.Exists(Path.Combine(this.Location, identifier));
        }

        private string MakeValidFileName(string fileName)
        {
            var validName = fileName;
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                validName = validName.Replace(c, '_');
            }

            return validName;
        }

        internal void Save(string identifier, T obj)
        {
            obj._EntityId = MakeValidFileName(identifier);
            var path = Path.Combine(this.Location, obj._EntityId);

            using (var jw = new JsonTextWriter(new System.IO.StreamWriter(path)))
            {
                var serializer = Newtonsoft.Json.JsonSerializer.Create(new Newtonsoft.Json.JsonSerializerSettings() { Formatting = Newtonsoft.Json.Formatting.Indented });
                serializer.Serialize(jw, obj, obj.GetType());
            }
        }

        internal T Load(string identifier, Type objtype)
        {
            var path = Path.Combine(this.Location, identifier);

            using (var jw = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(path)))
            {
                var serializer = Newtonsoft.Json.JsonSerializer.Create();
                return (T)serializer.Deserialize(jw, objtype);
            }
        }

        /// <summary>
        /// Returns true when the record with given id exists.
        /// </summary>
        /// <param name="identifier">Idenfifier to seatch for.</param>
        /// <returns></returns>
        public bool Exists(string identifier)
        {
            return RecordExists(identifier);
        }

        /// <summary>
        /// Retrurn <see cref="IQueryable{T}"/> of this repository.
        /// </summary>
        public IQueryable<T> Queryable
        {
            get { return this.GetRecords("*").AsQueryable(); }
        }

        public OnCreateDelegate<T> OnCreate { get; set; } = null;
        public OnUpdateDelegate<T> OnUpdate { get; set; } = null;
        public ValidateDataDelegate<T> OnRecordUpdateValidation { get; set; } = null;
        public OnReadDelegate OnRead { get; set; } = null;
        public OnDeleteDelegate OnDelete { get; set; } = null;
        public OnCreateDoneDelegate<T> OnCreateDone { get; set; } = null;
        public OnReadDoneDelegate<T> OnReadDone { get; set; } = null;
        public OnUpdateDoneDelegate<T> OnUpdateDone { get; set; } = null;
        public OnDeleteDoneDelegate OnDeleteDone { get; set; } = null;
        public OnCreateFailedDelegate<T> OnCreateFailed { get; set; } = null;
        public OnReadFailedDelegate OnReadFailed { get; set; } = null;
        public OnUpdateFailedDelegate<T> OnUpdateFailed { get; set; } = null;
        public OnDeleteFailedDelegate OnDeleteFailed { get; set; } = null;
    }
}

