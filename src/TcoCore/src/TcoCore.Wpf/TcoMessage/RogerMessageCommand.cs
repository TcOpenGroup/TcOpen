namespace TcoCore
{
    using System;
    using System.Linq;
    using System.Windows.Input;
    using System.Windows.Markup;

    public class RogerMessageCommand : MarkupExtension, ICommand, TcOpen.Inxton.Abstractions.Input.ITcoCommand
    {
        public RogerMessageCommand(TcoMessage message)
        {
            _message = message;
        }

        private TcoMessage _message;

        public event System.EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(_message != null)
            { 
                _message.Persist.Cyclic = false;
            }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
