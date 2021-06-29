using Pie.Domain.Models;
using Pie.EntityFramework.Services.ServerServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pie.EntityFramework.Services.UserStateHandlers
{
    public class UserStateHandler : PropertyChangedObject, IUserStateHandler
    {
        private readonly ServerService _serverService;
        private User _currentUser;

        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }
            private set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        public bool isLoggedIn
        {
            get
            {
                return CurrentUser != null;
            }
        }

        public UserStateHandler(ServerService serverService)
        {
            _serverService = serverService;
        }

        public void SetCurrentUser(User user)
        {
            CurrentUser = user;
        }
        public void RefreshCurrentUser(User user)
        {
            if (user.ID == CurrentUser.ID)
            {
                CurrentUser = user;
            }
        }
        

        public async Task Login(string username, string password)
        {
            await _serverService.Login(username, password);
        }

        public async Task Register(string email, string username, string password, string confirmPassword)
        {
            await _serverService.Register(email, username, password, confirmPassword);
        }

        public async Task Logout()
        {
            if (isLoggedIn)
            {
                await _serverService.Logout(CurrentUser.Username);
                CurrentUser = null;
            }
        }
    }
}
