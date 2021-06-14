﻿using System;
using System.Windows.Input;
using Vortex.Connector;
using Vortex.Connector.ValueTypes;
using TcOpen.Inxton.Threading;

namespace TcOpen.Inxton.Input
{
    /// <summary>
    /// A command whose sole purpose is to relay its functionality to other
    /// objects by invoking delegates. 
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;

        private readonly Func<bool> _canExecute;

        private readonly Action _logAction;

        public RelayCommand(Action<object> execute, Func<bool> canExecute = null, Action logAction = null)
        {
            _execute = execute;
            _canExecute = canExecute;
            _logAction = logAction;
        }

        public void ValidateCanExecute(IValueTag sender, ValueChangedEventArgs args)
        {
            TcoAppDomain.Current.Dispatcher.Invoke(() =>
            {
                CanExecuteChanged?.Invoke(sender, args);
            });
        }

        public bool CanExecute(object parameter)
        {
            return  _canExecute == null ? true : _canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            if(CanExecute(parameter))
            {               
               _logAction?.Invoke();                                     
               _execute.Invoke(parameter);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
