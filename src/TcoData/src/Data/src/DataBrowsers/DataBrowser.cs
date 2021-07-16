using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using TcOpen.Inxton.Data;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;

namespace TcOpen.Inxton.Data
{
    public class DataBrowser<T> : IDataBrowser where T  : IBrowsableDataObject, new()
    {
        public DataBrowser(IRepository<T> repository)
        {
            this.Repository = repository;
            this.Records = new List<T>();
        }

        public IList<T> Records { get; protected set; }

        protected IRepository<T> Repository { get; set; }        

        public void Filter(string identifier, int limit = 10, int skip = 0)
        {
            Records.Clear();

            foreach (var item in this.Repository.GetRecords(identifier, limit: limit, skip: skip))
            {
                this.Records.Add(item);
            }
        }
        public T FindById(string id)
        {
            Records.Clear();

            foreach (var record in this.Repository.GetRecords())
            {
                Records.Add(record);
            }
            
            if(Records.Count > 0)
            {
                var found = Records.FirstOrDefault(p => p._EntityId == id);
                if (found == null)
                    throw new UnableToLocateRecordId($"Unable to locate record id '{id}'", null);
                else
                    return found;
            }
            
            if(id != "*")
                throw new UnableToLocateRecordId($"Unable to locate record id '{id}'", null);

            return new T();
        }
        public IEnumerable<T> FindByCreatedRange(DateTime start, DateTime end) { return null; }
        public IEnumerable<T> FindByModifiedRange(DateTime start, DateTime end) { return null; }
        public void AddRecord(T data) { Repository.Create((data)._EntityId, data); }
        public void UpdateRecord(T data)
        {            
            Repository.Update(((IBrowsableDataObject)data)._EntityId, data);
        }
        public void Delete(T data) { Repository.Delete(((IBrowsableDataObject)data)._EntityId); }
        public long Count { get { return this.Repository.Count; } }        
        public long FilteredCount(string id)
        {
            return this.Repository.FilteredCount(id);
        }
        object IDataBrowser.FindById(string id)
        {
            return this.FindById(id);
        }
        IEnumerable<object> IDataBrowser.FindByCreatedRange(DateTime start, DateTime end)
        {
            return FindByCreatedRange(start, end) as IEnumerable<object>;
        }
        IEnumerable<object> IDataBrowser.FindByModifiedRange(DateTime start, DateTime end)
        {
            return FindByModifiedRange(start, end) as IEnumerable<object>;
        }
        void IDataBrowser.AddRecord(object data)
        {
            this.AddRecord((T)data);
        }
        void IDataBrowser.UpdateRecord(object data)
        {
            this.UpdateRecord((T)data);
        }
        void IDataBrowser.Delete(object data)
        {
            this.Delete((T)data);
        }
        IList<object> IDataBrowser.Records { get { return this.Records.ToList().ConvertAll(p => (object)p); } }
        long IDataBrowser.Count { get { return this.Count; } }        
        object IDataBrowser.CreateEmpty()
        {
            return new T();
        }
        long IDataBrowser.FilteredCount(string id) { return this.FilteredCount(id); }

        public IQueryable<T> GetRecords(Expression<Func<T, bool>> expression)
        {
            return this.Repository.Queryable.Where(expression);
        }

        public IEnumerable<string> Export(Expression<Func<T, bool>> expression, char separator = ';')
        {
            var onliner = typeof(T).Name.Replace("Plain", string.Empty);

            var adapter = new Vortex.Connector.ConnectorAdapter(typeof(DummyConnectorFactory));
            var dummyConnector = adapter.GetConnector(new object[] { });

            var onlinerType = Assembly.GetAssembly(typeof(T)).GetTypes().FirstOrDefault(p => p.Name == onliner);
            var prototype = Activator.CreateInstance(onlinerType, new object[] { dummyConnector, string.Empty, string.Empty}) as IVortexObject;
            var exportables = this.Repository.Queryable.Where(expression);
            var itemExport = new StringBuilder();
            var export = new List<string>();

            // Create header
            var valueTags = prototype.RetrieveValueTags();
            foreach (var valueTag in valueTags)
            {
                itemExport.Append($"{valueTag.Symbol}{separator}");
            }

            export.Add(itemExport.ToString());
            itemExport.AppendLine();

            itemExport.Clear();
            foreach (var valueTag in valueTags)
            {
                itemExport.Append($"{valueTag.HumanReadable}{separator}");
            }

            export.Add(itemExport.ToString());
            itemExport.AppendLine();


            foreach (var document in exportables)
            {
                itemExport.Clear();
                ((dynamic) prototype).CopyPlainToShadow(document);
                var values = prototype.RetrieveValueTags();
                foreach (var @value in values)
                {
                    var val = (string)(((dynamic) @value).Shadow.ToString());
                    if(val.Contains(separator))
                    { 
                        val = val.Replace(separator, '►');
                    }

                    itemExport.Append($"{val}{separator}");
                }

                export.Add(itemExport.ToString());

            }

            return export;

        }

        private class ImportItems
        {
            internal string Key { get; set; }
            internal dynamic Value { get; set; }
        }

