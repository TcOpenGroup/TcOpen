using System;
using System.Collections.Generic;
using System.Linq;
using Vortex.Connector;

namespace TcoCore.Tests
{
    public class MockRootObject : IVortexObject, ITwinObject, IVortexElement
    {
        public string Symbol => string.Empty;

        public string AttributeName => string.Empty;

        public string HumanReadable => string.Empty;

        public IList<IVortexObject> _children = new List<IVortexObject>();

        public void AddChild(IVortexObject vortexObject)
        {
            _children.Add(vortexObject);
        }

        public IList<IVortexElement> _kids = new List<IVortexElement>();

        public void AddKid(IVortexElement kid)
        {
            _kids.Add(kid);
        }

        public IList<IValueTag> _valueTags = new List<IValueTag>();

        public void AddValueTag(IValueTag valueTag)
        {
            _valueTags.Add(valueTag);
        }

        public IEnumerable<IVortexObject> GetChildren()
        {
            return _children;
        }

        private ConnectorAdapter _adapter = new ConnectorAdapter(typeof(DummyConnectorFactory));

        public IConnector GetConnector()
        {
            return _adapter.GetConnector(null);
        }

        public IEnumerable<IVortexElement> GetKids()
        {
            return _kids;
        }

        private IVortexObject _root = new Vortex.Connector.RootVortexerObject();

        public IVortexObject GetParent()
        {
            return _root;
        }

        public string GetSymbolTail()
        {
            return string.Empty;
        }

        public IEnumerable<IValueTag> GetValueTags()
        {
            return _valueTags;
        }
    }
}