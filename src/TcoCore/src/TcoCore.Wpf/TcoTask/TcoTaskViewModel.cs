using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortex.Presentation.Wpf;

namespace TcoCore
{
    public class TcoTaskViewModel : RenderableViewModel
    {
        public TcoTaskViewModel()
        {
            ResetCommand = new RelayCommand(a => this.TcoTask._taskState.Cyclic = (int)eTaskState.Idle);
            InvokeCommand = new RelayCommand(a => this.TcoTask._taskState.Cyclic = (int)eTaskState.Request);
        }

        public TcoTask TcoTask { get; private set; }

        public override object Model { get => TcoTask; set => TcoTask = value as TcoTask; }

        public RelayCommand ResetCommand { get; }
        public RelayCommand InvokeCommand { get; }
    }
}
