using Pie.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pie.Domain.Services
{
    public interface IUserDataService : IDataService<User>
    {
        Task<User> GetByEmail(string email);
        Task<User> GetByUsername(string username);
        Task<User> UserEntityByUsername(string username);
        Task<User> UserEntityByEmail(string email);
    }
}
