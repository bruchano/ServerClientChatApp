using Pie.Commands;
using Pie.Domain.Models;
using Pie.EntityFramework.Services.ServerServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pie.ViewModels
{
    public class FriendViewModel : BaseViewModel
    {
        private readonly ServerService _serverService;
        private readonly User _user;
        private readonly Friendship _friendship;
        private FriendsPanelViewModel _friendsPanelViewModel;
        public ICommand DeleteFriendshipCommand { get; private set; }
        public string FriendUsername
        {
            get
            {
                if (_user.Username == _friendship.Inviter.Username)
                {
                    return _friendship.Accepter.Username;
                }
                else
                {
                    return _friendship.Inviter.Username;
                }
            }
        }

        public string FriendEmail
        {
            get
            {
                if (_user.Username == _friendship.Inviter.Username)
                {
                    return _friendship.Accepter.Email;
                }
                else
                {
                    return _friendship.Inviter.Email;
                }
            }
        }

        public DateTime TimeCreated
        {
            get
            {
                return _friendship.TimeCreated;
            }
        }

        public FriendViewModel(FriendsPanelViewModel friendsPanelViewModel, ServerService serverService, User user, Friendship friendship)
        {
            _friendsPanelViewModel = friendsPanelViewModel;
            _serverService = serverService;
            _user = user;
            _friendship = friendship;
            DeleteFriendshipCommand = new DeleteFriendshipCommand(this);
        }

        public async Task DeleteFriendship()
        {
            if (_user.Username == _friendship.Inviter.Username)
            {
                await _serverService.DeleteFriendship(_user.Username, FriendUsername);
                _friendsPanelViewModel.Friends.Remove(this);
            }
            else
            {
                await _serverService.DeleteFriendship(FriendUsername, _user.Username);
                _friendsPanelViewModel.Friends.Remove(this);
            }
        }

    }
}
