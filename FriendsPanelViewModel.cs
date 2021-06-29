using Microsoft.AspNetCore.SignalR.Client;
using Pie.Commands;
using Pie.Domain.Models;
using Pie.EntityFramework.Services.IAuthenticationService;
using Pie.EntityFramework.Services.ServerServices;
using Pie.EntityFramework.Services.UserStateHandlers;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pie.ViewModels
{
    public class FriendsPanelViewModel : BaseViewModel
    {
        private readonly ServerService _serverService;
        private readonly User _user;
        private string _tbInputUsername;
        public ICommand SendFriendRequestCommand { get; private set; }
        public string TbInputUsername
        {
            get
            {
                return _tbInputUsername;
            }
            set
            {
                _tbInputUsername = value;
                OnPropertyChanged(nameof(TbInputUsername));
            }
        }
        public ObservableCollection<FriendViewModel> Friends { get; }
        public ObservableCollection<FriendRequestViewModel> FriendRequests { get; }

        public FriendsPanelViewModel(WindowViewModel m, ServerService serverService, User currentUser)
        {
            windowViewModel = m;
            _serverService = serverService;
            _user = currentUser;

            SendFriendRequestCommand = new SendFriendRequestCommand(this);
            
            Friends = new ObservableCollection<FriendViewModel>();
            FriendRequests = new ObservableCollection<FriendRequestViewModel>();

            foreach (Friendship friendship in _user.InvitedFriendships)
            {
                Friends.Add(new FriendViewModel(this, _serverService, currentUser, friendship));
            }
            foreach (Friendship friendship in _user.AcceptedFriendships)
            {
                Friends.Add(new FriendViewModel(this, _serverService, currentUser, friendship));
            }
            foreach (FriendRequest friendRequest in _user.FriendRequests)
            {
                FriendRequests.Add(new FriendRequestViewModel(this, _serverService, currentUser, friendRequest));
            }
        }

        public async Task SendFriendRequest()
        {
            if (_user.Username != TbInputUsername)
            {
                await _serverService.SendFriendRequest(_user.Username, TbInputUsername);
                TbInputUsername = "";
            }
            
        }

    }
}
