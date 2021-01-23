using Tco.Wpf.CustomTree.Persistence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Vortex.Connector;
using Vortex.Presentation.Wpf;

namespace inxton.vortex.framework.dynamictreeview.wpf.sandbox
{
    public class TreeGroup : TreeObject
    {
        public ICommand AddGroupCommand { get; private set; }
        public ICommand AddItemCommand { get; private set; }
        public ICommand FavouriteCommand { get; private set; }
        public ICommand RemoveFavouriteCommand { get; private set; }
        public bool IsExpanded { get; set; } = true;

        public TreeGroup(string Header) : base(Header) => InitCommands();

        internal void AddFavourite(IValueTag valueTag) => Favourites.Add(valueTag);
        internal void RemoveFavourite(IValueTag valueTag) => Favourites.Remove(valueTag);
        internal void AddItem(IValueTag valueTag) => Children.Add(new TreeItem(valueTag));
        internal void AddGroup(string nameOfGroup) => Children.Add(new TreeGroup(nameOfGroup));
        internal void AddGroup(TreeWrapperObject group) => Children.Add(new TreeGroup(group));
       
        public IEnumerable<TreeObject> AsTreeObject(IVortexObject vortexObject) => vortexObject
                .GetType()
                .GetProperties()
                .Select(prop => AsTreeItem(prop, vortexObject))
                .Where(x => !(x is null));

        private TreeObject AsTreeItem(PropertyInfo p, IVortexObject vortexObject)
        {
            TreeObject toReturn = null;
            switch (p.GetValue(vortexObject))
            {
                case IVortexObject o:
                    toReturn = new TreeGroup(o); break;
                case IValueTag o:
                    toReturn = new TreeItem(o); break;
                case Array o when o.OfType<IVortexElement>().Count() > 0:
                    toReturn = new TreeGroup(o); break;
            }
            return toReturn;
        }

        public IList<TreeObject> Children { get; set; } = new ObservableCollection<TreeObject>();
        public IList<IValueTag> Favourites { get; set; } = new ObservableCollection<IValueTag>();

        private void InitCommands()
        {
            AddGroupCommand = new RelayCommand(AddGroup);
            AddItemCommand = new RelayCommand(o => AddItem(o as IValueTag));
            FavouriteCommand = new RelayCommand(o => AddFavourite(o as IValueTag));
            RemoveFavouriteCommand = new RelayCommand(o => RemoveFavourite(o as IValueTag));
        }
       
        private void AddGroup(object obj)
        {
            if (obj is TreeWrapperObject treeWrap)
                AddGroup(treeWrap);
            if (obj is string str)
                AddGroup(str);
            if (obj is null)
                AddGroup("New group");
        }

        public TreeGroup(TreeWrapperObject vortexObject)
        {
            InitCommands();
            Header = vortexObject.Symbol;
            var _obj = vortexObject.Obj;
            var x = AsTreeObject(_obj);
            Children = new ObservableCollection<TreeObject>(x);
        }

        public TreeGroup(IVortexObject o)
        {
            InitCommands();
            Header = o.Symbol;
            var x = AsTreeObject(o);
            Children = new ObservableCollection<TreeObject>(x);
        }

        public TreeGroup(Array o)
        {
            InitCommands();
        }

        public TreeGroup(TreeItemDTO treeDTO, IConnector connector)
        {
            if (treeDTO.Kind != nameof(TreeGroup))
                throw new ArgumentException("The kind of DTO has to be tree");
            Header = treeDTO.Header;
            var tags = connector.RetrieveValueTags().ToDictionary(keySelector => keySelector.Symbol);
            Favourites = treeDTO.Favourites.Select(fav => tags[fav]).ToObservableCollection();
            Children = treeDTO.Children.Select(x => x.FromDTO(connector)).ToObservableCollection();
            InitCommands();
        }
    }

    class ProductComparer : IEqualityComparer<IValueTag>
    {
        public bool Equals(IValueTag x, IValueTag y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(IValueTag obj)
        {
            throw new NotImplementedException();
        }
    }








}