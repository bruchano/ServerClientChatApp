using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pie.Commands
{
    class TestCommand : ICommand
    {
        private Action<string, string> _execute;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            
            var p = (object[])parameter;
            _execute.Invoke(p[0].ToString(), p[1].ToString());
        }

        public TestCommand(Action<string, string> exe)
        {
            _execute = exe;
        }
    }
}
