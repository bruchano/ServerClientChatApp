using Pie.Domain.Models;
using Pie.EntityFramework.Services.IAuthenticationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pie.ServerResponds
{
    public interface IServerRespondsHandler
    {
        void UserLoginResult(string username);
        void UserLogoutResult(bool loggedOut);
        void UserRegisterResult(RegistrationResult result);
        void SendFriendRequestResult(bool success);
        void ReceiveFriendRequestResult(string senderUsername);
        void AcceptFriendRequestResult(bool success);
        void ReceiveAcceptFriendRequestResult(bool success);
        void DeclineFriendRequestResult(bool success);
        void DeleteFriendshipResult(bool success);
        void ReceiveDeleteFriendshipResult(string callerUsername);
        void SendMessageResult(bool success);
        void ReceiveMessageResult(string senderUsername);

    }
}
