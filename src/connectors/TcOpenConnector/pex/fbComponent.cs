using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Vortex.Connector;

namespace TcOpen
{
    public abstract partial class fbComponent
    {
        private static readonly IList<fbComponent> _compnents = new List<fbComponent>();
        public static IEnumerable<fbComponent> Components { get { return _compnents; } } 

        partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
        {
            _compnents.Add(this);
        }

        public abstract bool Simulate();

        public static void SimulateComponents()
        {
            Task.Run(() => { 
                while(true)
                {
                    System.Threading.Thread.Sleep(100);
                    
                    foreach (var component in Components)
                    {                        
                        component.Simulate();
                    }
                }
            });
        }
    }
}
