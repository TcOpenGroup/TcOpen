using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Vortex.Connector;

namespace TcoCore
{
    public class TreeViewWrapper 
    {

        public IVortexObject Wrapper { get; set; }
        public TreeViewWrapper(IVortexObject obj)
        {
            Wrapper = obj;
            if (Wrapper.GetType() == typeof(IVortexObject))
            {
                HasChildren = true;
                Children = Wrapper.GetKids();
            }
        }
        public IEnumerable<IVortexElement> Children;

        public bool HasChildren { get; set; }
       
    }
}