using Pie.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Pie.Commands
{
    public class AcceptFriendRequestCommand : ICommand
    {
        private readonly FriendRequestViewModel _friendRequestViewModel;

        public AcceptFriendRequestCommand(FriendRequestViewModel friendRequestViewModel)
        {
            _friendRequestViewModel = friendRequestViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await _friendRequestViewModel.AcceptFriendRequest();
        }
    }
}
