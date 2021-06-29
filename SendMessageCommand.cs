using Pie.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Pie.Commands
{
    public class SendMessageCommand : ICommand
    {
        private readonly ChatsPanelViewModel _viewModel;

        public SendMessageCommand(ChatsPanelViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await _viewModel.SendMessage();
        }
    }
}
