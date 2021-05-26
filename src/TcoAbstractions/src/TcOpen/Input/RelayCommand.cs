using System;
using System.Windows.Input;

namespace TcoCore.Input
{
    /// <summary>
    /// A command whose sole purpose is to relay its functionality to other
    /// objects by invoking delegates. 
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;

        private readonly Func<bool> _canExecute;

        private readonly Action _action;

        public RelayCommand(Action<object> execute, Func<bool> canExecute = null, Action logAction = null)
        {
            _execute = execute;
            _canExecute = canExecute;
            _action = logAction;
        }

        public bool CanExecute(object parameter)
        {
            return  _canExecute == null ? false : _canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            _action?.Invoke();
            _execute.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
