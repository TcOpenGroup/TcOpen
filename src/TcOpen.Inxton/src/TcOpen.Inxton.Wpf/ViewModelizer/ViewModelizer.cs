using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Connector;
using Vortex.Presentation.Wpf;

namespace TcOpen.Inxton.Wpf
{
    public static class ViewModelizer
    {
        public static object ViewModelizeDataContext<VM, M>(this object currentContext)
            where VM : RenderableViewModel, new()
            where M : IVortexObject
        {
            if (!(currentContext is VM))
            {
                if (currentContext is M)
                {
                    return new VM() { Model = currentContext };
                }
            }

            return currentContext;
        }
    }
}
