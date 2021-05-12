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

        public RelayCommand(Action<object> execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute.Invoke();

        public void Execute(object parameter) => _execute.Invoke(parameter);

        public event EventHandler CanExecuteChanged;
    }
}
