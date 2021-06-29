using Microsoft.EntityFrameworkCore;
using Pie.Domain.Models;
using Pie.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pie.EntityFramework.Services
{
    public class UserChatDataService : IUserChatDataService
    {
        private readonly PieDbContextFactory _contextFactory;

        public UserChatDataService(PieDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<bool> Add(string username, int chatID)
        {
            using (PieDbContext context = _contextFactory.CreateDbContext())
            {
                if (await context.UserChats.FirstOrDefaultAsync(userChat => userChat.User.Username == username && userChat.Chat.ID == chatID) == null)
                {
                    UserChat newEntity = new UserChat()
                    {
                        User = await context.Users.FirstOrDefaultAsync(x => x.Username == username),
                        Chat = await context.Chats.FirstOrDefaultAsync(x => x.ID == chatID)
                    };

                    var x = await context.UserChats.AddAsync(newEntity);
                    await context.SaveChangesAsync();
                    return x != null;
                }
            }
            return false;
        }

        public async Task<UserChat> CreatGroupChat(string groupName, object usernames)
        {
            throw new NotImplementedException();
        }

        public async Task<UserChat> CreatPrivateChat(string usernameA, string usernameB)
        {
            using (PieDbContext context = _contextFactory.CreateDbContext())
            {
                User userA = await context.Users.FirstOrDefaultAsync(x => x.Username == usernameA);
                User userB = await context.Users.FirstOrDefaultAsync(x => x.Username == usernameB);


            }
            return null;
        }

        public async Task<UserChat> CreatPublicChat(string groupName, object usernames)
        {
            throw new NotImplementedException();

        }


        public async Task<bool> Delete(int id)
        {
            using (PieDbContext context = _contextFactory.CreateDbContext())
            {
                UserChat x = await context.UserChats.FirstOrDefaultAsync(x => x.ID == id);
                context.UserChats.Remove(x);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<UserChat> Get(int id)
        {
            using (PieDbContext context = _contextFactory.CreateDbContext())
            {
                UserChat x = await context.UserChats
                    .FirstOrDefaultAsync(x => x.ID == id);
                return x;
            }
        }

        public async Task<ICollection<UserChat>> GetAll()
        {
            using (PieDbContext context = _contextFactory.CreateDbContext())
            {
                ICollection<UserChat> x = await context.UserChats
                    .ToListAsync();
                return x;
            }
        }

        public async Task<bool> Update(int id, UserChat entity)
        {
            using (PieDbContext context = _contextFactory.CreateDbContext())
            {
                entity.ID = id;
                context.UserChats.Update(entity);
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
