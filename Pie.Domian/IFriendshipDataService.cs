using Pie.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pie.Domain.Services
{
    public interface IFriendshipDataService
    {
        Task<Friendship> Create(string inviterUsername, string accepterUsername);
        Task<Friendship> Update(int id);
        Task<Friendship> Update(string inviterUsername, string accepterUsername);
        Task<bool> Delete(int id);
        Task<bool> Delete(string inviterUsername, string accepterUsername);
        Task<Friendship> Get(int id);
        Task<Friendship> GetByInviterAccepter(string inviterUsername, string accepterUsername);
    }
}
