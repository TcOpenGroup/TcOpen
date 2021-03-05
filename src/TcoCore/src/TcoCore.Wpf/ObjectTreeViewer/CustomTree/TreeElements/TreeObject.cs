using Tco.Wpf.CustomTree.Persistence;
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
        public static TreeItemDTO AsDTO(this TreeObject treeObject)
        {
            if (treeObject is TreeItem item)
                return new TreeItemDTO(item);
            if (treeObject is TreeGroup group)
                return new TreeItemDTO(group);
            throw new ArgumentException("DTO for this type is not supported");
        }

        public static TreeObject FromDTO(this TreeItemDTO treeDTO, IConnector connector)
        {
            if (treeDTO.Kind == nameof(TreeGroup))
                return new TreeGroup(treeDTO, connector);
            if (treeDTO.Kind == nameof(TreeItem))
                return new TreeItem(treeDTO, connector);
            throw new ArgumentException("Can't convert this item from DTO");
        }
        public static IList<TSource> ToObservableCollection<TSource>(this IEnumerable<TSource> source) => new ObservableCollection<TSource>(source);
    }


}