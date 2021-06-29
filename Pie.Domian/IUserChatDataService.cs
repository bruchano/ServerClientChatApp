using Pie.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pie.Domain.Services
{
    public interface IUserChatDataService
    {
        Task<UserChat> Get(int id);
        Task<ICollection<UserChat>> GetAll();
        Task<UserChat> CreatPrivateChat(string usernameA, string usernameB);
        Task<UserChat> CreatGroupChat(string groupName, object usernames);
        Task<UserChat> CreatPublicChat(string groupName, object usernames);
        Task<bool> Add(string username, int chatID);
        Task<bool> Delete(int id);
        Task<bool> Update(int id, UserChat entity);
    }
}
