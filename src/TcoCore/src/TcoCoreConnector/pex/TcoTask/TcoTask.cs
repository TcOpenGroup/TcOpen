using System;
using System.Collections.Generic;
using System.Windows.Input;
using TcoCore.Logging;
using TcoCore.Threading;
using TcOpen;
using TcOpen.Inxton;
using TcOpen.Inxton.Input;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;

namespace TcoCore
{
    public partial class TcoTask : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public ICommand Abort { get; private set; }
        public ICommand Restore { get; private set; }

        partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
        {
            this._enabled.Subscribe(ValidateCanExecute);
            CanExecuteChanged += TcoTask_CanExecuteChanged;
            Abort = new RelayCommand(AbortTask, CanAbortTask, () => TcoAppDomain.Current.Logger.Information($"Task '{LogInfo.NameOrSymbol(this)}' aborted. {{@sender}}", LogInfo.Create(this)));
            Restore = new RelayCommand(RestoreTask, CanRestoreTask, () => TcoAppDomain.Current.Logger.Information($"Task '{LogInfo.NameOrSymbol(this)}' restored. {{@sender}}", LogInfo.Create(this)));
        }

        private void TcoTask_CanExecuteChanged(object sender, EventArgs e)
        {
            CanExecute(new object());
        }

        void ValidateCanExecute(IValueTag sender, ValueChangedEventArgs args)
        {
            Dispatcher.Get.Invoke(() => CanExecuteChanged(sender, args));
        }

        public bool CanExecute(object parameter)
        {
            return this._enabled.Cyclic;
        }

        public void Execute(object parameter)
        {
            if (this._isServiceable.Synchron)
            {
                if (this._taskState.Synchron == (short)(eTaskState.Done))
                {
                    this._restoreRequest.Synchron = true;
                    System.Threading.Thread.Sleep(50);
                }
                           
                this._invokeRequest.Synchron = true;

                TcoAppDomain.Current.Logger.Information($"Task '{LogInfo.NameOrSymbol(this)}' executed. {{@sender}}", LogInfo.Create(this));
            }
        }

       

        private bool CanAbortTask()
        {
            return true;
        }

        private void AbortTask(object obj)
        {
            
            this._abortRequest.Cyclic = true;
        }
        private bool CanRestoreTask()
        {
            return true;
        }
        private void RestoreTask(object obj)
        {            
            this._restoreRequest.Cyclic = true;
        }        
    }
}
