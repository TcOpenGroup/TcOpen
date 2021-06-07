using System;
using System.Collections.Generic;
using System.Linq;
using Vortex.Presentation.Wpf;

namespace TcoCore
{
    public class TcoContextTreeViewModel : RenderableViewModel
    {
        public TcoContextTreeViewModel()
        {
            
        }
       
        public TcoContext TcoContext { get; private set; }

        public override object Model { get => TcoContext; set { TcoContext  = value as TcoContext; } }
      
    }
}
