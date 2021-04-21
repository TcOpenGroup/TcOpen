using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Vortex.Connector;

namespace Tco.Wpf
{
    public class TreeWrapperObject : IVortexObject
    {

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

        public IEnumerable<object> Children => _obj?.GetKids()
                                     .Select(p =>
                                     {
                                         if (p is IVortexElement vortexElement)
                                         {
                                             if (HasRenderIgnoreAttribute(vortexElement))
                                                 return Dummy;
                                         }
                                         switch (p)
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
                                     })
                                    .Where(x => x != Dummy);

        private bool HasRenderIgnoreAttribute(IVortexElement o)
        {

            var propertyInfo = GetPropertyViaSymbol(o);
            if (propertyInfo != null)
            {
                var propertyAttribute = propertyInfo.GetCustomAttributes()
                    .ToList()
                    .Where(p => p is RenderIgnoreAttribute).FirstOrDefault() as RenderIgnoreAttribute;

                return propertyAttribute != null;

            }

            var typeAttribute = o.GetType()
                .GetCustomAttributes(true)
                .ToList()
                .Any(p => p is RenderIgnoreAttribute);

            return typeAttribute;
        }

        private PropertyInfo GetPropertyViaSymbol(IVortexElement vortexObject)
        {
            var propertyName = vortexObject.GetSymbolTail();

            if (vortexObject.Symbol == null)
                return null;

            if (vortexObject.Symbol.EndsWith("]"))
            {
                propertyName = propertyName?.Substring(0, propertyName.IndexOf('[') - 1);
            }

            var propertyInfo = vortexObject?.GetParent()?.GetType().GetProperty(propertyName);

            return propertyInfo;
        }

        private object Dummy { get; } = new object();

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

        public IEnumerable<IVortexElement> GetKids()
        {
            return this.Kids;
        }
    }
}
