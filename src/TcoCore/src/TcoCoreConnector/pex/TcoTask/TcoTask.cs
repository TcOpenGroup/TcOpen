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
    public partial class TcoTask : ICommand, IDecorateLog
    {
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Gets command that aborts the execution of this task.
        /// </summary>
        public ICommand Abort { get; private set; }

        /// <summary>
        /// Gets command that restores this task.
        /// </summary>
        public ICommand Restore { get; private set; }

        private Func<object> _logPayloadDecoration;

        /// <summary>
        /// Gets or sets log payload decoration function. 
        /// The return object will can be added to provide additional information about this task execution.
        /// <note:important>
        /// There must be an implementation that calls and adds the result object into the log message payload.
        /// an example of the implementation can be found here <see cref="LogInfo.Create(IVortexElement)"/> (TcoCore.Logging.LogInfo.Create).
        /// </important>
        /// </summary>
        public Func<object> LogPayloadDecoration { get => _logPayloadDecoration; set => _logPayloadDecoration = value; }

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

        /// <summary>
        /// Queries whether the command can be executed.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>Boolean result of the query.</returns>
        public bool CanExecute(object parameter)
        {
            return this._enabled.Cyclic;
        }

        /// <summary>
        /// Executes this task.
        /// </summary>
        /// <param name="parameter"></param>
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
