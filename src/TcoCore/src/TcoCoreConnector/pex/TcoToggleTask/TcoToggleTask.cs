using System;
using System.Windows.Input;
using TcoCore.Logging;
using TcOpen;
using TcOpen.Inxton;
using TcOpen.Inxton.Swift;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;

namespace TcoCore
{
    public partial class TcoToggleTask : ICommand, IDecorateLog, IsTask
    {
        public event EventHandler CanExecuteChanged;

        partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
        {
            this._enabled.Subscribe(ValidateCanExecute);
            this._isServiceable.Subscribe(ValidateCanExecute);
            CanExecuteChanged += TcoToggleTask_CanExecuteChanged;          
        }

        private ICodeProvider codeProvider;

        private Func<object> _logPayloadDecoration;

        /// <summary>
        /// Gets or sets log payload decoration function. 
        /// The return object will can be added to provide additional information about this task execution.
        /// <note:important>
        /// There must be an implementation that calls and adds the result object into the log message payload.
        /// an example of the implementation can be found here <see cref="LogInfo.Create(IVortexElement)"/> (TcoCore.Logging.LogInfo.Create).
        /// How to create a payload decorator see in <see cref="TcoSequencer.PexConstructor(IVortexObject, string, string)"/>
        /// </important>
        /// </summary>
        public Func<object> LogPayloadDecoration { get => _logPayloadDecoration; set => _logPayloadDecoration = value; }

        /// <summary>
        /// Gets swift code provider for this task.
        /// </summary>
        public virtual ICodeProvider CodeProvider
        {
            get
            {
                if (codeProvider == null)
                {
                    codeProvider = new Swift.TcoToggleTaskDefaultCodeProvider(this);
                }

                return codeProvider;
            }

            protected set => codeProvider = value;
        }

        private void TcoToggleTask_CanExecuteChanged(object sender, EventArgs e)
        {
            CanExecute(new object());
        }

        void ValidateCanExecute(IValueTag sender, ValueChangedEventArgs args)
        {
            TcoAppDomain.Current.Dispatcher.Invoke(() => CanExecuteChanged(sender, args));
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
            if(CanExecute(parameter))
            { 
                var originalState = this._state.Synchron;
                var changeStateDescription = originalState ? $"{this.AttributeStateOnDesc} -> {this.AttributeStateOffDesc}" 
                                                           : $"{this.AttributeStateOffDesc} -> {this.AttributeStateOnDesc}";

                this._toggleRequest.Synchron = true;            
                TcoAppDomain.Current.Logger.Information($"Task '{LogInfo.NameOrSymbol(this)}' toggled '{changeStateDescription}'. {{@sender}}", LogInfo.Create(this));
            }
        }
    }
}
