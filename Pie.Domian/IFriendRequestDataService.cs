using Pie.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pie.Domain.Services
{
    public interface IFriendRequestDataService
    {
        Task<FriendRequest> Create(string senderUsername, string receiverUsername);
        Task<FriendRequest> Update(int id);
        Task<FriendRequest> Update(string senderUsername, string receiverUsername);
        Task<bool> Delete(int id);
        Task<bool> Delete(string senderUsername, string receiverUsername);
        Task<FriendRequest> Get(int id);
        Task<FriendRequest> GetBySenderReceiver(string senderUsername, string receiverUsername);
    }
}
