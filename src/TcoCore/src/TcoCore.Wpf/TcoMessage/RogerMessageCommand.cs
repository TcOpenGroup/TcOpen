namespace TcoCore
{
    using System;
    using System.Linq;
    using System.Windows.Input;
    using System.Windows.Markup;

    public class RogerMessageCommand
        : MarkupExtension,
            ICommand,
            TcOpen.Inxton.Abstractions.Input.ITcoCommand
    {
        public RogerMessageCommand(TcoMessage message)
        {
            _message = message;
        }

        private TcoMessage _message;

#pragma warning disable CS0067
        public event System.EventHandler CanExecuteChanged;
#pragma warning restore CS0067

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_message != null)
            {
                _message.Pinned.Cyclic = false;
            }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
