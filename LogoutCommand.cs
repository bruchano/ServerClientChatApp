using Pie.EntityFramework.Services.IAuthenticationService;
using Pie.ViewModels;
using System;
using System.Windows.Input;

namespace Pie.Commands
{
    public class LogoutCommand : ICommand
    {
        private MenuViewModel _viewModel;

        public LogoutCommand(MenuViewModel viewModel)
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
            await _viewModel.Logout();
        }
    }
}
