using Pie.EntityFramework.Services.ServerServices;
using Pie.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Pie.Commands
{
    public class DeleteFriendshipCommand : ICommand
    {
        private readonly FriendViewModel _friendViewModel;

        public DeleteFriendshipCommand(FriendViewModel friendViewModel)
        {
            _friendViewModel = friendViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await _friendViewModel.DeleteFriendship();
        }
    }
}
