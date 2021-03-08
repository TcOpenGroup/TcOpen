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
            RestoreCommand = new RelayCommand(action => this.TcoTask._restoreRequest.Cyclic = true);
            InvokeCommand = new RelayCommand(action => this.TcoTask._invokeRequest.Cyclic = true, canExecuteMethod => IsReady);
        }

        public TcoTask TcoTask { get; private set; }

        public override object Model { get => TcoTask; set => TcoTask = value as TcoTask; }

        public RelayCommand RestoreCommand { get; }
        public RelayCommand InvokeCommand { get; }

        private bool _isReady;

        public bool IsReady
        {
            get
            {
                _isReady = TcoTask._taskState.Synchron == (short)eTaskState.Ready;
                return _isReady;
            }

        }
    }
}
