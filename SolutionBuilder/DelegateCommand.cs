using System;
using System.Diagnostics;
using System.Windows.Input;

namespace SolutionBuilder
{
    public class DelegateCommand : DelegateCommand<Object>
    {
        public DelegateCommand(Action executeMethod) : base(o => executeMethod()) { }
        public DelegateCommand(Action executeMethod, Func<Boolean> canExecuteMethod) : base(o => executeMethod(), o => canExecuteMethod()) { }
    }

    /// <summary>
    /// A command that calls the specified delegate when the command is executed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DelegateCommand<T> : ICommand, IRaiseCanExecuteChanged
    {
        private readonly Func<T, Boolean> _canExecuteMethod;
        private readonly Action<T> _executeMethod;
        private Boolean _isExecuting;

        public DelegateCommand(Action<T> executeMethod)
            : this(executeMethod, null)
        {
        }

        public DelegateCommand(Action<T> executeMethod, Func<T, Boolean> canExecuteMethod)
        {
            if ((executeMethod == null) && (canExecuteMethod == null))
            {
                throw new ArgumentNullException("executeMethod", @"Execute Method cannot be null");
            }
            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        bool ICommand.CanExecute(Object parameter)
        {
            return !_isExecuting && CanExecute((T)parameter);
        }

        void ICommand.Execute(Object parameter)
        {
            _isExecuting = true;
            try
            {
                RaiseCanExecuteChanged();
                Execute((T)parameter);
            }
            finally
            {
                _isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }

        public Boolean CanExecute(T parameter)
        {
            if (_canExecuteMethod == null || parameter == null)
                return true;

            return _canExecuteMethod(parameter);
        }

        public void Execute(T parameter)
        {
            _executeMethod(parameter);
        }
    }
}
