using System;
using System.Windows.Input;
using TcoCore.Logging;
using TcoCore.Threading;
using TcOpen.Inxton;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;

namespace TcoCore
{
    public partial class TcoMomentaryTask : ICommand
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

        }

        public void Stop()
        {
            TcoAppDomain.Current.Logger.Information($"Instant task '{LogInfo.NameOrSymbol(this)}' stopped. {{@sender}}", LogInfo.Create(this));
            _setOnRequest.Cyclic = false;
        }

        public void Start()
        {
            TcoAppDomain.Current.Logger.Information($"Instant task '{LogInfo.NameOrSymbol(this)}' started. {{@sender}}", LogInfo.Create(this));
            _setOnRequest.Cyclic = true;
        }
    }
}
