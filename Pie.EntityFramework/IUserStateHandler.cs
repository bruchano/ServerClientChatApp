using Pie.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pie.EntityFramework.Services.UserStateHandlers
{
    public interface IUserStateHandler
    {
        User CurrentUser { get; }
        bool isLoggedIn { get; }
        Task Login(string username, string password);
        void SetCurrentUser(User user);
        void RefreshCurrentUser(User user);
        Task Register(string email, string username, string password, string confirmPassword);
        Task Logout();
    }
}
