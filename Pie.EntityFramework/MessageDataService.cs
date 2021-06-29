using Microsoft.EntityFrameworkCore;
using Pie.Domain.Models;
using Pie.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pie.EntityFramework.Services
{
    public class MessageDataService : IMessageDataService
    {
        private readonly PieDbContextFactory _contextFactory;

        public MessageDataService(PieDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Message> Create(string senderUsername, int chatID, string content)
        {
            using (PieDbContext context = _contextFactory.CreateDbContext())
            {
                Message newMessage = new Message()
                {
                    Sender = await context.Users.FirstOrDefaultAsync(x => x.Username == senderUsername),
                    Chat = await context.Chats.FirstOrDefaultAsync(x => x.ID == chatID),
                    Text = content
                };

                var newEntity = await context.Messages.AddAsync(newMessage);
                await context.SaveChangesAsync();
                return newEntity.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (PieDbContext context = _contextFactory.CreateDbContext())
            {

                Message message = await context.Messages.FirstOrDefaultAsync(x => x.ID == id);
                context.Messages.Add(message);
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
