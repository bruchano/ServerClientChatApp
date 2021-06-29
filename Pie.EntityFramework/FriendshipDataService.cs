using Microsoft.EntityFrameworkCore;
using Pie.Domain.Models;
using Pie.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pie.EntityFramework.Services
{
    public class FriendshipDataService : IFriendshipDataService
    {
        private readonly PieDbContextFactory _contextFactory;

        public FriendshipDataService(PieDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Friendship> Create(string inviterUsername, string accepterUsername)
        {
            using (PieDbContext context = _contextFactory.CreateDbContext())
            {
                Friendship friendship = await context.Friendships.FirstOrDefaultAsync(a => a.Inviter.Username == inviterUsername && a.Accepter.Username == accepterUsername);
                Friendship friendshipReverse = await context.Friendships.FirstOrDefaultAsync(a => a.Inviter.Username == accepterUsername && a.Accepter.Username == inviterUsername);
                if (friendship == null && friendshipReverse == null)
                {
                    User inviter = await context.Users.FirstOrDefaultAsync(a => a.Username == inviterUsername);
                    User accepter = await context.Users.FirstOrDefaultAsync(a => a.Username == accepterUsername);
                    Friendship newFriendship = new Friendship() { Inviter = inviter, Accepter = accepter };
                    var newEntity = await context.Friendships.AddAsync(newFriendship);
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
                Friendship friendship = await context.Friendships.FirstOrDefaultAsync(a => a.ID == id);
                if (friendship != null)
                {
                    context.Friendships.Remove(friendship);
                    await context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
        }

        public async Task<bool> Delete(string callerUsername, string targetUsername)
        {
            using (PieDbContext context = _contextFactory.CreateDbContext())
            {
                Friendship friendship = await context.Friendships.FirstOrDefaultAsync(a => a.Inviter.Username == callerUsername && a.Accepter.Username == targetUsername);
                if (friendship != null)
                {
                    context.Friendships.Remove(friendship);
                    await context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    friendship = await context.Friendships.FirstOrDefaultAsync(a => a.Inviter.Username == targetUsername && a.Accepter.Username == callerUsername);
                    if (friendship != null)
                    {
                        context.Friendships.Remove(friendship);
                        await context.SaveChangesAsync();
                        return true;
                    }
                }
                return false;
            }
        }

        public async Task<Friendship> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Friendship> GetByInviterAccepter(string inviterUsername, string accepterUsername)
        {
            throw new NotImplementedException();
        }

        public async Task<Friendship> Update(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Friendship> Update(string inviterUsername, string accepterUsername)
        {
            throw new NotImplementedException();
        }
    }
}
