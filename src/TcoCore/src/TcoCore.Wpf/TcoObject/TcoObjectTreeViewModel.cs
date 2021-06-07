using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Presentation.Wpf;
using Vortex.Connector;
using System.Windows.Data;
using System.Windows.Markup;
using System.Globalization;
using System.Windows;

namespace TcoCore
{
    public class TcoObjectTreeViewModel : RenderableViewModel
    {
        public TcoObjectTreeViewModel()
        {
            
        }
              
        public TcoObject TcoObject { get; private set; }

        public override object Model { get => TcoObject; set { TcoObject  = value as TcoObject; } }       
    }
}
