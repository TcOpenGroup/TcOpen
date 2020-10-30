using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcOpen
{
    public class ComponentsCollector
    {
        private static volatile object mutex = new object();
        private static ComponentsCollector _instance;

        public static ComponentsCollector Get 
        {
            get
            {
                if(_instance == null)
                {
                    lock(mutex)
                    {
                        if(_instance == null)
                        {
                            _instance = new ComponentsCollector();
                        }
                    }
                }

                return _instance;
            }
        } 

        private ComponentsCollector()
        {

        }

        private readonly IList<fbComponent> _compnents = new List<fbComponent>();
        public IEnumerable<fbComponent> Components { get { return _compnents; } }
       
        public void AddComponent(fbComponent component)
        {
            _compnents.Add(component);
        }
    }
}
