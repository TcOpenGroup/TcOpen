using System;
using System.ComponentModel;
using Vortex.Connector;
using Vortex.Framework.Abstractions;

namespace TcoCore
{
    public partial class TcoRemoteTask : INotifyPropertyChanged
    {
        private Action DefferedAction { get; set; }
        
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes this  <see cref="RemoteTask"/>.
        /// </summary>
        /// <param name="deferredAction">Action to be executed on this <see cref="RemoteTask"/> call.</param>
        public void Initialize(Action deferredAction)
        {
            DefferedAction = deferredAction;
            this._isInitialized.Cyclic = true;
            this._startSignature.Subscribe(ExecuteAsync);
            defferedActionCount++;
        }

        /// <summary>
        /// Initializes this  <see cref="RemoteTask"/>.
        /// </summary>
        /// <param name="deferredAction">Action to be executed on this <see cref="RemoteTask"/> call.</param>
        public void Initialize(Func<bool> deferredAction)
        {
            DefferedAction = new Action(() => deferredAction());
            this._isInitialized.Cyclic = true;
            this._startSignature.Subscribe(ExecuteAsync);
            defferedActionCount++;
        }

        private int defferedActionCount;

        /// <summary>
        /// Initializes this  <see cref="RemoteTask"/> exclusively for this <see cref="DefferedAction"/>. Any following attempt
        /// to initialize this <see cref="RemoteTask"/> will throw an exception.
        /// </summary>
        /// <param name="deferredAction">Action to be executed on this <see cref="RemoteTask"/> call.</param>
        public void InitializeExclusively(Action deferredAction)
        {

            if (defferedActionCount > 0)
            {
                throw new MultipleRemoteCallInitializationException("There was an attempt to initialize exclusive RPC call more than once in this application.");
            }

            DefferedAction = deferredAction;
            this._isInitialized.Cyclic = true;
            this._startSignature.Subscribe(ExecuteAsync);
            defferedActionCount++;
        }

        /// <summary>
        /// Initializes this  <see cref="RemoteTask"/> exclusively for this <see cref="DefferedAction"/>. Any following attempt
        /// to initialize this <see cref="RemoteTask"/> will throw an exception.
        /// </summary>
        /// <param name="deferredAction">Action to be executed on this <see cref="RemoteTask"/> call.</param>
        public void InitializeExclusively(Func<bool> deferredAction)
        {

            if (defferedActionCount > 0)
            {
                throw new MultipleRemoteCallInitializationException("There was an attempt to initialize exclusive RPC call more than once in this application.");
            }

            DefferedAction = new Action(() => deferredAction());
            this._isInitialized.Cyclic = true;
            this._startSignature.Subscribe(ExecuteAsync);
            defferedActionCount++;
        }

        /// <summary>
        /// Removes currently bound <see cref="DefferedAction"/> from the execution of this <see cref="RemoteTask"/>
        /// </summary>
        public void DeInitialize()
        {
            this._isInitialized.Cyclic = false;
            this._startSignature.UnSubscribe(ExecuteAsync);
            defferedActionCount--;
        }

        private bool IsRunning = false;

        private async void ExecuteAsync(Vortex.Connector.IValueTag sender, Vortex.Connector.ValueTypes.ValueChangedEventArgs args)
        {
            (this as IVortexObject).Read();

            if (this._startSignature.LastValue != string.Empty &&
                !IsRunning &&
                this._startSignature.LastValue != this._doneSignarure.LastValue)
            {
                try
                {
                    IsRunning = true;
                    RemoteExecutionException = null;
                    await System.Threading.Tasks.Task.Run(() => { DefferedAction.Invoke(); });
                }
                catch (Exception ex)
                {
                    this._hasException.Synchron = true;
                    this._exceptionMessage.Synchron = ex.ToString().Substring(0, 244);
                    RemoteExecutionException = ex;
                    Vortex.Framework.Abstractions.Journal.Journaling.Journal.LogRemoteExecutionEventException(this, ex);
                    return;
                }
                finally
                {
                    IsRunning = false;
                }

                this._doneSignarure.Synchron = this._startSignature.LastValue;
            }
        }

        Exception remoteExecutionException;

        /// <summary>
        /// Gets the exception that occurred during the last execution.
        /// </summary>
        public Exception RemoteExecutionException
        {
            get
            {
                return remoteExecutionException;
            }
            private set
            {
                if (remoteExecutionException == value)
                {
                    return;
                }

                remoteExecutionException = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RemoteExecutionException)));
            }
        }

        /// <summary>
        /// Resets the resets this instance of <see cref="RemoteTask"/>.        
        /// </summary>
        /// <note>If the procedure is being called from the PLC once the <see cref="Reset"/> method is called the execution of this 
        /// <see cref="RemoteTask"/> will start again.</note>
        public void ResetExecution()
        {
            this._startSignature.Synchron = string.Empty;
            this._doneSignarure.Synchron = string.Empty;
            this._hasException.Synchron = false;
            IsRunning = false;
        }
    }
}
