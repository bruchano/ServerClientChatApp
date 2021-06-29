using Microsoft.EntityFrameworkCore;
using Pie.Domain.Models;
using Pie.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pie.EntityFramework.Services
{
    public class FriendRequestDataService : IFriendRequestDataService
    {
        private readonly PieDbContextFactory _contextFactory;

        public FriendRequestDataService(PieDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<FriendRequest> Create(string senderUsername, string receiverUsername)
        {
            using (PieDbContext context = _contextFactory.CreateDbContext())
            {
                FriendRequest friendRequest = await context.FriendRequests.FirstOrDefaultAsync(a => a.Sender.Username == senderUsername && a.Receiver.Username == receiverUsername);
                FriendRequest friendRequestReverse = await context.FriendRequests.FirstOrDefaultAsync(a => a.Sender.Username == receiverUsername && a.Receiver.Username == senderUsername);
                Friendship friendship = await context.Friendships.FirstOrDefaultAsync(a => a.Inviter.Username == senderUsername && a.Accepter.Username == receiverUsername);
                Friendship friendshipReverse = await context.Friendships.FirstOrDefaultAsync(a => a.Inviter.Username == receiverUsername && a.Accepter.Username == senderUsername);

                if (friendRequest == null && friendRequestReverse == null && friendship == null && friendshipReverse == null)
                {
                    User sender = await context.Users.FirstOrDefaultAsync(a => a.Username == senderUsername);
                    User receiver = await context.Users.FirstOrDefaultAsync(a => a.Username == receiverUsername);
                    FriendRequest newfriendRequest = new FriendRequest() { Sender = sender, Receiver = receiver };
                    var newEntity = await context.FriendRequests.AddAsync(newfriendRequest);
                    await context.SaveChangesAsync();
                    return newEntity.Entity;
                }
            }
            return null;
        }

        public async Task<bool> Delete(int id)
        {
            using (PieDbContext context = _contextFactory.CreateDbContext())
            {
                FriendRequest friendRequest = await context.FriendRequests.FirstOrDefaultAsync(a => a.ID == id);
                if (friendRequest != null)
                {
                    context.FriendRequests.Remove(friendRequest);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> Delete(string senderUsername, string receiverUsername)
        {
            using (PieDbContext context = _contextFactory.CreateDbContext())
            {
                FriendRequest friendRequest = await context.FriendRequests.FirstOrDefaultAsync(a => a.Sender.Username == senderUsername && a.Receiver.Username == receiverUsername);
                if (friendRequest != null)
                {
                    context.FriendRequests.Remove(friendRequest);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<FriendRequest> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<FriendRequest> GetBySenderReceiver(string senderUsername, string receiverUsername)
        {
            throw new NotImplementedException();
        }

        public async Task<FriendRequest> Update(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<FriendRequest> Update(string senderUsername, string receiverUsername)
        {
            throw new NotImplementedException();
        }
    }
}
