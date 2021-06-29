using Pie.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Pie.Commands
{
    public class SelectChatCommand : ICommand
    {
        private readonly ChatViewModel _viewModel;

        public SelectChatCommand(ChatViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.SelectChat();
        }
    }
}
