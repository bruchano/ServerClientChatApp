using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pie.Commands
{
    class VoidCommand : ICommand
    {
        private Action _execute;

        public event EventHandler CanExecuteChanged;

        public VoidCommand(Action exe)
        {
            _execute = exe;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke();
        }
    }
}
