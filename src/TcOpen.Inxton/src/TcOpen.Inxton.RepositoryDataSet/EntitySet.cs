using System;
using System.ComponentModel;
using System.Collections.Generic;
using TcOpen.Inxton.Data;

namespace TcOpen.Inxton.RepositoryDataSet
{
    public  class EntitySet<T> : IBrowsableDataObject, INotifyPropertyChanged  where T : class, new()
    {
        private IList<T> items ;
        private T item; 

        public event PropertyChangedEventHandler PropertyChanged;

        public EntitySet()
        {
            items = new List<T>();
            item = new T();
        }

        protected void NotifyPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Gets list of items of this set.
        /// </summary>
        public T Item
        {
            get => item; set
            {
                item = value;
                NotifyPropertyChange(nameof(Item));
            }
        }
        /// <summary>
        /// Gets list of items of this set.
        /// </summary>
        public IList<T> Items
        {
            get => items; set
            {
                items = value;
                NotifyPropertyChange(nameof(Items));
            }
        }
        public void AddRecord(T item)
        {
            Items.Add(item);

        }

        public void RemoveRecord(T item)
        {
            Items.Remove(item);

        }


        public dynamic _recordId { get; set; }
        public DateTime _Created { get; set; }
        public string _EntityId { get; set; }
        public DateTime _Modified { get; set; }
      
    }
}
