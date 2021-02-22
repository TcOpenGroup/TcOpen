using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Vortex.Connector;

namespace inxton.vortex.framework.dynamictreeview.wpf.sandbox
{
    public class TreeWrapperObject : IVortexObject
    {
        public ObservableCollection<TreeGroup> CustomView
        {
            get; set;
        } = new ObservableCollection<TreeGroup>();

        public TreeWrapperObject(IVortexObject obj)
        {
            _obj = obj;
        }

        private readonly IVortexObject _obj;

        public IVortexObject Obj
        {
            get { return _obj; }
        }

        public class NullVortexObject : IVortexElement
        {
            public string Symbol => string.Empty;

            public string AttributeName => string.Empty;

            public string HumanReadable => string.Empty;

            public IVortexObject GetParent()
            {
                return null;
            }

            public string GetSymbolTail()
            {
                return string.Empty;
            }
        };

        public IEnumerable<object> Children
        {
            get
            {
                return _obj?.GetType().GetProperties()
                                     .Select(p =>
                                     {
                                         switch (p.GetValue(_obj))
                                         {
                                             case IVortexObject o:
                                                 return new TreeWrapperObject(o);
                                             case IValueTag o:
                                                 return o;
                                             case Array o when o.OfType<IVortexElement>().Count() > 0:
                                                 return o;
                                             default:
                                                 return Dummy;
                                         }
                                     }
                                     ).Where(x => x != Dummy);
            }
        }

        private object Dummy { get;} = new object();

        public string Symbol => _obj.Symbol;

        public string AttributeName => _obj.AttributeName;

        public string HumanReadable => _obj.HumanReadable;

        public IList<IVortexElement> Kids => new List<IVortexElement>();

        public IEnumerable<IVortexObject> GetChildren()
        {
            return _obj.GetChildren();
        }

        public IEnumerable<IValueTag> GetValueTags()
        {
            return _obj.GetValueTags();
        }

        public void AddChild(IVortexObject vortexObject)
        {
            _obj.AddChild(vortexObject);
        }

        public void AddValueTag(IValueTag valueTag)
        {
            _obj.AddValueTag(valueTag);
        }

        public IConnector GetConnector()
        {
            return _obj.GetConnector();
        }

        public IVortexObject GetParent()
        {
            return _obj.GetParent();
        }

        public string GetSymbolTail()
        {
            return _obj.GetSymbolTail();
        }

        public void AddKid(IVortexElement kid)
        {
            this.Kids.Add(kid);
        }
    }
}
