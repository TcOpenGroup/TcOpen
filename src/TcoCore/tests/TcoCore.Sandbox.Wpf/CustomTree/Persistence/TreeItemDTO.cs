using inxton.vortex.framework.dynamictreeview.wpf.sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using Vortex.Connector;

namespace Tco.Wpf.CustomTree.Persistence
{
    public class TreeItemDTO
    {
        public string Kind { get; set; } = "Unknown";
        public string Symbol { get; set; }
        public string Header { get; set; }
        public List<TreeItemDTO> Children { get; set; }
        public List<string> Favourites { get; set; }

        public TreeItemDTO(TreeGroup treeGroup)
        {
            Header = treeGroup.Header;
            Children = AsDTO(treeGroup.Children);
            Favourites = AsDTO(treeGroup.Favourites);
            Kind = nameof(TreeGroup);
        }

        public TreeItemDTO()
        {
            //constructor for deserialization
        }

        public TreeItemDTO(TreeItem item)
        {
            Symbol = item.Item.As<IValueTag>().Symbol;
            Header = item.Header;
            Kind = nameof(TreeItem);
        }

        private List<string> AsDTO(IList<IValueTag> favourites) => favourites.Select(fav => fav.Symbol).ToList();
        private List<TreeItemDTO> AsDTO(IList<TreeObject> children) => children.Select(AsDTO).ToList();

        private TreeItemDTO AsDTO(TreeObject x)
        {
            if (x is TreeGroup group)
                return new TreeItemDTO(group);
            if (x is TreeItem item)
                return new TreeItemDTO(item);
            else
                throw new ArgumentException("Tree object not expected");
        }
    }


}
