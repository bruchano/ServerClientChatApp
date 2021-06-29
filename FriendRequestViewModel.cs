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
    public class FriendRequestViewModel : BaseViewModel
    {
        private readonly ServerService _serverService;
        private readonly User _user;
        private readonly FriendRequest _friendRequest;
        private FriendsPanelViewModel _friendsPanelViewModel;

        public ICommand AcceptFriendRequestCommand { get; private set; }
        public ICommand DeclineFriendRequestCommand { get; private set; }


        public string SenderUsername
        {
            get
            {
                return _friendRequest.Sender.Username;
            }
        }
        public string SenderEmail
        {
            get
            {
                return _friendRequest.Sender.Email;
            }
        }

        public DateTime TimeCreated
        {
            get
            {
                return _friendRequest.TimeCreated;
            }
        }

        public FriendRequestViewModel(FriendsPanelViewModel friendsPanelViewModel, ServerService serverService, User user, FriendRequest friendRequest)
        {
            _friendsPanelViewModel = friendsPanelViewModel;
            _serverService = serverService;
            _user = user;
            _friendRequest = friendRequest;

            AcceptFriendRequestCommand = new AcceptFriendRequestCommand(this);
            DeclineFriendRequestCommand = new DeclineFriendRequestCommand(this);
        }

        public async Task AcceptFriendRequest()
        {
            await _serverService.AcceptFriendRequest(SenderUsername, _user.Username);
            _friendsPanelViewModel.FriendRequests.Remove(this);
            _friendsPanelViewModel.Friends.Add(new FriendViewModel(_friendsPanelViewModel, _serverService, _user, new Friendship() { Inviter = _friendRequest.Sender, Accepter = _user }));
        }

        public async Task DeclineFriendRequest()
        {
            await _serverService.DeclineFriendRequest(SenderUsername, _user.Username);
            _friendsPanelViewModel.FriendRequests.Remove(this);
        }



    }
}
