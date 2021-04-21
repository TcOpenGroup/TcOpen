using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Vortex.Connector;

namespace Tco.Wpf
{
    public abstract class TreeObject : INotifyPropertyChanged
    {
        private string header;

        public string Header
        {
            get { return header; }
            set
            {
                header = value;
                OnPropertyChanged(nameof(Header));
            }
        }

        public TreeObject(string Header)
        {
            this.Header = Header;
        }
        public TreeObject()
        {

        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public event PropertyChangedEventHandler PropertyChanged;

    }

    public static class TreeItemExtension
    {
        public static IList<TSource> ToObservableCollection<TSource>(this IEnumerable<TSource> source) => new ObservableCollection<TSource>(source);
    }


}