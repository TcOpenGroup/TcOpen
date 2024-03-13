using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;

namespace TcoCore
{
    public partial class TcoComponent
    {
        partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
        {
            
        }

        private IEnumerable<IsTask> _tasks;
        private IEnumerable<TcoObject> _children;
        private IEnumerable<TcoObject> _components;

        public IEnumerable<IsTask> Tasks 
        {
            get { if (_tasks == null) _tasks = this.GetChildren<IsTask>(); return _tasks; }
        }


        public IEnumerable<TcoObject> Children
        {
            get { if (_children == null) _children = this.GetDescendants<TcoObject>(); return _children; }
        }
       

        public IEnumerable<TcoObject> Components
        {
            get { if (_components == null) _components = this.GetDescendants<TcoComponent>(SearchComponentsDepth); return _components; }

        }

        public int SearchComponentsDepth
        {
            get; set;
        } = 2;

        public bool HasComponents
        {
            get {  return Components.Count()>0; }
        }



        public object StatusControl
        {
            get
            {
                object status  = this.GetKids().Where(p => p.Symbol.EndsWith("_status")).FirstOrDefault();
                return status != null ? status  : new object();
            }
        }


        public bool IsStatusControlRenderable
        {
            get
            {

                return StatusControl is IVortexObject;
            }
        }



        public object ConfigControl
        {
            get
            {
                object config = this.GetKids().Where(p => p.Symbol.EndsWith("_config")).FirstOrDefault();
                return config != null ? config : new object();
            }
        }
        public bool IsConfigControlRenderable
        {
            get
            {

                return ConfigControl is IVortexObject;
            }
        }

        public object Control
        {
            get
            {
                object control = this.GetKids().Where(p => p.Symbol.EndsWith("_control")).FirstOrDefault();
                return control != null ? control : new object();
            }
        }


        public bool IsControlRenderable
        {
            get
            {

                return Control is IVortexObject;
            }
        }

    }

}
