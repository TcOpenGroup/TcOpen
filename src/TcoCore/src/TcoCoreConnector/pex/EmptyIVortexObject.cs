using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Connector;

namespace TcoCore
{
    public class EmptyIVortexObject : IVortexObject
    {
        public string Symbol => "";

        public string AttributeName => "";

        public string HumanReadable => "";

        public void AddChild(IVortexObject vortexObject)
        {
            // throw new NotImplementedException();
        }

        public void AddKid(IVortexElement kid)
        {
            //throw new NotImplementedException();
        }

        public void AddValueTag(IValueTag valueTag)
        {
            //throw new NotImplementedException();
        }

        public IEnumerable<IVortexObject> GetChildren()
        {
            return new List<IVortexObject>();
        }

        public IConnector GetConnector()
        {
            return new DummyConnector();
        }

        public IEnumerable<IVortexElement> GetKids()
        {
            return new List<IVortexObject>();
        }

        public IVortexObject GetParent()
        {
            return new RootVortexerObject();
        }

        public string GetSymbolTail()
        {
            return "";
        }

        public IEnumerable<IValueTag> GetValueTags()
        {
            return new List<IValueTag>();
        }
    }
}
