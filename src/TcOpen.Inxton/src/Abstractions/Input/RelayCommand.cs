using System;
using System.Windows.Input;
using TcOpen.Inxton.Threading;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;

namespace TcOpen.Inxton.Input
{
    /// <summary>
    /// A command whose sole purpose is to relay its functionality to other
    /// objects by invoking delegates. 
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;

        private readonly Func<object, bool> _canExecute;

        private readonly Action _logAction;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null, Action logAction = null)
        {
            _execute = execute;
            _canExecute = canExecute;
            _logAction = logAction;
        }

        public void ValidateCanExecute(IValueTag sender, ValueChangedEventArgs args)
        {
            Dispatcher.Get.Invoke(() =>
            {
                CanExecuteChanged?.Invoke(sender, args);
            });
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                _logAction?.Invoke();
                _execute.Invoke(parameter);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
