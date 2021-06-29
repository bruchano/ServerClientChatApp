using Microsoft.AspNetCore.SignalR.Client;
using Pie.Domain.Models;
using Pie.Domain.Services;
using Pie.EntityFramework.Services.IAuthenticationService;
using Pie.EntityFramework.Services.ServerServices;
using Pie.EntityFramework.Services.UserStateHandlers;
using Pie.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pie.ServerResponds
{
    class ServerRespondsHandler : IServerRespondsHandler
    {
        private readonly WindowViewModel _windowViewModel;
        private readonly HubConnection _connection;
        private readonly IUserDataService _userDataService;
        private IUserStateHandler _userStateHandler;

        public event Action<string> ServerCallBack;
        public event Action<string> UserLoginResultHandler;
        public event Action<bool> UserLogoutResultHandler;
        public event Action<RegistrationResult> UserRegisterResultHandler;
        public event Action<bool> SendFriendRequestResultHandler;
        public event Action<string> ReceiveFriendRequestResultHandler;
        public event Action<bool> AcceptFriendRequestResultHandler;
        public event Action<bool> ReceiveAcceptFriendRequestResultHandler;
        public event Action<bool> DeclineFriendRequestResultHandler;
        public event Action<bool> DeleteFriendshipResultHandler;
        public event Action<string> ReceiveDeleteFriendshipResultHandler;
        public event Action<bool> SendMessageResultHandler;
        public event Action<string> ReceiveMessageResultHandler;

        public ServerRespondsHandler(WindowViewModel windowViewModel, HubConnection connection, IUserDataService userDataService, IUserStateHandler userStateHandler)
        {
            _windowViewModel = windowViewModel;
            _connection = connection;
            _userDataService = userDataService;
            _userStateHandler = userStateHandler;

            _connection.On<string>("ReceiveServerCallBack", x => ServerCallBack?.Invoke(x));
            _connection.On<string>("UserLoginResult", x => UserLoginResultHandler?.Invoke(x));
            _connection.On<bool>("UserLogoutResult", x => UserLogoutResultHandler?.Invoke(x));
            _connection.On<RegistrationResult>("UserRegisterResult", x => UserRegisterResultHandler?.Invoke(x));
            _connection.On<bool>("SendFriendRequestResult", x => SendFriendRequestResultHandler?.Invoke(x));
            _connection.On<string>("ReceiveFriendRequestResult", x => ReceiveFriendRequestResultHandler?.Invoke(x));
            _connection.On<bool>("AcceptFriendRequestResult", x => AcceptFriendRequestResultHandler?.Invoke(x));
            _connection.On<bool>("ReceiveAcceptFriendRequestResult", x => ReceiveAcceptFriendRequestResultHandler?.Invoke(x));
            _connection.On<bool>("DeclineFriendRequestResult", x => DeclineFriendRequestResultHandler?.Invoke(x));
            _connection.On<bool>("DeleteFriendshipResult", x => DeleteFriendshipResultHandler?.Invoke(x));
            _connection.On<string>("ReceiveDeleteFriendshipResult", x => ReceiveDeleteFriendshipResultHandler?.Invoke(x));
            _connection.On<bool>("SendMessageResult", x => SendMessageResultHandler?.Invoke(x));
            _connection.On<string>("ReceiveMessageResult", x => ReceiveMessageResultHandler?.Invoke(x));

            UserLoginResultHandler += UserLoginResult;
            UserLogoutResultHandler += UserLogoutResult;
            UserRegisterResultHandler += UserRegisterResult;
            SendFriendRequestResultHandler += SendFriendRequestResult;
            ReceiveFriendRequestResultHandler += ReceiveFriendRequestResult;
            AcceptFriendRequestResultHandler += AcceptFriendRequestResult;
            ReceiveAcceptFriendRequestResultHandler += ReceiveAcceptFriendRequestResult;
            DeclineFriendRequestResultHandler += DeclineFriendRequestResult;
            DeleteFriendshipResultHandler += DeleteFriendshipResult;
            ReceiveDeleteFriendshipResultHandler += ReceiveDeleteFriendshipResult;
            SendMessageResultHandler += SendMessageResult;
            ReceiveMessageResultHandler += ReceiveMessageResult;
        }

        public async void UserLoginResult(string username)
        {
            if (username != "")
            {
                User user = await _userDataService.GetByUsername(username);
                _userStateHandler.SetCurrentUser(user);
                _windowViewModel.viewCommands.ShowMenuViewCommand.Execute(null);
                _windowViewModel.viewCommands.ShowUserPanelViewCommand.Execute(null);
            }
        }

        public void UserLogoutResult(bool loggedOut)
        {
            if (loggedOut)
            {
                _windowViewModel.viewCommands.ShowLoginViewCommand.Execute(null);
                _windowViewModel.viewCommands.ShowGuestPanelViewCommand.Execute(null);
            }
        }

        public void UserRegisterResult(RegistrationResult result)
        {
            if (result == RegistrationResult.Success)
            {
                MessageBox.Show("Sucessfully Register");
            } 
            else if (result == RegistrationResult.EmailAlreadyExists)
            {
                MessageBox.Show("Email Already Existed");
            }
            else if (result == RegistrationResult.UsernameAlreadyExists)
            {
                MessageBox.Show("Username Already Existed");
            }
            else if (result == RegistrationResult.PasswordsDoNotMatch)
            {
                MessageBox.Show("Passwords Do Not Match");
            }
        }

        public async void SendFriendRequestResult(bool success)
        {
            if (success)
            {
                User updatedUser = await _userDataService.Get(_userStateHandler.CurrentUser.ID);
                _userStateHandler.RefreshCurrentUser(updatedUser);
            }
        }

        public async void ReceiveFriendRequestResult(string senderUsername)
        {
            User updatedUser = await _userDataService.Get(_userStateHandler.CurrentUser.ID);
            _userStateHandler.RefreshCurrentUser(updatedUser);
        }

        public async void AcceptFriendRequestResult(bool success)
        {
            if (success)
            {
                User updatedUser = await _userDataService.Get(_userStateHandler.CurrentUser.ID);
                _userStateHandler.RefreshCurrentUser(updatedUser);
            }
        }

        public async void ReceiveAcceptFriendRequestResult(bool success)
        {
            if (success)
            {
                User updatedUser = await _userDataService.Get(_userStateHandler.CurrentUser.ID);
                _userStateHandler.RefreshCurrentUser(updatedUser);
            }
        }

        public async void DeclineFriendRequestResult(bool success)
        {
            if (success)
            {
                User updatedUser = await _userDataService.Get(_userStateHandler.CurrentUser.ID);
                _userStateHandler.RefreshCurrentUser(updatedUser);
            }
        }

        public async void DeleteFriendshipResult(bool success)
        {
            if (success)
            {
                User updatedUser = await _userDataService.Get(_userStateHandler.CurrentUser.ID);
                _userStateHandler.RefreshCurrentUser(updatedUser);
            }
        }

        public async void ReceiveDeleteFriendshipResult(string callerUsername)
        {
            User updatedUser = await _userDataService.Get(_userStateHandler.CurrentUser.ID);
            _userStateHandler.RefreshCurrentUser(updatedUser);
        }

        public async void SendMessageResult(bool success)
        {
            if (success)
            {
                User updatedUser = await _userDataService.Get(_userStateHandler.CurrentUser.ID);
                _userStateHandler.RefreshCurrentUser(updatedUser);
            }
        }

        public async void ReceiveMessageResult(string senderUsername)
        {
            User updatedUser = await _userDataService.Get(_userStateHandler.CurrentUser.ID);
            _userStateHandler.RefreshCurrentUser(updatedUser);
        }
    }
}