        public void Import(IEnumerable<string> records, IVortexObject crudDataObject = null, char separator = ';')
        {
            var documents = records.ToArray();
            var header = documents[0];

            var headerItems = header.Split(separator);
            var dictionary = new List<ImportItems>();

            // Prepare swappable object
            var onliner = typeof(T).Name.Replace("Plain", string.Empty);

            var adapter = new Vortex.Connector.ConnectorAdapter(typeof(DummyConnectorFactory));
            var dummyConnector = adapter.GetConnector(new object[] { });

            var onlinerType = Assembly.GetAssembly(typeof(T)).GetTypes().FirstOrDefault(p => p.Name == onliner);

            IVortexObject prototype;

            if(crudDataObject == null)
                prototype = Activator.CreateInstance(onlinerType, new object[] { dummyConnector, string.Empty, string.Empty }) as IVortexObject;
            else
                prototype = crudDataObject; 

            var valueTags = prototype.RetrieveValueTags();

            // Get headered dictionary
            foreach (var headerItem in headerItems)
            {                
                dictionary.Add(new ImportItems() {Key = headerItem});
            }


            // Load values
            for (int i = 2; i < documents.Count(); i++)
            {
                var documentItems = documents[i].Split(separator);
                for (int a = 0; a < documentItems.Count(); a++)
                {
                    dictionary[a].Value = documentItems[a];
                }

                UpdateDocument(dictionary, valueTags, prototype);
            }

            
           
        }

        private void UpdateDocument(List<ImportItems> dictionary, IEnumerable<IValueTag> valueTags, IVortexObject prototype)
        {                        
            string id = dictionary.FirstOrDefault(p => p.Key == "_EntityId").Value;
            var existing = this.Repository.Queryable.Where(p => p._EntityId == id).FirstOrDefault();
            if(existing != null)
            { 
                ((dynamic)prototype).CopyPlainToShadow(existing);
            }

            ((dynamic)prototype)._EntityId.Shadow = id;

            if(existing != null) ((dynamic)prototype).ChangeTracker.StartObservingChanges();
            // Swap values to shadow
            foreach (var item in dictionary)
            {
                if (!string.IsNullOrEmpty(item.Key))
                {
                    var tag = valueTags.FirstOrDefault(p => p.Symbol == item.Key);
                    OnlinerBaseType type = tag as OnlinerBaseType;
                    
                    if (type is OnlinerString || type is OnlinerWString)
                    {
                        ((dynamic) tag).Shadow = (CastValue(type, item.Value) as string)?.Replace('►', ';');
                    }
                    else
                    {
                        ((dynamic)tag).Shadow = CastValue(type, item.Value);
                    }
                }
            }

            if (existing != null) ((dynamic)prototype).ChangeTracker.StopObservingChanges();
                                                                       
                        
            if (existing != null)
            {
                ((dynamic)prototype).ChangeTracker.Import(existing);
                ((dynamic)existing).CopyShadowToPlain((dynamic)prototype);
                this.Repository.Update(existing._EntityId, existing);
            }
            else
            {
                T newRecord = new T();
                ((dynamic)prototype).ChangeTracker.Import(newRecord);
                ((dynamic)newRecord).CopyShadowToPlain((dynamic)prototype);
                this.Repository.Create(newRecord._EntityId, newRecord);
            }            
        }

        private dynamic CastValue(OnlinerBaseType type, string @value)
        {
            switch (type)
            {
                case OnlinerBool c:
                    return bool.Parse(@value);                    
                case OnlinerByte c:
                    return  byte.Parse(@value);
                    
                case OnlinerDate c:
                    return  DateTime.Parse(@value);
                    
                case OnlinerDInt c:
                    return  int.Parse(@value);
                    
                case OnlinerDWord c:
                    return  uint.Parse(@value);
                    
                case OnlinerInt c:
                    return  short.Parse(@value);
                    
                case OnlinerLInt c:
                    return  long.Parse(@value);
                    
                case OnlinerLReal c:
                    return  double.Parse(@value);
                    
                case OnlinerLTime c:
                    return  TimeSpan.Parse(@value);
                    
                case OnlinerLWord c:
                    return  ulong.Parse(@value);
                    
                case OnlinerReal c:
                    return  float.Parse(@value);
                    
                case OnlinerSInt c:
                    return  sbyte.Parse(@value);
                    
                case OnlinerString c:
                    return  @value;
                    
                case OnlinerTime c:
                    return  TimeSpan.Parse(@value);
                    
                case OnlinerTimeOfDay c:
                    return  TimeSpan.Parse(@value);

                case OnlinerDateTime c:
                    return DateTime.Parse(@value);

                case OnlinerUDInt c:
                    return  uint.Parse(@value);
                    
                case OnlinerUInt c:
                    return  ushort.Parse(@value);
                    
                case OnlinerULInt c:
                    return  ulong.Parse(@value);
                    
                case OnlinerUSInt c:
                    return  byte.Parse(@value);
                    
                case OnlinerWord c:
                    return  ushort.Parse(@value);
                    
                case OnlinerWString c:
                    return  @value;               
                default:
                    throw new Exception($"Unknown type {type.GetType()}");
                    
            }
        }
    }

    public static class DataBrowser
    {       
        public static DataBrowser<T> Factory<T>(IRepository<T> repository) where T : IBrowsableDataObject, new()
        {
            return new DataBrowser<T>(repository);
        }


        public static DataBrowser<T> Create<T>(IRepository<T> repository) where T : IBrowsableDataObject, new()
        {
            return Factory<T>(repository);            
        }
    }

    public interface IDataBrowser
    {
        IList<object> Records { get; }
        void Filter(string identifier, int limit, int skip);
        object FindById(string id);
        IEnumerable<object> FindByCreatedRange(DateTime start, DateTime end);
        IEnumerable<object> FindByModifiedRange(DateTime start, DateTime end);
        void AddRecord(object data);
        void UpdateRecord(object data);
        void Delete(object data);
        object CreateEmpty();              
        long Count { get; }
        long FilteredCount(string id);
    }
}
