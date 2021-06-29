using Pie.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Pie.Commands
{
    public class SendFriendRequestCommand : ICommand
    {
        private readonly FriendsPanelViewModel _friendsPanelViewModel;

        public SendFriendRequestCommand(FriendsPanelViewModel friendsPanelViewModel)
        {
            _friendsPanelViewModel = friendsPanelViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            await _friendsPanelViewModel.SendFriendRequest();
        }
    }
}
