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
    public class TcoObjectViewControlModel : RenderableViewModel
    {
        public TcoObjectViewControlModel()
        {
            
        }
       
        private void Update()
        {
            Tasks = TcoObject.GetChildren<TcoToggleTask>();
            TcoObjectChildren = TcoObject.GetChildren<TcoObject>(Tasks);
            DiagnosticsViewModel = new TcoDiagnosticsViewModel(this.TcoObject);
        }

        public TcoObject TcoObject { get; private set; }

        public override object Model { get => TcoObject; set { TcoObject  = value as TcoObject; Update(); } }

        IEnumerable<TcoToggleTask> tasks;
        public IEnumerable<TcoToggleTask> Tasks
        {
            get => tasks;

            private set
            {
                if (tasks == value)
                {
                    return;
                }

                SetProperty(ref tasks, value);
            }
        }

        public IEnumerable<TcoObject> TcoObjectChildren { get; private set; }

        public TcoDiagnosticsViewModel DiagnosticsViewModel { get; private set; }
    }
}
