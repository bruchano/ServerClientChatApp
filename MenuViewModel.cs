using Pie.Commands;
using Pie.Commands.ViewCommand;
using Pie.EntityFramework.Services.IAuthenticationService;
using Pie.EntityFramework.Services.UserStateHandlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pie.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private readonly IUserStateHandler _userStateHandler;

        public ViewCommands viewCommands { get; private set; }
        public ICommand LogoutCommand { get; private set; }
        public MenuViewModel(WindowViewModel m, IUserStateHandler userStateHandler, ViewCommands viewCommands)
        {
            windowViewModel = m;
            _userStateHandler = userStateHandler;
            this.viewCommands = viewCommands;
            LogoutCommand = new LogoutCommand(this);
        }

        public async Task Logout()
        {
            await _userStateHandler.Logout();
        }
    }
}
