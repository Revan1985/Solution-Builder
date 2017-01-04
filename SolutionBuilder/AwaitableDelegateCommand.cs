using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SolutionBuilder
{

    public class AwaitableDelegateCommand : AwaitableDelegateCommand<Object>, IAsyncCommand
    {
        public AwaitableDelegateCommand(Func<Task> executeMethod) : base(o => executeMethod()) { }
        public AwaitableDelegateCommand(Func<Task> executeMethod, Func<Boolean> canExecuteMethod) : base(o => executeMethod(), o => canExecuteMethod()) { }
    }
    public class AwaitableDelegateCommand<T> : IAsyncCommand<T>, ICommand
    {
        private readonly Func<T, Task> executeMethod;
        private readonly DelegateCommand<T> underlyingCommand;
        private Boolean isExecuting = false;

        public AwaitableDelegateCommand(Func<T, Task> executeMethod) : this(executeMethod, _ => true) { }
        public AwaitableDelegateCommand(Func<T, Task> executeMethod, Func<T, Boolean> canExecuteMethod)
        {
            this.executeMethod = executeMethod;
            this.underlyingCommand = new DelegateCommand<T>(x => { }, canExecuteMethod);
        }

        public async Task ExecuteAsync(T obj)
        {
            try
            {
                isExecuting = true;
                RaiseCanExecuteChanged();
                await executeMethod(obj);
            }
            finally
            {
                isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }

        public ICommand Command { get { return this; } }

        public bool CanExecute(Object parameter)
        {
            return !isExecuting && underlyingCommand.CanExecute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { underlyingCommand.CanExecuteChanged += value; }
            remove { underlyingCommand.CanExecuteChanged -= value; }
        }

        public async void Execute(Object parameter)
        {
            await ExecuteAsync((T)parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            underlyingCommand.RaiseCanExecuteChanged();
        }
    }
}
