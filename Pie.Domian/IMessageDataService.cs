using Pie.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pie.Domain.Services
{
    public interface IMessageDataService
    {
        Task<Message> Create(string senderUsername, int chatID, string content);
        Task<bool> Delete(int id);
    }
}
