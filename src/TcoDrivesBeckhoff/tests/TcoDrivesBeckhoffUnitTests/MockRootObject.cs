using System;
using System.Collections.Generic;
using System.Linq;
using Vortex.Connector;

namespace TcoDrivesBeckhoffUnitTests
{
    public class MockRootObject : IVortexObject, ITwinObject, IVortexElement
    {
        public MockRootObject()
        {
            _adapter = new ConnectorAdapter(typeof(DummyConnectorFactory));
            _connector = _adapter.GetConnector(null);
            _connector
                .GetConnector()
                .SuspendWriteProtection(
                    "Hoj morho vetvo mojho rodu, kto kramou rukou siahne na tvoju slobodu a co i dusu das v tom boji divokom vol nebyt ako byt otrokom!"
                );
        }

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

        private ConnectorAdapter _adapter;
        private IConnector _connector;

        public IConnector GetConnector()
        {
            return _connector;
        }

        public IEnumerable<IVortexElement> GetKids()
        {
            return _kids;
        }

        public IVortexObject GetParent()
        {
            return GetConnector();
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
