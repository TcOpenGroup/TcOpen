using System;
using System.Windows.Input;
using TcoCore.Logging;
using TcOpen.Inxton;
using TcOpen.Inxton.Input;
using TcOpen.Inxton.Swift;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;

namespace TcoCore
{
    public partial class TcoTask : ICommand, IDecorateLog, IsTask
    {

        private Func<object> _logPayloadDecoration;



        ICodeProvider codeProvider;

        public event EventHandler CanExecuteChanged;

        private void AbortTask(object obj)
        {
            this._abortRequest.Synchron = true;
        }

        private bool CanAbortTask()
        {
            return this._isServiceable.Synchron;
        }
        private bool CanRestoreTask()
        {
            return this._isServiceable.Synchron;
        }

        private void InitCommands()
        {
            this._enabled.Subscribe(ValidateCanExecute);
            this._isServiceable.Subscribe(ValidateCanExecute);
            CanExecuteChanged += TcoTask_CanExecuteChanged;
            Abort = new RelayCommand(AbortTask, x => CanAbortTask(), () => TcoAppDomain.Current.Logger.Information($"Task '{LogInfo.NameOrSymbol(this)}' aborted. {{@sender}}", LogInfo.Create(this)));
            Restore = new RelayCommand(RestoreTask, x => CanRestoreTask(), () => TcoAppDomain.Current.Logger.Information($"Task '{LogInfo.NameOrSymbol(this)}' restored. {{@sender}}", LogInfo.Create(this)));
            this._isServiceable.Subscribe(ValidateCanExecuteAbortRestore);
        }

        partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
        {
            InitCommands();
        }

        partial void PexConstructorParameterless()
        {
            InitCommands();
        }
        private void RestoreTask(object obj)
        {
            this._restoreRequest.Synchron = true;
        }

        private void TcoTask_CanExecuteChanged(object sender, EventArgs e)
        {
            CanExecute(new object());
        }

        void ValidateCanExecute(IValueTag sender, ValueChangedEventArgs args)
        {
            TcoAppDomain.Current.Dispatcher.Invoke(() =>
            {
                CanExecuteChanged(sender, args);
            });
        }

        /// <summary>
        /// Queries whether the command can be executed.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>Boolean result of the query.</returns>
        public bool CanExecute(object parameter)
        {
            return this._enabled.Synchron && this._isServiceable.Synchron;
        }

        /// <summary>
        /// Executes this task.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            if (CanExecute(new object()))
            {
                if (this._taskState.Synchron == (short)(eTaskState.Done))
                {
                    this._restoreRequest.Synchron = true;
                    System.Threading.Thread.Sleep(50);
                }

                this._invokeRequest.Synchron = true;

                TcoAppDomain.Current.Logger.Information($"Task '{LogInfo.NameOrSymbol(this)}' executed. {{@sender}}", LogInfo.Create(this));

                RecordTaskAction?.Invoke(this.CodeProvider);
            }
        }

        public void ValidateCanExecuteAbortRestore(IValueTag sender, ValueChangedEventArgs args)
        {
            Abort.ValidateCanExecute(sender, args);
            Restore.ValidateCanExecute(sender, args);
        }

        /// <summary>
        /// Gets command that aborts the execution of this task.
        /// </summary>
        public RelayCommand Abort { get; private set; }

        /// <summary>
        /// Gets swift code provider for this task.
        /// </summary>
        public virtual ICodeProvider CodeProvider 
        {
            get
            {
                if (codeProvider == null)
                {
                    codeProvider = new Swift.TcoTaskDefaultCodeProvider(this);
                }

                return codeProvider;
            }

            set => codeProvider = value; 
        }
        
        /// <summary>
        /// Gets or sets log payload decoration function. 
        /// The return object will can be added to provide additional information about this task execution.
        /// <note:important>
        /// There must be an implementation that calls and adds the result object into the log message payload.
        /// an example of the implementation can be found here <see cref="LogInfo.Create(IVortexElement)"/> (TcoCore.Logging.LogInfo.Create).
        /// </important>
        /// </summary>
        public Func<object> LogPayloadDecoration { get => _logPayloadDecoration; set => _logPayloadDecoration = value; }

        /// <summary>
        /// Gets command that restores this task.
        /// </summary>
        public RelayCommand Restore { get; private set; }

        /// <summary>
        /// Gets or set action recording delegate for this task.
        /// </summary>
        public RecordTaskActionDelegate RecordTaskAction { get; set; }
    }

    
}
