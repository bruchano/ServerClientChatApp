using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.SignalR;
using Pie.Domain.Models;
using Pie.Domain.Services;
using Pie.EntityFramework;
using Pie.EntityFramework.Services;
using Pie.EntityFramework.Services.IAuthenticationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pie.Server.Hubs
{
    public class PieHub : Hub
    {
        private readonly PieDbContextFactory _contextFactory;
        private readonly IUserDataService _userDataService;
        private readonly IFriendshipDataService _friendshipDataService;
        private readonly IFriendRequestDataService _friendRequestDataService;
        private readonly IChatDataService _chatDataService;
        private readonly IMessageDataService _messageDataService;
        private readonly IAuthenticationService _authenticationService;

        public PieHub()
        {
            _contextFactory = new PieDbContextFactory();
            _userDataService = new UserDataService(_contextFactory);
            _friendshipDataService = new FriendshipDataService(_contextFactory);
            _friendRequestDataService = new FriendRequestDataService(_contextFactory);
            _chatDataService = new ChatDataService(_contextFactory);
            _messageDataService = new MessageDataService(_contextFactory);
            _authenticationService = new AuthenticationService(_userDataService, new PasswordHasher());
        }

        public override Task OnConnectedAsync()
        {
            
            return base.OnConnectedAsync();
        }

        public async Task UserLogin(string username, string password)
        {
            User loginUser = await _authenticationService.Login(username, password);
            if (loginUser != null)
            {
                loginUser = await _userDataService.GetByUsername(loginUser.Username);
                await Groups.AddToGroupAsync(Context.ConnectionId, $"User_{loginUser.Username}");
                Console.WriteLine($"Added to User group User_{loginUser.Username} and callback sent");
                foreach (UserChat userChat in loginUser.Chats)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, $"Chat_{userChat.Chat.ID}");
                    Console.WriteLine($"Added to Chat group Chat_{userChat.Chat.ID} created and callback sent");
                }
                await Clients.Caller.SendAsync("UserLoginResult", loginUser.Username);
            }
            else
            {
                await Clients.Caller.SendAsync("UserLoginResult", "");
                Console.WriteLine("UserLogin failed and callback sent");
            }

        }
        public async Task UserLogout(string username)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"User_{username}");
            await Clients.Caller.SendAsync("UserLogoutResult", true);
            Console.WriteLine("User removed from the server callback sent");
        }

        public async Task UserRegister(string email, string username, string password, string confirmPassword)
        {

        }

        public async Task SendFriendRequest(string senderUsername, string receiverUsername)
        {
            Console.WriteLine("SendFriendRequest called");
            User receiver = await _userDataService.UserEntityByUsername(receiverUsername);
            if (receiver != null)
            {
                FriendRequest friendRequest = await _friendRequestDataService.Create(senderUsername, receiverUsername);
                if (friendRequest != null)
                {
                    
                    await Clients.Caller.SendAsync("SendFriendRequestResult", true);
                    await Clients.Group($"User_{receiverUsername}").SendAsync("ReceiveFriendRequestResult", senderUsername);
                    Console.WriteLine("FriendRequest created and callback sent");
                }
            }
        }

        public async Task AcceptFriendRequest(string senderUsername, string receiverUsername)
        {
            bool success = await _friendRequestDataService.Delete(senderUsername, receiverUsername);
            if (success)
            {
                Friendship friendship = await _friendshipDataService.Create(senderUsername, receiverUsername);
                await Clients.Caller.SendAsync("AcceptFriendRequestResult", friendship != null);
                await Clients.Group($"User_{senderUsername}").SendAsync("ReceiveAcceptFriendRequestResult", friendship != null);
                Console.WriteLine("Friendship created and callback sent\n");
            }
            else
            {
                await Clients.Caller.SendAsync("AcceptFriendRequestResult", success);
                await Clients.Group($"User_{senderUsername}").SendAsync("ReceiveAcceptFriendRequestResult", success);
                Console.WriteLine("Friendship creation failed and callback sent\n");
            }
        }

        public async Task DeclineFriendRequest(string senderUsername, string receiverUsername)
        {
            bool success = await _friendRequestDataService.Delete(senderUsername, receiverUsername);
            await Clients.Caller.SendAsync("DeclineFriendRequestResult", success);
            Console.WriteLine("FriendRequest deleted and callback sent\n");
        }

        public async Task DeleteFriendship(string callerUsername, string targetUsername)
        {
            bool success = await _friendshipDataService.Delete(callerUsername, targetUsername);
            await Clients.Caller.SendAsync("DeleteFriendshipResult", success);
            await Clients.Group($"User_{targetUsername}").SendAsync("ReceiveDeleteFriendshipResult", callerUsername);
            Console.WriteLine("Friendship deleted and callback sent\n");
        }

        public async Task SendMessage(string senderUsername, int chatID, string message)
        {
            Message newMessage = await _messageDataService.Create(senderUsername, chatID, message);
            await Clients.Caller.SendAsync("SendMessageResult", newMessage != null);
            await Clients.Group($"Chat_{chatID}").SendAsync("ReceiveMessageResult", senderUsername);
            Console.WriteLine($"Message created and callback sent to User_{senderUsername} and Chat_{chatID}\n");
        }
    }
}
