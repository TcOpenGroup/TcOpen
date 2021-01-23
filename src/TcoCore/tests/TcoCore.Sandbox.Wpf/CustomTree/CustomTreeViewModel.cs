using inxton.vortex.framework.dynamictreeview.wpf.sandbox;
using Newtonsoft.Json;
using Tco.Wpf.CustomTree.Persistence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Vortex.Connector;
using Vortex.Presentation.Wpf;

namespace Tco.Wpf.CustomTree
{

    public class CustomTreeViewModel
    {
        public ICommand AddNewGroupCommand { get; }
        public ICommand AddNewItemCommand { get; }
        internal ICommand PersistTreeCommand { get; }
        internal ICommand RetrieveTreeCommand { get; }

        public IList<TreeObject> Tree { get; private set; } = new ObservableCollection<TreeObject>();
        public CustomTreeViewModel()
        {
            AddNewGroupCommand = new RelayCommand(AddNewGroup);
            AddNewItemCommand = new RelayCommand(p => AddNewItem(p as IValueTag));
            PersistTreeCommand = new RelayCommand(PersistTree);
            RetrieveTreeCommand = new RelayCommand(controller => RetrieveTree(controller as IConnector));

            var helperTreeGroup = new TreeGroup("helper");
            helperTreeGroup.AddGroup("test group");

            if (Tree.Count() == 0) Tree.Add(helperTreeGroup);
        }

        private string JsonName => "JsonTree.json";
        private void RetrieveTree(IConnector connector)
        {
            if (!File.Exists(JsonName))
                return;
            var json = File.ReadAllText(JsonName);
            var ax = JsonConvert.DeserializeObject<List<TreeItemDTO>>(json).Select(x => x.FromDTO(connector)).ToList();
            Tree.Clear();
            ax.ForEach(Tree.Add);
        }

        private void PersistTree(object obj)
        {
            var tree = JsonConvert.SerializeObject(Tree.Select(x => x.AsDTO()), Formatting.Indented);
            File.WriteAllText(JsonName, tree);
        }

        private void AddNewGroup(object obj)
        {
            var helperTreeGroup = new TreeGroup("helper");
            helperTreeGroup.AddGroupCommand.Execute(obj);

            Tree.Add(helperTreeGroup.Children.First());
        }
        private void AddNewItem(IValueTag valueTag) => Tree.Add(new TreeItem(valueTag));
    }

}

