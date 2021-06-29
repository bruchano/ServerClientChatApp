using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace Pie.Commands
{
    public class PasswordBoxGotFocusCommand : ICommand
    {
        public Action<PasswordBox> _execute { get; private set; }
        public event EventHandler CanExecuteChanged;

        public PasswordBoxGotFocusCommand(Action<PasswordBox> execute)
        {
            _execute = execute;
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke((PasswordBox)parameter);
        }
    }
}
