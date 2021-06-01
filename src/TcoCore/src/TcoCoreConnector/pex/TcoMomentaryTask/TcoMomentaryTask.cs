using System;
using System.Windows.Input;
using TcoCore.Logging;
using TcoCore.Threading;
using TcOpen.Inxton;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;

namespace TcoCore
{
    public partial class TcoMomentaryTask : ICommand, IDecorateLog
    {
        public event EventHandler CanExecuteChanged;

        partial void PexConstructor(IVortexObject parent, string readableTail, string symbolTail)
        {
            this._enabled.Subscribe(ValidateCanExecute);
            CanExecuteChanged += TcoToggleTask_CanExecuteChanged;          
        }

        private void TcoToggleTask_CanExecuteChanged(object sender, EventArgs e)
        {
            CanExecute(new object());
        }

        void ValidateCanExecute(IValueTag sender, ValueChangedEventArgs args)
        {
            Dispatcher.Get.Invoke(() => CanExecuteChanged(sender, args));
        }

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
        /// Queries whether the command can be executed.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>Boolean result of the query.</returns>
        public bool CanExecute(object parameter)
        {
            return this._enabled.Cyclic;
        }
        /// <summary>
        /// The calling of the execute mehtod does not have effect on this particular task type.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {

        }

        /// <summary>
        /// Stops momentary task execution.
        /// </summary>        
        public void Stop()
        {            
            _setOnRequest.Synchron = false;
            TcoAppDomain.Current.Logger.Information($"Instant task '{LogInfo.NameOrSymbol(this)}' stopped. {{@sender}}", LogInfo.Create(this));            
        }

        /// <summary>
        /// Starts momentary task execution.
        /// </summary>
        public void Start()
        {

            if(_isServiceable.Synchron)
            { 
                _setOnRequest.Synchron = true;
                TcoAppDomain.Current.Logger.Information($"Instant task '{LogInfo.NameOrSymbol(this)}' started. {{@sender}}", LogInfo.Create(this));
            }
        }
    }
}
