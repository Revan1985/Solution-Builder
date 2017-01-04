using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SolutionBuilder
{
    public interface IAsyncCommand : IAsyncCommand<Object> { }
    public interface IAsyncCommand<in T>
    {
        Task ExecuteAsync(T obj);
        Boolean CanExecute(Object obj);
        ICommand Command { get; }
    }
}
