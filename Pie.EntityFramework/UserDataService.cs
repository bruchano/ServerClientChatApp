using Microsoft.EntityFrameworkCore;
using Pie.Domain.Models;
using Pie.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pie.EntityFramework.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly PieDbContextFactory _contextFactory;

        public UserDataService(PieDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<User> Create(User entity)
        {
            using (PieDbContext context = _contextFactory.CreateDbContext())
            {
                if (await context.Users.FirstOrDefaultAsync(a => a == entity) == null)
                {
                    var createdEntity = await context.Users.AddAsync(entity);
                    await context.SaveChangesAsync();

                    return createdEntity.Entity;
                }
            }
            return null;
        }

        public async Task<bool> Delete(int id)
        {
            using (PieDbContext context = _contextFactory.CreateDbContext())
            {
                User entity = await context.Users.FirstOrDefaultAsync(a => a.ID == id);
                context.Users.Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<User> Get(int id)
        {
            using (PieDbContext context = _contextFactory.CreateDbContext())
            {
                User entity = await context.Users.FindAsync(id);
                if (entity != null)
                {
                    entity = await context.Users
                        .Include(user => user.InvitedFriendships).ThenInclude(friendship => friendship.Accepter)
                        .Include(user => user.AcceptedFriendships).ThenInclude(friendship => friendship.Inviter)
                        .Include(user => user.FriendRequests).ThenInclude(friendRequest => friendRequest.Sender)
                        .Include(user => user.Chats).ThenInclude(userChat => userChat.Chat).ThenInclude(chat => chat.Messages)
                        .FirstOrDefaultAsync(a => a.ID == id);
                    return entity;
                }
                else
                {
                    return entity;
                }
            }
        }

        public async Task<ICollection<User>> GetAll()
        {
            using (PieDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Users
                    .Include(user => user.InvitedFriendships).ThenInclude(friendship => friendship.Accepter)
                    .Include(user => user.AcceptedFriendships).ThenInclude(friendship => friendship.Inviter)
                    .Include(user => user.FriendRequests).ThenInclude(friendRequest => friendRequest.Sender)
                    .Include(user => user.Chats).ThenInclude(userChat => userChat.Chat).ThenInclude(chat => chat.Messages)
                    .ToListAsync();
            }
        }

        public async Task<User> GetByEmail(string email)
        {
            using (PieDbContext context = _contextFactory.CreateDbContext())
            {
                User entity = await context.Users.FirstOrDefaultAsync(a => a.Email == email);
                if (entity != null)
                {
                    entity = await context.Users
                        .Include(user => user.InvitedFriendships).ThenInclude(friendship => friendship.Accepter)
                        .Include(user => user.AcceptedFriendships).ThenInclude(friendship => friendship.Inviter)
                        .Include(user => user.FriendRequests).ThenInclude(friendRequest => friendRequest.Sender)
                        .Include(user => user.Chats).ThenInclude(userChat => userChat.Chat).ThenInclude(chat => chat.Messages)
                        .FirstOrDefaultAsync(a => a.Email == email);
                    return entity;
                }
                else
                {
                    return entity;
                }
            }
        }

        public async Task<User> GetByUsername(string username)
        {
            using (PieDbContext context = _contextFactory.CreateDbContext())
            {
                User entity = await context.Users.FirstOrDefaultAsync(a => a.Username == username);
                if (entity != null)
                {
                    entity = await context.Users
                        .Include(user => user.InvitedFriendships).ThenInclude(friendship => friendship.Accepter)
                        .Include(user => user.AcceptedFriendships).ThenInclude(friendship => friendship.Inviter)
                        .Include(user => user.FriendRequests).ThenInclude(friendRequest => friendRequest.Sender)
                        .Include(user => user.Chats).ThenInclude(userChat => userChat.Chat).ThenInclude(chat => chat.Messages)
                        .FirstOrDefaultAsync(a => a.Username == username);
                    return entity;
                }
                else
                {
                    return entity;
                }

            }
        }

        public async Task<User> Update(int id, User entity)
        {
            using (PieDbContext context = _contextFactory.CreateDbContext())
            {
                entity.ID = id;
                context.Users.Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<User> UserEntityByEmail(string email)
        {
            using (PieDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Users.FirstOrDefaultAsync(a => a.Email == email);
            }
        }

        public async Task<User> UserEntityByUsername(string username)
        {
            using (PieDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Users.FirstOrDefaultAsync(a => a.Username == username);
            }
        }
    }
}
