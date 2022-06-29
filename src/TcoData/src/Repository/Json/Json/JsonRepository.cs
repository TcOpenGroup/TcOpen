using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TcOpen.Inxton.Data;


namespace TcOpen.Inxton.Data.Json
{
    /// <summary>
    /// Provides repository for storing data in files with `Json` format.
    /// <note type="warning">
    /// This repository type is not suitable for large data collections.   
    /// Use this repository for settings, recipes or data persistence with limited number of records.
    /// </note>
    /// </summary>
    /// <typeparam name="T">POCO twin type</typeparam>
    public class JsonRepository<T> : RepositoryBase<T> where T : IBrowsableDataObject
    {
        /// <summary>
        /// Creates new instance of <see cref="JsonRepository{T}"/>
        /// </summary>
        /// <param name="parameters">Repository parameters</param>
        public JsonRepository(JsonRepositorySettings<T> parameters)
        {
            Location = parameters.Location;

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
        /// Get the location (directory) where the entries of this repository are placed.
        /// </summary>
        public string Location { get; private set; }
        protected override void CreateNvi(string identifier, T data)
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
        protected override T ReadNvi(string identifier)
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
        protected override void UpdateNvi(string identifier, T data)
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
        protected override void DeleteNvi(string identifier)
        {
            if (this.RecordExists(identifier))
            {
                File.Delete(Path.Combine(this.Location, identifier));
            }
        }
        protected override long CountNvi
        {
            get { return Directory.EnumerateFiles(Location).Count(); }
        }

        protected override IEnumerable<T> GetRecordsNvi(string identifier, int limit, int skip, eSearchMode searchMode)
        {
            var filetered = new List<T>();

            if (identifier == "*")
            {
                foreach (var item in Directory.EnumerateFiles(this.Location))
                {
                    filetered.Add(this.Load(new FileInfo(item).Name, typeof(T)));
                }
            }
            else
            {
                IEnumerable<string> files;

                switch (searchMode)
                {                   
                    case eSearchMode.StartsWith:
                        files = Directory.EnumerateFiles(this.Location).Where(p => new FileInfo(p).Name.StartsWith(identifier));
                        break;
                    case eSearchMode.Contains:
                        files = Directory.EnumerateFiles(this.Location).Where(p => new FileInfo(p).Name.Contains(identifier));
                        break;
                    case eSearchMode.Exact:
                    default:
                        files = Directory.EnumerateFiles(this.Location).Select(p => new FileInfo(p)).Where(p => p.Name == identifier).Select(p => p.FullName);
                        break;
                }
                
                foreach (var item in files)
                {
                    filetered.Add(this.Load(new FileInfo(item).Name, typeof(T)));
                }
            }

            return filetered;

        }

        protected override long FilteredCountNvi(string id, eSearchMode searchMode)
        {
            if (id == "*")
            {
                return Directory.EnumerateFiles(this.Location).Count();
            }
            else
            {
                switch (searchMode)
                {
                    case eSearchMode.StartsWith:
                        return Directory.EnumerateFiles(this.Location).Where(p => new FileInfo(p).Name.StartsWith(id)).Count();
                    case eSearchMode.Contains:
                        return Directory.EnumerateFiles(this.Location).Where(p => new FileInfo(p).Name.Contains(id)).Count();
                    case eSearchMode.Exact:
                    default:
                        return Directory.EnumerateFiles(this.Location).Select(p => new FileInfo(p)).Where(p => p.Name == id).Select(p => p.FullName).Count();
                }
            }
        }

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

            using (var jw = new Newtonsoft.Json.JsonTextWriter(new System.IO.StreamWriter(path)))
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

        protected override bool ExistsNvi(string identifier)
        {
            return RecordExists(identifier);
        }

        public override IQueryable<T> Queryable
        {
            get { return this.GetRecords("*").AsQueryable(); }
        }
    }
}
