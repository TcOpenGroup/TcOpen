using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using Vortex.Connector;
using Vortex.Presentation.Wpf;

namespace TcoCore
{
    public class TcoObjectTreeViewModel : RenderableViewModel
    {
        public TcoObjectTreeViewModel() { }

        public TcoObject TcoObject { get; private set; }

        public override object Model
        {
            get => TcoObject;
            set { TcoObject = value as TcoObject; }
        }
    }
}
