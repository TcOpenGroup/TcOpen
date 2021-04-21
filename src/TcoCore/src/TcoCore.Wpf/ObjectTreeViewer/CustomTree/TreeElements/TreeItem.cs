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

        public TreeItem(string Header, object Item) : base(Header)
        {
            this.Item = Item;
        }


        public object Item { get; set; }

    }



}