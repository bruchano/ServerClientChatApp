using Pie.EntityFramework.Services.IAuthenticationService;
using Pie.ViewModels;
using System;
using System.Windows.Input;

namespace Pie.Commands
{
    public class LoginCommand : ICommand
    {
        private LoginViewModel _viewModel;

        public LoginCommand(LoginViewModel viewModel)
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
            await _viewModel.Login(_viewModel.TbUsername, (string)parameter);
        }
    }
}
