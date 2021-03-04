using Tco.Wpf.CustomTree.Persistence;
using System;
using System.Linq;
using Vortex.Connector;

namespace Tco.Wpf
{

    public class TreeItem : TreeObject
    {

        public TreeItem(string Header) : base(Header)
        {
        }

        public TreeItem(IValueTag tag)
        {
            Header = tag.Symbol;
            Item = tag;
        }

        public TreeItem(TreeItemDTO treeDTO, IConnector connector)
        {
            if (treeDTO.Kind != nameof(TreeItem))
                throw new ArgumentException("The kind of DTO has to be tree item");

            Item = connector.RetrieveValueTags().Where(tag => tag.Symbol == treeDTO.Symbol).FirstOrDefault() ;
            Header = treeDTO.Header;
        }

        public TreeItem(string Header, object Item) : base(Header)
        {
            this.Item = Item;
        }


        public object Item { get; set; }

    }



}