using Pie.Domain.Models;
using Pie.EntityFramework.Services.IAuthenticationService;
using Pie.ViewModels;
using System;
using System.Windows.Input;

namespace Pie.Commands
{
    public class RegisterCommand : ICommand
    {
        private RegisterViewModel _viewModel;

        public RegisterCommand(RegisterViewModel viewModel)
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
            await _viewModel.Register(string.Empty, string.Empty, string.Empty, string.Empty);
        }
    }
}
