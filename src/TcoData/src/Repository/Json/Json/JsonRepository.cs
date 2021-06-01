using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TcOpen.Inxton.Abstractions.Data;
using TcOpen.Inxton.Data;

namespace TcoData.Repository.Json
{
    public class JsonRepository<T> : RepositoryBase<T> where T : IBrowsableDataObject
    {
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

        protected override IEnumerable<T> GetRecordsNvi(string identifier, int limit, int skip)
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
                var files = Directory.EnumerateFiles(this.Location).Where(p => p.Contains(identifier));

                foreach (var item in files)
                {
                    filetered.Add(this.Load(new FileInfo(item).Name, typeof(T)));
                }
            }

            return filetered;

        }

        protected override long FilteredCountNvi(string id)
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
            obj._Id = MakeValidFileName(identifier);
            var path = Path.Combine(this.Location, obj._Id);

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

        public override IQueryable<T> Queryable
        {
            get { return this.GetRecords("*").AsQueryable(); }
        }
    }
}
