using System;
using System.Windows.Input;
using TcoCore.Logging;
using TcoCore.Threading;
using TcOpen;
using TcOpen.Inxton;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;

namespace TcoCore
{
    public partial class TcoToggleTask : ICommand
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

        public bool CanExecute(object parameter)
        {
            return this._enabled.Cyclic;
        }

        public void Execute(object parameter)
        {
            var originalState = this._state.Synchron;
            var changeStateDescription = originalState ? $"{this.AttributeStateOnDesc} -> {this.AttributeStateOffDesc}" 
                                                       : $"{this.AttributeStateOffDesc} -> {this.AttributeStateOnDesc}";

            this._toggleRequest.Synchron = true;            
            TcoAppDomain.Current.Logger.Information($"Task '{LogInfo.NameOrSymbol(this)}' toggled '{changeStateDescription}'. {{@sender}}", LogInfo.Create(this));
        }
    }
}
