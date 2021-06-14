#undef WITH_CONNECTOR

namespace TcOpen.Inxton.Security.Wpf.Internal
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Relay command with logging capabilities.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RelayCommand<T> : ICommand
    {
        Action<T> _TargetExecuteMethod;
        Func<T, bool> _TargetCanExecuteMethod;
#if WITH_CONNECTOR
        /// <summary>
        /// Creates new instance of <see cref="RelayCommand"/>.
        /// </summary>
        /// <param name="executeMethod">Method to be executed with this command.</param>
        /// <param name="canExecuteMethod">Can execute method determined whether this command can be executed.</param>
        /// <param name="callerObject">Object that invoked this command.</param>
        /// <param name="commandName">Optional command description of the command to enrich the logged data.</param>
        public RelayCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod = null, Connector.IVortexObject callerObject = null, string commandName = "")
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
            _vortexCallerObject = callerObject;
            _commandName = commandName;
        }

#endif
        /// <summary>
        /// Creates new instance of <see cref="RelayCommand"/>.
        /// </summary>
        /// <param name="executeMethod">Method to be executed with this command.</param>
        /// <param name="canExecuteMethod">Can execute method determined whether this command can be executed.</param>
        /// <param name="callerObject">Object that invoked this command.</param>
        /// <param name="commandName">Optional command description of the command to enrich the logged data.</param>
        public RelayCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod = null, object callerObject = null, string commandName = "")
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
            _callerObject = callerObject;
            _commandName = commandName;
        }

        private object _callerObject;
#if WITH_CONNECTOR
        private IVortexObject _vortexCallerObject;
#endif
        private string _commandName;


        /// <summary>
        /// Forces raising of <see cref="CanExecuteChanged"/> can execute event.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }


        #region ICommand Members

        bool ICommand.CanExecute(object parameter)
        {
            if (_TargetCanExecuteMethod != null)
            {
                T tparm = (T)parameter;
                return _TargetCanExecuteMethod(tparm);
            }
            if (_TargetExecuteMethod != null)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Can execute handler.
        /// </summary>
        public event EventHandler CanExecuteChanged = delegate { };


        void ICommand.Execute(object parameter)
        {
            if (_TargetExecuteMethod != null)
            {
                _TargetExecuteMethod((T)parameter);
#if WITH_CONNECTOR
                if (_vortexCallerObject != null)
                    Journaling.Journal.LogCommandInvocation($"{_vortexCallerObject?.Symbol}.{_commandName}");

                if (_callerObject != null)
                    Journaling.Journal.LogCommandInvocation($"{_callerObject.GetType().Name}.{_commandName}");
#endif
            }
        }
        #endregion
    }

    /// <summary>
    /// Relay command with logging capabilities.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        /// <summary>
        /// Adds or removes <see cref="CanExecuteChanged"/> handler.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

#if WITH_CONNECTOR
        /// <summary>
        /// Creates new instance of <see cref="RelayCommand"/>.
        /// </summary>
        /// <param name="executeMethod">Method to be executed with this command.</param>
        /// <param name="canExecuteMethod">Can execute method determined whether this command can be executed.</param>
        /// <param name="callerObject">Object that invoked this command.</param>
        /// <param name="commandName">Optional command description of the command to enrich the logged data.</param>
        public RelayCommand(Action<object> executeMethod, Func<object, bool> canExecuteMethod = null, Connector.IVortexObject callerObject = null, string commandName = "")
        {
            this.execute = executeMethod;
            this.canExecute = canExecuteMethod;
            _vortexCallerObject = callerObject;
            _commandName = commandName;
        }
#endif

        /// <summary>
        /// Creates new instance of <see cref="RelayCommand"/>.
        /// </summary>
        /// <param name="executeMethod">Method to be executed with this command.</param>
        /// <param name="canExecuteMethod">Can execute method determined whether this command can be executed.</param>
        /// <param name="callerObject">Object that invoked this command.</param>
        /// <param name="commandName">Optional command description of the command to enrich the logged data.</param>
        public RelayCommand(Action<object> executeMethod, Func<object, bool> canExecuteMethod = null, object callerObject = null, string commandName = "")
        {
            this.execute = executeMethod;
            this.canExecute = canExecuteMethod;
            _callerObject = callerObject;
            _commandName = commandName;
        }

        /// <summary>
        /// Creates new instance of <see cref="RelayCommand"/>.
        /// </summary>
        /// <param name="executeMethod">Method to be executed with this command.</param>
        /// <param name="canExecuteMethod">Can execute method determined whether this command can be executed.</param>
        public RelayCommand(Action<object> executeMethod, Func<object, bool> canExecuteMethod = null)
        {
            this.execute = executeMethod;
            this.canExecute = canExecuteMethod;
        }

        /// <summary>
        /// Creates new instance of <see cref="RelayCommand"/>.
        /// </summary>
        /// <param name="executeMethod">Method to be executed with this command.</param>        
        public RelayCommand(Action<object> executeMethod)
        {
            this.execute = executeMethod;
        }

        private object _callerObject;
#if WITH_CONNECTOR
        private IVortexObject _vortexCallerObject;
#endif
        private string _commandName;

        bool ICommand.CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        void ICommand.Execute(object parameter)
        {
            this.execute(parameter);
#if WITH_CONNECTOR
            if (_vortexCallerObject != null)
                Journaling.Journal.LogCommandInvocation($"{_vortexCallerObject?.Symbol}.{_commandName}");

            if (_callerObject != null)
                Journaling.Journal.LogCommandInvocation($"{_callerObject.GetType().Name}.{_commandName}");
#endif

        }
    }
}
