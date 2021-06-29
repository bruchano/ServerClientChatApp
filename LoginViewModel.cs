using Pie.Commands;
using Pie.Commands.ViewCommand;
using Pie.EntityFramework.Services.IAuthenticationService;
using Pie.EntityFramework.Services.UserStateHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Pie.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IUserStateHandler _userStateHandler;
        public ViewCommands viewCommands { get; private set; }
        public ICommand UsernameGotFocusCommand { get; private set; }
        public ICommand PasswordGotFocusCommand { get; private set; }
        public ICommand LoginCommand { get; private set; }

        private string _tbUsername;

        public string TbUsername
        {
            get
            {
                return _tbUsername;
            }
            set
            {
                _tbUsername = value;
                OnPropertyChanged(nameof(TbUsername));
            }
        }

        public LoginViewModel(WindowViewModel m, IUserStateHandler userStateHandler, ViewCommands viewCommands)
        {
            windowViewModel = m;
            _userStateHandler = userStateHandler;
            this.viewCommands = viewCommands;

            UsernameGotFocusCommand = new VoidCommand(UsernameGotFocus);
            PasswordGotFocusCommand = new PasswordBoxGotFocusCommand(PasswordGotFocus);
            LoginCommand = new LoginCommand(this);
        }

        public void UsernameGotFocus()
        {
            if (TbUsername == "username") TbUsername = "";
        }

        public void PasswordGotFocus(PasswordBox passwordBox)
        {
            if (passwordBox.Password == "******") passwordBox.Password = "";
        }

        public async Task Login(string username, string password)
        {
            await _userStateHandler.Login(username, password);
        }
    }
}
