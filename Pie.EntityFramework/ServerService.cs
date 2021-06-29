using Microsoft.AspNetCore.SignalR.Client;
using Pie.Domain.Models;
using Pie.EntityFramework.Services.IAuthenticationService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pie.EntityFramework.Services.ServerServices
{
    public class ServerService : PropertyChangedObject
    {
        private readonly HubConnection _connection;
        
        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ServerService(HubConnection connection)
        {
            _connection = connection;
        }

        public static ServerService CreateServerService(HubConnection connection)
        {
            ServerService serverService = new ServerService(connection);
            serverService.ConnectServer().ContinueWith(task =>
            {
                if (task.Exception != null)
                {
                    serverService.ErrorMessage = "Unable to connect to server.";
                }
            });
            return serverService;
        }

        public async Task ConnectServer()
        {
            await _connection.StartAsync();
        }

        public async Task Login(string username, string password)
        {
            await _connection.SendAsync("UserLogin", username, password);
        }

        public async Task Register(string email, string username, string password, string confirmPassword)
        {
            await _connection.SendAsync("UserRegister", email, username, password, confirmPassword);
        }

        public async Task Logout(string username)
        {
            await _connection.SendAsync("UserLogout", username);
        }

        public async Task SendFriendRequest(string senderUsername, string receiverUsername)
        {
            await _connection.SendAsync("SendFriendRequest", senderUsername, receiverUsername);
        }

        public async Task AcceptFriendRequest(string senderUsername, string receiverUsername)
        {
            await _connection.SendAsync("AcceptFriendRequest", senderUsername, receiverUsername);
        }

        public async Task DeclineFriendRequest(string senderUsername, string receiverUsername)
        {
            await _connection.SendAsync("DeclineFriendRequest", senderUsername, receiverUsername);
        }

        public async Task DeleteFriendship(string inviterUsername, string accepterUsername)
        {
            await _connection.SendAsync("DeleteFriendship", inviterUsername, accepterUsername);
        }

        public async Task SendMessage(string senderUsername, int chatID, string message)
        {
            await _connection.SendAsync("SendMessage", senderUsername, chatID, message);
        }
    }
}
